// <copyright file="ChunkExtensions.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: ChunkExtensions.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.SilverlightMediaFramework.Plugins.Primitives;

    public static class MediaPluginExtensions
    {
        public static IEnumerable<Tuple<double, double>> FindChunks(this IEnumerable<IDataChunk> chunks, Func<double, double, bool> condition)
        {
            var chunksList = chunks.ToArray();
            var resultChunks = new List<Tuple<double, double>>();
            double prevD = 0;
            double prevT = 0;

            for (int i = 0; i < chunksList.Length; i++)
            {
                var current = chunksList[i];
                var t = current.Timestamp.TotalSeconds;
                var d = current.Duration.TotalSeconds;

                // infer T from previous chunk
                if (t == 0 && i > 0)
                {
                    t = prevT + prevD;
                }

                // infer D from next chunk
                if (d == 0 && i < chunksList.Length - 1)
                {
                    if (chunksList[i + 1].Timestamp.TotalSeconds > 0)
                    {
                        d = chunksList[i + 1].Timestamp.TotalSeconds - t;
                    }
                    else
                    {
                        throw new Exception("Cannot infer duration! The next chunk does not have a time value.");
                    }
                }

                if (condition(t, d))
                {
                    resultChunks.Add(new Tuple<double, double>(t, d));
                }

                prevD = current.Duration.TotalSeconds;
                prevT = current.Timestamp.TotalSeconds;
            }

            return resultChunks;
        }

        public static TimeSpan GetStartOffset(this IMediaStream stream)
        {
            var firstChunk = stream.DataChunks.First();

            return firstChunk.Timestamp;
        }

        public static TimeSpan GetVideoStreamStartOffset(this IRCESmoothStreamingMediaPlugin mediaPlugin)
        {
            var currentSegment = mediaPlugin.CurrentSegment;
            if (currentSegment != null)
            {
                var firstVideoStream = currentSegment.SelectedStreams.Where(s => s.Type == StreamType.Video).FirstOrDefault();
                if (firstVideoStream != null)
                {
                    return firstVideoStream.GetStartOffset();
                }
            }

            return mediaPlugin.StartPosition;
        }
    }
}
