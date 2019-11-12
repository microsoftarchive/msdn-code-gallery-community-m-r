//----------------------------------------------------------------------------------------------------------------------------
// <copyright file="DataPacket.cs" company="Microsoft Corporation">
//  Copyright 2011 Microsoft Corporation
// </copyright>
// Licensed under the MICROSOFT LIMITED PUBLIC LICENSE version 1.1 (the "License"); 
// You may not use this file except in compliance with the License. 
//---------------------------------------------------------------------------------------------------------------------------
namespace SLBlobUploader.Code.Infrastructure
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Data packet to be transmitted.
    /// </summary>
    public class DataPacket
    {
        /// <summary>
        /// Size of each packet to transmit.
        /// </summary>
        private int packetSize = Constants.ChunkSize;

        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        /// <value>The payload.</value>
        private byte[] payload = null;

        /// <summary>
        /// Gets or sets the is transported.
        /// </summary>
        /// <value>The is transported.</value>
        public bool? IsTransported
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the serial number.
        /// </summary>
        /// <value>The serial number.</value>
        public string SerialNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        /// <value>The retry count.</value>
        public int RetryCount
        {
            get;
            set;
        }

        /// <summary>
        /// Sets the payload.
        /// </summary>
        /// <param name="data">The byte data to be added to payload.</param>
        public void SetPayload(byte[] data)
        {
            this.payload = data;
        }

        /// <summary>
        /// Gets the payload.
        /// </summary>
        /// <returns>Byte array of payload</returns>
        public byte[] GetPayload()
        {
            return this.payload;
        }

        /// <summary>
        /// Transforms the stream to packets.
        /// </summary>
        /// <param name="sourceStream">The source stream.</param>
        /// <returns>List of packets split out from stream</returns>
        public Collection<DataPacket> TransformStreamToPackets(Stream sourceStream)
        {
            if (sourceStream != null && sourceStream.CanRead)
            {
                int bytesToRead = 0;
                int serialNumber = 1;
                byte[] buffer = new byte[this.packetSize];
                var dataBlocks = new Collection<DataPacket>();
                while ((bytesToRead = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    var payloadArray = new byte[bytesToRead];
                    Array.Copy(buffer, payloadArray, bytesToRead);
                    var dataPacket = new DataPacket()
                    {
                        IsTransported = false,
                        RetryCount = 0,
                        SerialNumber = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format(CultureInfo.InvariantCulture, "{0:D4}", serialNumber++)))
                    };
                    dataPacket.SetPayload(payloadArray);
                    dataBlocks.Add(dataPacket);
                }

                return dataBlocks;
            }

            return null;
        }
    }
}
