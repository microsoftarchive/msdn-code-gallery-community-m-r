////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation.  All rights reserved.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Gadgeteer
{
    /// <summary>
    /// This abstract class allows the Gadgeteer libraries to access the functionality provided by mainboards.  Every Gadgeteer program uses a single mainboard accessed by Program.Mainboard.  
    /// </summary>
    public abstract class Mainboard
    {
        /// <summary>
        /// Allows mainboards to support storage device mounting/umounting.  This provides modules with a list of storage device volume names supported by the mainboard. 
        /// </summary>
        public abstract string[] GetStorageDeviceVolumeNames();

        /// <summary>
        /// Functionality provided by mainboard to mount storage devices, given the volume name of the storage device (see <see cref="GetStorageDeviceVolumeNames"/>).
        /// This should result in a <see cref="Microsoft.SPOT.IO.RemovableMedia.Insert"/> event if successful.
        /// </summary>
        public abstract bool MountStorageDevice(string volumeName);

        /// <summary>
        /// Functionality provided by mainboard to ummount storage devices, given the volume name of the storage device (see <see cref="GetStorageDeviceVolumeNames"/>).
        /// This should result in a <see cref="Microsoft.SPOT.IO.RemovableMedia.Eject"/> event if successful.
        /// </summary>
        public abstract bool UnmountStorageDevice(string volumeName);

        /// <summary>
        /// Sets the programming interface that will be used to program the mainboard.
        /// </summary>
        /// <param name="programmingInterface"></param>
        public abstract void SetProgrammingMode(ProgrammingInterface programmingInterface);

        /// <summary>
        /// Sets the LCD controller configuration to use.
        /// </summary>
        /// <remarks>
        /// Should throw an ArgumentException if there is no LCD functionality on this mainboard, except if Mainboard_LCDConfiguration.HeadlessConfig (or null) is provided.
        /// </remarks>
        /// <param name="lcdConfig">the LCD configuration to use</param>
        public abstract void SetLCDConfiguration(LCDConfiguration lcdConfig);

        /// <summary>
        /// Configures rotation in the LCD controller.
        /// </summary>
        /// <param name="rotation">The LCD rotation to use</param>
        /// <returns>true if the rotation is supported</returns>
        public abstract bool SetLCDRotation(Gadgeteer.Modules.Module.DisplayModule.LCDRotation rotation);

        /// <summary>
        /// A delegate for serial peripheral interface (SPI) display support, which marshals an RGB byte array into a target byte array, e.g. encoded as 16-bit color.
        /// </summary>
        /// <param name="bitmapBytes">The array of bitmap bytes.</param>
        /// <param name="pixelBytes">The array of video bytes, which should be sized accordingly.</param>
        /// <param name="bpp">The encoding to use.</param>
        public delegate void BitmapConvertBPP(byte[] bitmapBytes, byte[] pixelBytes, BPP bpp);

        /// <summary>
        /// Native bitmap conversion functionality provided by the mainboard.  Null if not available on this mainboard.
        /// </summary>
        public BitmapConvertBPP NativeBitmapConverter = null;

        /// <summary>
        /// A delegate for serial peripheral interface (SPI) display support, which copies Bitmap data to an SPI bus.
        /// </summary>
        /// <param name="bitmap">The source bitmap.</param>
        /// <param name="config">The SPI configuration the display uses.</param>
        /// <param name="xSrc">The X coordinate of the upper-left corner of the rectangular area in the source bitmap from which the specified pixels are to be copied.</param>
        /// <param name="ySrc">The Y coordinate of the upper-left corner of the rectangular area in the source bitmap from which the specified pixels are to be copied.</param>
        /// <param name="width">The width of the rectangular block of pixels to be copied.</param>
        /// <param name="height">The height of the rectangular block of pixels to be copied.</param>
        /// <param name="bpp">The encoding to use.</param>
        public delegate void BitmapCopyToSpi(Bitmap bitmap, SPI.Configuration config, int xSrc, int ySrc, int width, int height, BPP bpp);

        /// <summary>
        /// Native bitmap to SPI functionality provided by the mainboard. Null if not available on this mainboard.
        /// </summary>
        public BitmapCopyToSpi NativeBitmapCopyToSpi = null;

        /// <summary>
        /// Sets the debug light emiting diode (LED) on or off.  If there is no debug LED, this method returns without setting the out parameter.
        /// </summary>
        /// <param name="on">true if the debug LED should be on.</param>
        public abstract void SetDebugLED(bool on);

        /// <summary>
        /// Called after the initialization of the user's program, after the ProgramStarted method and field initializations, but before the Dispatcher is started.
        /// This can be used by the mainboard driver to do tasks that need to occur after modules are initialized.
        /// </summary>
        public abstract void PostInit();

        /// <summary>
        /// The name of this mainboard, which is automatically printed to the debug stream at startup.
        /// </summary>
        public abstract string MainboardName { get; }

        /// <summary>
        /// The version of this mainboard, which is automatically printed to the debug stream at startup.
        /// </summary>
        public abstract string MainboardVersion { get; }

        /// <summary>
        /// Specifies the properties of a liquid crystal display (LCD) module.
        /// </summary>
        public class LCDConfiguration
        {
            /// <summary>
            /// The HeadlessConfig property of the LCD.
            /// </summary>
            public static LCDConfiguration HeadlessConfig = null;

            /// <summary>
            /// Indicates whether the LCD is enabled.
            /// </summary>
            public bool LCDControllerEnabled;

            /// <summary>
            /// The width property of the LCD.
            /// </summary>
            public uint Width = 0;
            /// <summary>
            /// The height property of the LCD.
            /// </summary>
            public uint Height = 0;
            /// <summary>
            /// The PixelClockDivider property of the LCD.
            /// </summary>
            public byte PixelClockDivider;
            /// <summary>
            /// The PriorityEnable property of the LCD.
            /// </summary>
            public bool PriorityEnable;
            /// <summary>
            /// The OutputEnableIsFixed property of the LCD.
            /// </summary>
            public bool OutputEnableIsFixed;
            /// <summary>
            /// The OutputEnablePolarity of the LCD.
            /// </summary>
            public bool OutputEnablePolarity;
            /// <summary>
            /// The HorizontalSyncPolarity property of the LCD.
            /// </summary>
            public bool HorizontalSyncPolarity;
            /// <summary>
            /// The VerticalSyncPolarity property of the LCD.
            /// </summary>
            public bool VerticalSyncPolarity;
            /// <summary>
            /// The PixelPolarity property of the LCD.
            /// </summary>
            public bool PixelPolarity;
            /// <summary>
            /// The HorizontalSyncPulseWidth property of the LCD.
            /// </summary>
            public byte HorizontalSyncPulseWidth;
            /// <summary>
            /// The HorizontalBackPorch property of the LCD.
            /// </summary>
            public byte HorizontalBackPorch;
            /// <summary>
            /// The HorizontalFrontPorch property of the LCD.
            /// </summary>
            public byte HorizontalFrontPorch;
            /// <summary>
            /// The VerticalSyncPulseWidth property of the LCD.
            /// </summary>
            public byte VerticalSyncPulseWidth;
            /// <summary>
            /// The VerticalBackPorch property of the LCD.
            /// </summary>
            public byte VerticalBackPorch;
            /// <summary>
            /// The VerticalFrontPorch property of the LCD.
            /// </summary>
            public byte VerticalFrontPorch;
        }

        /// <summary>
        /// Specifies the number of bits per LCD pixel and the format of those bits.
        /// </summary>
        public enum BPP
        {
            /// <summary>
            /// 16 bits per pixel, 5 for red, 6 for green, 5 for blue in this order, big endian.
            /// </summary>
            BPP16_BGR_BE
        }


        /// <summary>
        /// Represents the available interfaces for the programming mode.
        /// </summary>
        public enum ProgrammingInterface
        {
            /// <summary>
            /// The first serial communication port (COM1).
            /// </summary>
            Serial_COM1 = 0,
            /// <summary>
            /// The second serial communication port (COM2).
            /// </summary>
            Serial_COM2 = 1,
            /// <summary>
            /// The third serial communication port (COM3).
            /// </summary>
            Serial_COM3 = 2,
            /// <summary>
            /// The fourth serial communication port (COM4).
            /// </summary>
            Serial_COM4 = 3,
            /// <summary>
            /// USB communication port.
            /// </summary>
            USB = 4,
            /// <summary>
            /// Network communication port.
            /// </summary>
            Network = 5
        }
    }
}
