// <copyright file="OutputService.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: OutputService.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Services
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel.Activation;
    using System.Web;
    using RCE.Services.Contracts;
    using SmoothStreamingManifestGenerator;
    using SmoothStreamingManifestGenerator.Models;
    using SMPTETimecode;

    /// <summary>
    /// Service used to save the output project on the server.
    /// </summary>
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class OutputService : IOutputService
    {
        private Uri gapUri;

        private Project project;

        /// <summary>
        /// Saves the project on the server.
        /// </summary>
        /// <param name="project">The project being saved.</param>
        public void EnqueueJob(Project project)
        {
            string queuePath = HttpContext.Current.Server.MapPath("encode/Queue");

            if (!Directory.Exists(queuePath))
            {
                Directory.CreateDirectory(queuePath);
            }

            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            string tmpFilePath = Path.Combine(queuePath, string.Format(CultureInfo.InvariantCulture, "{0}-{1}.jobtmp", project.Title.ToString(), datetime));
            string finalFilePath = Path.Combine(queuePath, string.Format(CultureInfo.InvariantCulture, "{0}-{1}.jobreq", project.Title.ToString(), datetime));

            using (FileStream fs = new FileStream(tmpFilePath, FileMode.Create, FileAccess.Write))
            {
                DataContractSerializer dataContractSerializer = new DataContractSerializer(typeof(Project));
                dataContractSerializer.WriteObject(fs, project);
                fs.Flush();
            }

            if (File.Exists(tmpFilePath))
            {
                if (File.Exists(finalFilePath))
                {
                    File.Delete(finalFilePath);
                }

                File.Move(tmpFilePath, finalFilePath);
            }
        }

        public string GenerateCompositeStreamManifest(Project project, string pbpDataStreamName, string adsDataStreamName, bool compressManifest, string gapUriString, string gapCmsId, string gapAzureId)
        {
            this.project = project;

            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);

            string queuePath = HttpContext.Current.Server.MapPath("encode/CSM");

            string outputDirectory = Path.Combine(queuePath, string.Format(CultureInfo.InvariantCulture, "{0}-{1}", project.Title, datetime));

            for (int j = 0; j < project.Sequences.Count; j++)
            {
                IEnumerable<string> manifests = this.GetManifests(pbpDataStreamName, adsDataStreamName, compressManifest, gapUriString, gapCmsId, gapAzureId, j);

                if (!Directory.Exists(outputDirectory))
                {
                    Directory.CreateDirectory(outputDirectory);
                }

                int i = 0;

                foreach (var manifest in manifests)
                {
                    string fileName = Path.Combine(outputDirectory, string.Format("T{1}.S{0}.csm", i, j));

                    using (StreamWriter output = new StreamWriter(fileName))
                    {
                        output.Write(manifest);
                        output.Flush();
                    }

                    i++;
                }
            }

            string csmUriString = string.Format(
                "{2}{3}{0}-{1}/T#.S#.csm",
                project.Title,
                datetime,
                HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/")),
                "/encode/CSM/");

            return csmUriString;
        }

        public string PersistManifest(string manifest)
        {
            string queuePath = HttpContext.Current.Server.MapPath("encode/CSM");

            if (!Directory.Exists(queuePath))
            {
                Directory.CreateDirectory(queuePath);
            }

            Guid guid = Guid.NewGuid();

            string datetime = DateTime.Now.ToString("yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            string outputFile = Path.Combine(
                queuePath, string.Format(CultureInfo.InvariantCulture, "{0}-{1}.csm", guid, datetime));

            using (StreamWriter output = new StreamWriter(outputFile))
            {
                output.Write(manifest);
                output.Flush();
            }

            string csmUriString = string.Format(
                "{2}{3}{0}-{1}.csm",
                guid,
                datetime,
                HttpContext.Current.Request.Url.AbsoluteUri.Substring(0, HttpContext.Current.Request.Url.AbsoluteUri.LastIndexOf("/")),
                "/encode/CSM/");

            return csmUriString;
        }

        private static IDictionary<Shot, double> CalculateGapsDuration(Track track)
        {
            IDictionary<Shot, double> gapDurations = new Dictionary<Shot, double>();
            for (int i = 0; i < track.Shots.Count; i++)
            {
                double? duration;
                var currentElement = track.Shots[i];
                if (i > 0)
                {
                    var previousElement = track.Shots[i - 1];
                    duration = currentElement.TrackAnchor.MarkIn - (previousElement.TrackAnchor.MarkIn +
                        (previousElement.SourceAnchor.MarkOut - previousElement.SourceAnchor.MarkIn));
                }
                else
                {
                    duration = currentElement.TrackAnchor.MarkIn;
                }

                gapDurations.Add(currentElement, duration.Value);
            }

            return gapDurations;
        }

        private static void AddClipToCompositeManifestInfo(Shot shot, Stream manifestStream, CompositeManifestInfo compositeManifestInfo)
        {
            const ulong Timescale = 10000000;

            ulong startPosition = 0;

            SmoothStreamingManifestParser parser = new SmoothStreamingManifestParser(manifestStream);
            manifestStream.Seek(0, SeekOrigin.Begin);

            var subClipAsset = shot.Source as SubClipItem;
            var smoothStreamingVideoItem = shot.Source as SmoothStreamingVideoItem;
            var smoothStreamingAudioItem = shot.Source as SmoothStreamingAudioItem;

            // the timeline elements holds the in/out position, instead of creating an ISubClipAsset
            double markIn = shot.SourceAnchor.MarkIn.Value;
            double markOut = shot.SourceAnchor.MarkOut.Value;

            if (smoothStreamingVideoItem != null || smoothStreamingAudioItem != null)
            {
                startPosition = GetStartPositionFromManifest(parser);
            }

            if (!string.IsNullOrEmpty(shot.SequenceAudioStream))
            {
                var audioStreamsToRemove = parser.ManifestInfo.Streams.Where(s => s.StreamType.Equals("Audio", StringComparison.CurrentCultureIgnoreCase) &&
                    !s.Attributes["Name"].Equals(shot.SequenceAudioStream)).ToList();

                if (audioStreamsToRemove != null)
                {
                    foreach (var audioStream in audioStreamsToRemove)
                    {
                        parser.ManifestInfo.Streams.Remove(audioStream);
                    }
                }
            }

            if (subClipAsset != null)
            {
                if (!string.IsNullOrEmpty(subClipAsset.SequenceVideoStream))
                {
                    var videoStream = parser.ManifestInfo.Streams.First(s => s.StreamType.Equals("Video", StringComparison.CurrentCultureIgnoreCase));

                    var qualityLevelsToRemove = videoStream.QualityLevels.Where(q => !q.CustomAttributes.ContainsKey("cameraAngle") ||
                        !q.CustomAttributes["cameraAngle"].Equals(subClipAsset.SequenceVideoStream)).ToList();

                    foreach (var qualityLevel in qualityLevelsToRemove)
                    {
                        videoStream.QualityLevels.Remove(qualityLevel);
                    }
                }

                startPosition = GetStartPositionFromManifest(parser);
            }

            ulong clipBegin = (ulong)(Convert.ToUInt64(markIn * Timescale) + startPosition);
            ulong clipEnd = (ulong)(Convert.ToUInt64(markOut * Timescale) + startPosition);

            Resource resource = shot.Source.Resources.SingleOrDefault(x => !string.IsNullOrEmpty(x.Ref));

            Uri assetUri;

            Uri.TryCreate(resource.Ref, UriKind.Absolute, out assetUri);

            var customAttributes = new Dictionary<string, string> { { "CMSId", shot.Source.CMSId }, { "AzureId", shot.Source.AzureId } };
            compositeManifestInfo.AddClip(assetUri, clipBegin, clipEnd, customAttributes, parser.ManifestInfo);
        }

        private static ulong GetStartPositionFromManifest(SmoothStreamingManifestParser parser)
        {
            // The ClipBegin/ClipEnd positions must be video GOP aligned.
            ulong? maxStartTime = parser
                .ManifestInfo
                .Streams
                .Where(s => s.StreamType.Equals("video", StringComparison.CurrentCultureIgnoreCase))
                .Max(s => s.Chunks.First().Time);

            if (maxStartTime.HasValue)
            {
                return maxStartTime.Value;
            }

            return 0;
        }

        private void AddRubberBandingPoints(Shot shot, CompositeManifestInfo compositeManifestInfo)
        {
            const ulong Timescale = 10000000;

            List<VolumeLevelNode> volumeNodes = shot.VolumeNodeCollection;
            foreach (VolumeLevelNode volumeNode in volumeNodes)
            {
                double volume = volumeNode.Volume;
                long frames = (long)volumeNode.Position;
                SmpteFrameRate frameRate;
                SmpteFrameRate.TryParse(this.project.SmpteFrameRate, true, out frameRate);
                double totalSeconds = TimeCode.FromFrames(frames, frameRate).TotalSeconds + shot.TrackAnchor.MarkIn.Value;

                long ticks = (long)(totalSeconds * Timescale);

                compositeManifestInfo.AddRubberBandingPoint(ticks, volume);
            }
        }

        private void AddOverlay(Shot shot, CompositeManifestInfo compositeManifestInfo)
        {
            var overlayItem = shot.Source as OverlayItem;

            if (overlayItem != null)
            {
                var dict = new Dictionary<string, object>();
                overlayItem.Metadata.ForEach(mf => dict.Add(mf.Name, mf.Value));

                const double Timescale = 10000000.0;

                compositeManifestInfo.AddOverlay(
                    overlayItem.Title,
                    overlayItem.X,
                    overlayItem.Y,
                    overlayItem.Height,
                    overlayItem.Width,
                    dict,
                    overlayItem.XamlTemplate,
                    (ulong)((shot.SourceAnchor.MarkIn + shot.TrackAnchor.MarkIn) * Timescale),
                    (ulong)(((shot.SourceAnchor.MarkIn + shot.TrackAnchor.MarkIn) + (shot.SourceAnchor.MarkOut - shot.SourceAnchor.MarkIn)) * Timescale));
            }
        }

        private void AddPreviousGap(double gapDuration, Stream gapManifestStream, string cmsId, string azureId, CompositeManifestInfo compositeManifestInfo)
        {
            if (gapDuration == 0)
            {
                return;
            }

            const double Timescale = 10000000.0;

            SmoothStreamingManifestParser parser = new SmoothStreamingManifestParser(gapManifestStream);
            gapManifestStream.Seek(0, SeekOrigin.Begin);

            double gapVideoCount = gapDuration / (parser.ManifestInfo.ManifestDuration / Timescale);

            int completeVideos = (int)gapVideoCount;
            double partialVideoProportion = gapVideoCount - (int)gapVideoCount;

            ulong clipEnd = parser.ManifestInfo.ManifestDuration;
            var customAttributes = new Dictionary<string, string> { { "CMSId", cmsId }, { "AzureId", azureId } };

            for (int i = 0; i < completeVideos; i++)
            {
                compositeManifestInfo.AddClip(this.gapUri, 0, clipEnd, customAttributes, true, parser.ManifestInfo);
            }

            clipEnd = (ulong)(clipEnd * partialVideoProportion);

            if (clipEnd != 0)
            {
                compositeManifestInfo.AddClip(this.gapUri, 0, clipEnd, customAttributes, true, parser.ManifestInfo);
            }
        }

        private IEnumerable<string> GetManifests(string pbpDataStreamName, string adsDataStreamName, bool compressManifest, string gapUriString, string gapCmsId, string gapAzureId, int sequenceNumber)
        {
            List<string> manifests = new List<string>();
            this.gapUri = new Uri(gapUriString);
            DownloaderManager manager = new DownloaderManager();

            string manifest = string.Empty;

            if (this.project.Sequences != null && this.project.Sequences.Count >= 1)
            {
                Stream gapStream = manager.DownloadManifest(this.gapUri, true);
                byte[] gapResult = null;

                using (BinaryReader reader = new BinaryReader(gapStream))
                {
                    gapResult = reader.ReadBytes((int)manager.LastResponseLength);
                }

                MemoryStream gapMemoryStream = null;

                try
                {
                    gapMemoryStream = new MemoryStream(gapResult);
                    Sequence sequence = this.project.Sequences[0];
                    if (this.project.Sequences.Count > sequenceNumber)
                    {
                        sequence = this.project.Sequences[sequenceNumber];
                    }

                    foreach (
                        var track in
                            sequence.Tracks.Where(
                                t =>
                                (t.TrackType.Equals("visual", StringComparison.InvariantCultureIgnoreCase)
                                 || t.TrackType.Equals("audio", StringComparison.InvariantCultureIgnoreCase))
                                && t.Shots.Count > 0))
                    {
                        CompositeManifestInfo compositeManifestInfo = new CompositeManifestInfo(2, 0);
                        compositeManifestInfo.PlayByPlayDataStreamName = pbpDataStreamName;
                        compositeManifestInfo.AdsDataStreamName = adsDataStreamName;
                        compositeManifestInfo.RubberBandingDataStreamName = "RubberBanding";
                        compositeManifestInfo.TransitionDataStreamName = "Transition";

                        IDictionary<Shot, double> gapDurations = CalculateGapsDuration(track);
                        foreach (Shot shot in track.Shots)
                        {
                            Resource resource = shot.Source.Resources.SingleOrDefault(x => !string.IsNullOrEmpty(x.Ref));

                            Uri assetUri;

                            if (resource != null && Uri.TryCreate(resource.Ref, UriKind.Absolute, out assetUri))
                            {
                                Stream manifestStream = manager.DownloadManifest(assetUri, true);

                                MemoryStream stream = new MemoryStream();

                                if (manifestStream != null)
                                {
                                    byte[] buffer = ReadFully(manifestStream);
                                    if (buffer != null)
                                    {
                                        var binaryWriter = new BinaryWriter(stream);
                                        binaryWriter.Write(buffer);
                                    }

                                    stream.Seek(0, SeekOrigin.Begin);
                                }

                                this.AddPreviousGap(gapDurations[shot], gapMemoryStream, gapCmsId, gapAzureId, compositeManifestInfo);
                                AddClipToCompositeManifestInfo(shot, stream, compositeManifestInfo);
                                this.AddRubberBandingPoints(shot, compositeManifestInfo);
                                this.AddTransitions(shot, compositeManifestInfo);

                                stream.Close();

                                if (track.TrackType.Equals("visual", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    compositeManifestInfo.OverlayDataStreamName = "Overlay";
                                    var overlaysTrack = sequence.Tracks.First(t => t.TrackType.Equals("Overlay"));

                                    foreach (Shot s in overlaysTrack.Shots)
                                    {
                                        this.AddOverlay(s, compositeManifestInfo);
                                    }

                                    if (sequence.AdOpportunities != null)
                                    {
                                        foreach (
                                            RCE.Services.Contracts.AdOpportunity adOpportunity in
                                                sequence.AdOpportunities)
                                        {
                                            compositeManifestInfo.AddAdOpportunity(
                                                adOpportunity.ID, adOpportunity.TemplateType, adOpportunity.Time);
                                        }
                                    }

                                    if (sequence.MarkerCollection != null)
                                    {
                                        foreach (Marker marker in sequence.MarkerCollection)
                                        {
                                            compositeManifestInfo.AddPlayByPlay(marker.ID, marker.Text, marker.Time);
                                        }
                                    }
                                }
                            }
                        }

                        SmoothStreamingManifestWriter writer = new SmoothStreamingManifestWriter();
                        manifest = writer.GenerateCompositeManifest(compositeManifestInfo, false, compressManifest);
                        manifests.Add(manifest);
                    }
                }
                finally
                {
                    if (gapMemoryStream != null)
                    {
                        gapMemoryStream.Close();
                    }
                }
            }

            return manifests;
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;

                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }

        private void AddTransitions(Shot shot, CompositeManifestInfo compositeManifestInfo)
        {
            const double Timescale = 10000000.0;
            SubClipItem subclipItem = shot.Source as SubClipItem;
            Item itemForTransition = subclipItem != null ? subclipItem.RelatedItem : shot.Source;

            if (!(itemForTransition is VideoItem) && !(itemForTransition is AudioItem))
            {
                return;
            }

            AssetType assetType = (itemForTransition is VideoItem) ? AssetType.Video : AssetType.Audio;

            ulong position = (ulong)(shot.TrackAnchor.MarkIn * Timescale);

            compositeManifestInfo.AddTransition(SmoothStreamingManifestGenerator.Models.TransitionType.FadeIn, assetType, position, shot.InTransition.Duration);

            position = (ulong)((shot.TrackAnchor.MarkIn + (shot.SourceAnchor.MarkOut - shot.SourceAnchor.MarkIn) - shot.OutTransition.Duration) * Timescale);
            compositeManifestInfo.AddTransition(SmoothStreamingManifestGenerator.Models.TransitionType.FadeOut, assetType, position, shot.OutTransition.Duration);
        }

        private static void SaveImage(Sequence sequence, string outputFile)
        {
            if (!string.IsNullOrWhiteSpace(sequence.SequenceThumbnail))
            {
                byte[] imageBytes = Convert.FromBase64String(sequence.SequenceThumbnail);
                MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

                ms.Write(imageBytes, 0, imageBytes.Length);
                Image image = Image.FromStream(ms);

                // Save the thumbnail to the output file.
                image.Save(outputFile, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
