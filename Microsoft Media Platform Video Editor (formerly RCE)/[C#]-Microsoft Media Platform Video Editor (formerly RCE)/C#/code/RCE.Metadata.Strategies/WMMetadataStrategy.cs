// <copyright file="WMMetadataStrategy.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: WMMetadataStrategy.cs                     
//
// ===============================================================================
// Copyright 2010 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

namespace RCE.Metadata.Strategies
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Contracts;
    using Services.Contracts;
    using WMFSDKWrapper;

    public class WMMetadataStrategy : IMetadataStrategy
    {
        private const ushort StreamNumber = 0xFFFF;

        public bool CanRetrieveMetadata(object target)
        {
            if (target == null || !(target is string))
            {
                return false;
            }

            string filename = target.ToString();

            string extension = Path.GetExtension(filename).ToUpperInvariant();

            return extension == ".WMV" || extension == ".WMA" || extension == ".MP3";
        }

        public Metadata GetMetadata(object target)
        {
            if (target == null || !(target is string))
            {
                return null;
            }

            string filename = target.ToString();

            IWMMetadataEditor2 metadataEditor = null;

            IWMSyncReader syncReader = null;

            WMMetadata metadata = new WMMetadata();

            try
            {
                WMFSDKFunctions.WMCreateEditor(out metadataEditor);
                metadataEditor.OpenEx(filename, FILE_ACCESS.GENERIC_READ, FILE_SHARE.FILE_SHARE_NONE);

                IWMHeaderInfo3 header = (IWMHeaderInfo3)metadataEditor;

                ushort attributeCount;
                header.GetAttributeCountEx(StreamNumber, out attributeCount);

                for (int i = 0; i < attributeCount; i++)
                {
                    MetadataField metadataField = GetAttributeByIndex(header, i);

                    metadata.AddMetadataField(metadataField);
                }
            }
            catch (COMException ex)
            {
                // TODO: Logging
            }
            finally
            {
                if (metadataEditor != null)
                {
                    metadataEditor.Close();
                    Marshal.FinalReleaseComObject(metadataEditor);
                    metadataEditor = null;
                }
            }

            try
            {
                WMFSDKFunctions.WMCreateSyncReader(IntPtr.Zero, WMT_RIGHTS.WMT_RIGHT_PLAYBACK, out syncReader);
                syncReader.Open(filename);

                int outputCount;
                syncReader.GetOutputCount(out outputCount);

                IWMOutputMediaProps outputMediaProps = null;

                for (uint i = 0; i < outputCount; i++)
                {
                    IWMOutputMediaProps innerOutputMediaProps;
                    syncReader.GetOutputProps(i, out innerOutputMediaProps);

                    Guid type;
                    innerOutputMediaProps.GetType(out type);

                    if (type == WMFSDKFunctions.WMMEDIATYPE_Video)
                    {
                        outputMediaProps = innerOutputMediaProps;
                        break;
                    }
                }

                if (outputMediaProps != null)
                {
                    int pcbType = 0;
                    outputMediaProps.GetMediaType(IntPtr.Zero, ref pcbType);

                    IntPtr mediaTypeBufferPtr = Marshal.AllocHGlobal(pcbType);

                    outputMediaProps.GetMediaType(mediaTypeBufferPtr, ref pcbType);

                    WM_MEDIA_TYPE mediaType = new WM_MEDIA_TYPE();
                    WMVIDEOINFOHEADER videoInfoHeader = new WMVIDEOINFOHEADER();
                    Marshal.PtrToStructure(mediaTypeBufferPtr, mediaType);

                    Marshal.FreeHGlobal(mediaTypeBufferPtr);
                    Marshal.PtrToStructure(mediaType.pbFormat, videoInfoHeader);

                    double frameRate = Math.Round((double)10000000 / videoInfoHeader.AvgTimePerFrame, 2);

                    metadata.AddMetadataField(new MetadataField("FrameRate", frameRate));
                }
            }
            catch (COMException ex)
            {
                // TODO: Logging
            }
            finally
            {
                if (syncReader != null)
                {
                    syncReader.Close();
                    Marshal.FinalReleaseComObject(syncReader);
                    syncReader = null;
                }
            }

            return metadata;
        }

        private static object ParseAttributeValue(byte[] attributeValue, WMT_ATTR_DATATYPE attributeDataType)
        {
            switch (attributeDataType)
            {
                case WMT_ATTR_DATATYPE.WMT_TYPE_BINARY:
                    return attributeValue;

                case WMT_ATTR_DATATYPE.WMT_TYPE_BOOL:
                    return BitConverter.ToBoolean(attributeValue, 0);

                case WMT_ATTR_DATATYPE.WMT_TYPE_DWORD:
                    return BitConverter.ToUInt32(attributeValue, 0);

                case WMT_ATTR_DATATYPE.WMT_TYPE_QWORD:
                    return BitConverter.ToUInt64(attributeValue, 0);

                case WMT_ATTR_DATATYPE.WMT_TYPE_WORD:
                    return BitConverter.ToUInt16(attributeValue, 0);

                case WMT_ATTR_DATATYPE.WMT_TYPE_GUID:
                    return BitConverter.ToString(attributeValue, 0, attributeValue.Length);

                case WMT_ATTR_DATATYPE.WMT_TYPE_STRING:

                    if (attributeValue.Length < 2)
                    {
                        return null;
                    }

                    StringBuilder sb = new StringBuilder(attributeValue.Length / 2);

                    for (int i = 0; i < attributeValue.Length - 2; i += 2)
                    {
                        sb.Append(BitConverter.ToChar(attributeValue, i));
                    }

                    return sb.ToString();

                default:
                    return null;
            }
        }

        private static MetadataField GetAttributeByIndex(IWMHeaderInfo3 header, int index)
        {
            StringBuilder attributeName = null;
            ushort attributeNameLength = 0;
            WMT_ATTR_DATATYPE attributeDataType;
            ushort langIndex = 0;
            byte[] attributeValue = null;
            uint attributeValueLength = 0;

            header.GetAttributeByIndexEx(
                StreamNumber,
                (ushort)index,
                attributeName,
                ref attributeNameLength,
                out attributeDataType,
                out langIndex,
                attributeValue,
                ref attributeValueLength);

            attributeName = new StringBuilder(attributeNameLength, attributeNameLength);
            attributeValue = new byte[attributeValueLength];

            header.GetAttributeByIndexEx(
                StreamNumber,
                (ushort)index,
                attributeName,
                ref attributeNameLength,
                out attributeDataType,
                out langIndex,
                attributeValue,
                ref attributeValueLength);

            object value = ParseAttributeValue(attributeValue, attributeDataType);

            MetadataField field = new MetadataField(attributeName.ToString(), value);

            return field;
        }
    }
}