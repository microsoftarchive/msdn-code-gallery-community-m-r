// <copyright file="SequenceRegistry.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: SequenceRegistry.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Modules.Services.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Media.Imaging;
    using Infrastructure.Events;
    using Microsoft.Practices.Composite.Events;
    using Microsoft.Practices.Composite.Presentation.Events;
    using Microsoft.Practices.Unity;
    using RCE.Infrastructure;
    using RCE.Infrastructure.Models;
    using RCE.Infrastructure.Services;
    using SMPTETimecode;

    public class SequenceRegistry : ISequenceRegistry
    {
        private const int DefaultTimelineDuration = 10000;
        
        private readonly IUnityContainer container;

        private readonly IDictionary<Sequence, ISequenceModel> sequences;

        private readonly IProjectService projectService;

        private readonly IEventAggregator eventAggregator;

        private readonly double timelineDuration;

        private ISequenceModel currentSequenceModel;

        public SequenceRegistry(IUnityContainer container, IConfigurationService configurationService, IEventAggregator eventAggregator, IProjectService projectService)
        {
            this.container = container;
            this.timelineDuration = configurationService.GetParameterValueAsInt("DefaultTimelineDurationInSeconds").GetValueOrDefault(DefaultTimelineDuration);
            this.sequences = new Dictionary<Sequence, ISequenceModel>();
            this.eventAggregator = eventAggregator;
            this.eventAggregator.GetEvent<ThumbnailEvent>().Subscribe(this.SetThumbnail, ThreadOption.PublisherThread, true, this.FilterAddThumbnailEvent);
            this.projectService = projectService;
            this.projectService.ProjectRetrieved += this.UpdateSequences;
        }

        public event EventHandler<Infrastructure.DataEventArgs<ISequenceModel>> CurrentSequenceChanged;

        public IEnumerable<ISequenceModel> Sequences
        {
            get
            {
                return this.sequences.Values;
            }
        }

        public Sequence CurrentSequence 
        { 
            get
            {
                foreach (KeyValuePair<Sequence, ISequenceModel> keyValuePair in this.sequences)
                {
                    if (keyValuePair.Value == this.CurrentSequenceModel)
                    {
                        return keyValuePair.Key;
                    }
                }

                return null;
            }
        }

        public ISequenceModel CurrentSequenceModel
        {
            get
            {
                return this.currentSequenceModel;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var previousSequence = this.currentSequenceModel;
                this.currentSequenceModel = value;
                this.InvokeCurrentSequenceChanged(previousSequence);
            }
        }

        public ISequenceModel CreateSequence(Sequence sequence, TimeCode duration)
        {
            var newSequence = this.container.Resolve<ISequenceModel>();

            if (this.sequences.ContainsKey(sequence))
            {
                this.sequences[sequence] = newSequence;
            }
            else
            {
                this.sequences.Add(sequence, newSequence);
            }

            newSequence.Duration = duration;
            this.CurrentSequenceModel = newSequence;

            foreach (var track in sequence.Tracks)
            {
                newSequence.AddTrack(track);
            }

            return newSequence;            
        }

        public ISequenceModel CreateSequence(Sequence sequence)
        {
            return this.CreateSequence(sequence, TimeCode.FromSeconds(this.timelineDuration, SmpteFrameRate.Smpte2997Drop));
        }

        public ISequenceModel GetSequenceForTimeline(Sequence sequence)
        {
            return this.sequences[sequence];
        }

        private void InvokeCurrentSequenceChanged(ISequenceModel previousSequence)
        {
            EventHandler<Infrastructure.DataEventArgs<ISequenceModel>> handler = this.CurrentSequenceChanged;
            if (handler != null)
            {
                handler(this, new Infrastructure.DataEventArgs<ISequenceModel>(previousSequence));
            }
        }

        /// <summary>
        /// Encodes the given writeable bitmap using a <seealso cref="PngEncoder"/>.
        /// </summary>
        /// <param name="writeableBitmap">The bitmap to encode.</param>
        /// <returns>A base 64 representation of the bitmap encoded.</returns>
        private string EncodeImage(WriteableBitmap writeableBitmap)
        {
            string projectThumbnail = null;

            MemoryStream ms = null;

            try
            {
                ms = PngEncoder.Encode(writeableBitmap) as MemoryStream;
            }
            catch (Exception ex)
            {
                // TODO: log exception
            }

            if (ms != null)
            {
                BinaryReader binaryReader = new BinaryReader(ms);
                byte[] currentBytes = binaryReader.ReadBytes(1000);
                int bytesRead = currentBytes.Length;
                List<byte> byteList = currentBytes.ToList();

                while (bytesRead == 1000)
                {
                    currentBytes = binaryReader.ReadBytes(1000);
                    bytesRead = currentBytes.Length;
                    byteList.AddRange(currentBytes);
                }

                binaryReader.Close();
                ms.Close();

                projectThumbnail = Convert.ToBase64String(byteList.ToArray());
            }

            return projectThumbnail;
        }

        private void UpdateSequences(object sender, EventArgs e)
        {
            Project project = this.projectService.GetCurrentProject();
            this.sequences.Clear();
   
            foreach (var sequence in project.Timelines)
            {
                this.CreateSequence(sequence, this.GetSequenceDuration(sequence));
            }

            if (this.sequences.Values.Count > 0)
            {
                this.CurrentSequenceModel = this.sequences.Values.First();
            }

            this.eventAggregator.GetEvent<OperationExecutedInTimelineEvent>().Publish(null);
        }

        private TimeCode GetSequenceDuration(Sequence sequence)
        {
            double maxDuration = this.timelineDuration;

            foreach (var track in sequence.Tracks)
            {
                foreach (var shot in track.Shots)
                {
                    var duration = shot.Duration.TotalSeconds + shot.Position.TotalSeconds;
                    
                    if (duration > maxDuration)
                    {
                        maxDuration = duration;
                    }
                }
            }

            return TimeCode.FromSeconds(maxDuration * 1.1, SmpteFrameRate.Smpte2997Drop);
        }

        /// <summary>
        /// Sets the project thumbnail.
        /// </summary>
        private void SetThumbnail(ThumbnailEventPayload payload)
        {
            WriteableBitmap writeableBitmap = payload.Bitmap;

            if (writeableBitmap != null)
            {
                string encodedImage = this.EncodeImage(writeableBitmap);

                if (!string.IsNullOrEmpty(encodedImage))
                {
                    this.CurrentSequence.EncodedThumbnail = encodedImage;
                    this.CurrentSequence.Thumbnail = writeableBitmap;
                }
            }
        }

        private bool FilterAddThumbnailEvent(ThumbnailEventPayload payload)
        {
            return payload.Type == ThumbnailType.SequenceThumbnail;
        }
    }
}
