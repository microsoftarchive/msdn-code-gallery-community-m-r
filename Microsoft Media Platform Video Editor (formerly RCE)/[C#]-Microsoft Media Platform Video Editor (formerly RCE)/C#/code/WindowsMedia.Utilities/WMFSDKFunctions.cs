// *****************************************************************************
//
// Microsoft Windows Media
// Copyright ( C) Microsoft Corporation. All rights reserved.
//
// FileName:            WMFSDKFunction.cs
//
// Abstract:            Wrapper used by managed-code samples.
//
// *****************************************************************************

namespace WMFSDKWrapper
{
    using System;
    using System.CodeDom.Compiler;
    using System.Runtime.InteropServices;
    using System.Runtime.InteropServices.ComTypes;
    using System.Text;

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum WMT_ATTR_DATATYPE
    {
        WMT_TYPE_DWORD = 0,
        WMT_TYPE_STRING = 1,
        WMT_TYPE_BINARY = 2,
        WMT_TYPE_BOOL = 3,
        WMT_TYPE_QWORD = 4,
        WMT_TYPE_WORD = 5,
        WMT_TYPE_GUID = 6,
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum WMT_CODEC_INFO_TYPE
    {
        WMT_CODECINFO_AUDIO = 0,
        WMT_CODECINFO_VIDEO = 1,
        WMT_CODECINFO_UNKNOWN = 0xffffff
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum FILE_ACCESS : uint
    {
        GENERIC_READ = 0x80000000,
        GENERIC_WRITE = 0x40000000,
        GENERIC_EXECUTE = 0x20000000,
        GENERIC_ALL = 0x10000000,
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum FILE_SHARE : uint
    {
        FILE_SHARE_NONE = 0x00000000,
        FILE_SHARE_READ = 0x00000001,
        FILE_SHARE_WRITE = 0x00000002,
        FILE_SHARE_DELETE = 0x00000004,
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum WinErrorCode : uint
    {
        S_OK = 0,
        NS_E_NO_MORE_SAMPLES = 0xC00D0BCFU
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum WMFlags
    {
        WM_SF_CLEANPOINT = 0x1,
        WM_SF_DISCONTINUITY = 0x2,
        WM_SF_DATALOSS = 0x4
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum WMT_STREAM_SELECTION
    {
        WMT_OFF = 0,
        WMT_CLEANPOINT_ONLY = 1,
        WMT_ON = 2
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public enum WMT_RIGHTS
    {
        WMT_RIGHT_PLAYBACK = 0x1,
        
        WMT_RIGHT_COPY_TO_NON_SDMI_DEVICE = 0x2,
        
        WMT_RIGHT_COPY_TO_CD = 0x8,
   
        WMT_RIGHT_COPY_TO_SDMI_DEVICE = 0x10,
        
        WMT_RIGHT_ONE_TIME = 0x20,
        
        WMT_RIGHT_SAVE_STREAM_PROTECTED = 0x40,
        
        WMT_RIGHT_COPY = 0x80,
        
        WMT_RIGHT_COLLABORATIVE_PLAY = 0x100,
        
        WMT_RIGHT_SDMI_TRIGGER = 0x10000,
        
        WMT_RIGHT_SDMI_NOMORECOPIES = 0x20000
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [Guid("96406BD9-2B2B-11d3-B36B-00C04F6108FF"), 
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMetadataEditor
    {
        uint Open([In, MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);

        uint Close();
        
        uint Flush();
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [Guid("203cffe3-2e18-4fdf-b59d-6e71530534cf"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMMetadataEditor2 : IWMMetadataEditor
    {
        uint OpenEx(
            [In, MarshalAs(UnmanagedType.LPWStr)]   string pwszFilename, 
            [In, MarshalAs(UnmanagedType.U4)]       FILE_ACCESS dwDesiredAccess, 
            [In, MarshalAs(UnmanagedType.U4)]       FILE_SHARE  dwShareMode);
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [Guid("15CC68E3-27CC-4ecd-B222-3F5D02D80BD5"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMHeaderInfo3
    {
        uint GetAttributeCount(
            [In]                                    ushort wStreamNum,
            [Out]                                   out ushort pcAttributes);

        uint GetAttributeByIndex(
            [In]                                    ushort wIndex,
            [Out, In]                               ref ushort pwStreamNum,
            [Out, MarshalAs(UnmanagedType.LPWStr)]  StringBuilder pwszName,
            [Out, In]                               ref ushort pcchNameLen,
            [Out]                                   out WMT_ATTR_DATATYPE pType,
            [Out, MarshalAs(UnmanagedType.LPArray)] byte[] pValue,
            [Out, In]                               ref ushort pcbLength);

        uint GetAttributeByName(
            [Out, In]								ref ushort pwStreamNum,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [Out]									out WMT_ATTR_DATATYPE pType,
            [Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [Out, In]								ref ushort pcbLength);

        uint SetAttribute(
            [In]									ushort wStreamNum,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [In]									WMT_ATTR_DATATYPE Type,
            [In, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [In]									ushort cbLength);

        uint GetMarkerCount(
            [Out]									out ushort pcMarkers);

        uint GetMarker(
            [In]									ushort wIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszMarkerName,
            [Out, In]								ref ushort pcchMarkerNameLen,
            [Out]									out ulong pcnsMarkerTime);

        uint AddMarker(
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszMarkerName,
            [In]									ulong cnsMarkerTime);

        uint RemoveMarker(
            [In]									ushort wIndex);

        uint GetScriptCount(
            [Out]									out ushort pcScripts);

        uint GetScript(
            [In]									ushort wIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszType,
            [Out, In]								ref ushort pcchTypeLen,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszCommand,
            [Out, In]								ref ushort pcchCommandLen,
            [Out]									out ulong pcnsScriptTime);

        uint AddScript(
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszType,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszCommand,
            [In]									ulong cnsScriptTime);

        uint RemoveScript(
            [In]									ushort wIndex);

        uint GetCodecInfoCount(
            [Out]									out uint pcCodecInfos);

        uint GetCodecInfo(
            [In]									uint wIndex,
            [Out, In]								ref ushort pcchName,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszName,
            [Out, In]								ref ushort pcchDescription,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	string pwszDescription,
            [Out]									out WMT_CODEC_INFO_TYPE pCodecType,
            [Out, In]								ref ushort pcbCodecInfo,
            [Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pbCodecInfo);

        uint GetAttributeCountEx(
            [In]									ushort wStreamNum,
            [Out]									out ushort pcAttributes);

        uint GetAttributeIndices(
            [In]									ushort wStreamNum,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszName,
            [In]									ref ushort pwLangIndex,
            [Out, MarshalAs(UnmanagedType.LPArray)] ushort[] pwIndices,
            [Out, In]								ref ushort pwCount);

        uint GetAttributeByIndexEx(
            [In]									ushort wStreamNum,
            [In]									ushort wIndex,
            [Out, MarshalAs(UnmanagedType.LPWStr)]	StringBuilder pwszName,
            [Out, In]								ref ushort pwNameLen,
            [Out]									out WMT_ATTR_DATATYPE pType,
            [Out]									out ushort pwLangIndex,
            [Out, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [Out, In]								ref uint pdwDataLength);

        uint ModifyAttribute(
            [In]									ushort wStreamNum,
            [In]									ushort wIndex,
            [In]									WMT_ATTR_DATATYPE Type,
            [In]									ushort wLangIndex,
            [In, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [In]									uint dwLength);

        uint AddAttribute(
            [In]									ushort wStreamNum,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [Out]									out ushort pwIndex,
            [In]									WMT_ATTR_DATATYPE Type,
            [In]									ushort wLangIndex,
            [In, MarshalAs(UnmanagedType.LPArray)]	byte[] pValue,
            [In]									uint dwLength);

        uint DeleteAttribute(
            [In]									ushort wStreamNum,
            [In]									ushort wIndex);

        uint AddCodecInfo(
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pszName,
            [In, MarshalAs(UnmanagedType.LPWStr)]	string pwszDescription,
            [In]									WMT_CODEC_INFO_TYPE codecType,
            [In]									ushort cbCodecInfo,
            [In, MarshalAs(UnmanagedType.LPArray)]	byte[] pbCodecInfo);
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [Guid("9397F121-7705-4dc9-B049-98B698188414"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMSyncReader
    {
        [PreserveSig]
        uint Open([MarshalAs(UnmanagedType.LPWStr)] string pwszFilename);

        [PreserveSig]
        uint Close();

        [PreserveSig]
        uint SetRange(
            /* [in] */ long cnsStartTime,
            /* [in] */ long cnsDuration);

        [PreserveSig]
        uint SetRangeByFrame(
            /* [in] */ short wStreamNum,
            /* [in] */ long qwFrameNumber,
            /* [in] */ long cFramesToRead);

        [PreserveSig]
        WinErrorCode GetNextSample(
            /* [in] */ short wStreamNum,
            /* [out] */ [Out, MarshalAs(UnmanagedType.Interface)] out INSSBuffer ppSample,
            /* [out] */ ref long pcnsSampleTime,
            /* [out] */ ref long pcnsDuration,
            /* [out] */ ref WMFlags pdwFlags,
            /* [out] */ ref int pdwOutputNum,
            /* [out] */ ref short pwStreamNum);

        [PreserveSig]
        WinErrorCode SetStreamsSelected(
            /* [in] */ short cStreamCount,
            /* [in] */ out short pwStreamNumbers,
            /* [in] */ WMT_STREAM_SELECTION[] pSelections);

        [PreserveSig]
        WinErrorCode GetStreamSelected(
            /* [in] */ short wStreamNum,
            /* [out] */ WMT_STREAM_SELECTION[] pSelection);

        [PreserveSig]
        WinErrorCode SetReadStreamSamples(
            /* [in] */ short wStreamNum,
            /* [in] */ int fCompressed);

        [PreserveSig]
        WinErrorCode GetReadStreamSamples(
            /* [in] */ short wStreamNum,
            /* [out] */ out int pfCompressed);

        [PreserveSig]
        WinErrorCode GetOutputSetting(
            /* [in] */ int dwOutputNum,
            /* [in] */ [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            /* [out] */ out WMT_ATTR_DATATYPE pType,
            /* [size_is][out] */ out byte pValue,
            /* [out][in] */ out short pcbLength);

        [PreserveSig]
        WinErrorCode SetOutputSetting(
            /* [in] */ int dwOutputNum,
            /* [in] */ [MarshalAs(UnmanagedType.LPWStr)] string pszName,
            /* [in] */ WMT_ATTR_DATATYPE Type,
            /* [size_is][in] */ byte[] pValue,
            /* [in] */ short cbLength);

        [PreserveSig]
        WinErrorCode GetOutputCount(
            /* [out] */ out int pcOutputs);

        [PreserveSig]
        WinErrorCode GetOutputProps(
            /* [in] */ uint dwOutputNum,
            /* [out] */ [Out, MarshalAs(UnmanagedType.Interface)] out IWMOutputMediaProps ppOutput);

        [PreserveSig]
        WinErrorCode SetOutputProps(
            /* [in] */ int dwOutputNum,
            /* [in] */ [MarshalAs(UnmanagedType.Interface)] IWMOutputMediaProps pOutput);

        [PreserveSig]
        WinErrorCode GetOutputFormatCount(
            /* [in] */ int dwOutputNum,
            /* [out] */out int pcFormats);

        [PreserveSig]
        WinErrorCode GetOutputFormat(
            /* [in] */ int dwOutputNum,
            /* [in] */ int dwFormatNum,
            /* [out] */ [Out, MarshalAs(UnmanagedType.Interface)] IWMOutputMediaProps ppProps);

        [PreserveSig]
        WinErrorCode GetOutputNumberForStream(
            /* [in] */ short wStreamNum,
            /* [out] */out int pdwOutputNum);

        [PreserveSig]
        WinErrorCode GetStreamNumberForOutput(
            /* [in] */ uint dwOutputNum,
            /* [out] */out ushort pwStreamNum);

        [PreserveSig]
        WinErrorCode GetMaxOutputSampleSize(
            /* [in] */ int dwOutput,
            /* [out] */ out int pcbMax);

        [PreserveSig]
        WinErrorCode GetMaxStreamSampleSize(
            /* [in] */ short wStream,
            /* [out] */ out int pcbMax);

        [PreserveSig]
        WinErrorCode OpenStream(
            /* [in] */ IStream pStream);
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [Guid("96406BD7-2B2B-11d3-B36B-00C04F6108FF"),
    InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IWMOutputMediaProps
    {
        [PreserveSig]
        WinErrorCode GetType(
            /* [out] */ out Guid pguidType);

        [PreserveSig]
        WinErrorCode GetMediaType(
            /* [out] */ IntPtr pType,
            /* [out][in] */ ref int pcbType);

        [PreserveSig]
        WinErrorCode SetMediaType(
            /* [in] */ WM_MEDIA_TYPE pType);

        [PreserveSig]
        WinErrorCode GetStreamGroupName(
            /* [size_is][out] */ [Out, MarshalAs(UnmanagedType.LPWStr)] out string pwszName,
            /* [out][in] */ ref int pcchName);

        [PreserveSig]
        WinErrorCode GetConnectionName(
            /* [size_is][out] */ [Out, MarshalAs(UnmanagedType.LPWStr)] out string pwszName,
            /* [out][in] */ ref int pcchName);
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [Guid("E1CD3524-03D7-11d2-9EED-006097D2D7CF"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface INSSBuffer
    {
        [PreserveSig]
        WinErrorCode GetLength(
            /* [out] */ out int pdwLength);

        [PreserveSig]
        WinErrorCode SetLength(
            /* [in] */ int dwLength);

        [PreserveSig]
        WinErrorCode GetMaxLength(
            /* [out] */ out int pdwLength);

        [PreserveSig]
        WinErrorCode GetBuffer(
            /* [out] */ out IntPtr ppdwBuffer);

        [PreserveSig]
        WinErrorCode GetBufferAndLength(
            /* [out] */ out IntPtr ppdwBuffer,
            /* [out] */ out int pdwLength);
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [ComImport, Guid("F369E2F0-E081-4FE6-8450-B810B2F410D1"),
InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IWMReaderTimecode
    {
        /// <summary>
        /// Retrieves the total number of SMPTE time code ranges for a specified stream.
        /// </summary>
        WinErrorCode GetTimecodeRangeCount(ushort wStreamNum, out ushort rangeCount);

        /// <summary>
        /// Retrieves the starting and ending time codes for a specified SMPTE time code range.
        /// </summary>
        /// <returns></returns>
        WinErrorCode GetTimecodeRangeBounds(
            /* [in] */ [In]ushort wStreamNum,
            /* [in] */ [In]uint wRangeNum,
            /* [out] */ out uint pdwStartTimecode,
            /* [out] */ out uint pdwEndTimecode
                );

    };

    [GeneratedCode("BypassFxCop", "1.0")]
    [StructLayout(LayoutKind.Sequential)]
    public class WM_MEDIA_TYPE
    {
        public Guid majortype;
        
        public Guid subtype;
        
        public int bFixedSizeSamples;
        
        public int bTemporalCompression;
        
        public int lSampleSize;
        
        public Guid formattype;
        
        public IntPtr pUnk;
        
        public int cbFormat;
        
        public IntPtr pbFormat;
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [StructLayout(LayoutKind.Sequential)]
    public class WMVIDEOINFOHEADER
    {
        public RECT rcSource;
        public RECT rcTarget;
        public int dwBitRate;
        public int dwBitErrorRate;
        public long AvgTimePerFrame;
        public BITMAPINFOHEADER bmiHeader;
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left, top, right, bottom;
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct BITMAPINFOHEADER
    {
        public int biSize;

        public int biWidth;
        
        public int biHeight;
        
        public short biPlanes;
        
        public short biBitCount;
        
        public int biCompression;
        
        public int biSizeImage;
        
        public int biXPelsPerMeter;
        
        public int biYPelsPerMeter;
        
        public int biClrUsed;
        
        public int biClrImportant;
    }

    [GeneratedCode("BypassFxCop", "1.0")]
    public class WMFSDKFunctions
    {
        public static Guid WMMEDIATYPE_Video = new Guid("73646976-0000-0010-8000-00AA00389B71");

        public WMFSDKFunctions()
        {
        }

        [DllImport("WMVCore.dll", 
            EntryPoint = "WMCreateEditor", 
            SetLastError = true,
            CharSet = CharSet.Unicode, 
            ExactSpelling = true,
            CallingConvention = CallingConvention.StdCall)]
        public static extern uint WMCreateEditor(
            [Out, MarshalAs(UnmanagedType.Interface)] out IWMMetadataEditor2 ppMetadataEditor);

        [DllImport("WMVCore.dll", EntryPoint = "WMCreateSyncReader", SetLastError = true,
                CharSet = CharSet.Unicode, ExactSpelling = true,
                CallingConvention = CallingConvention.StdCall)]
        public static extern WinErrorCode WMCreateSyncReader(IntPtr pUnkCert, WMT_RIGHTS dwRights, [Out, MarshalAs(UnmanagedType.Interface)] out IWMSyncReader ppSyncReader);
    }
}
