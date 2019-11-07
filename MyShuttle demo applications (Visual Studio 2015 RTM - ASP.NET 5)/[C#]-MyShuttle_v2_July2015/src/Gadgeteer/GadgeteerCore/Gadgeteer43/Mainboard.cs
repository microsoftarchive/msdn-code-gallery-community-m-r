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
        public abstract void SetProgrammingMode(ProgrammingInterface programmingInterface);

        /// <summary>
        /// When overriden in a derived class, configure the onboard display controller to fulfil the requirements of a display using the RGB sockets.
        /// If doing this requires rebooting, then the method must reboot and not return.
        /// If there is no onboard display controller, then NotSupportedException must be thrown.
        /// </summary>
        /// <param name="displayModel">Display model name.</param>
        /// <param name="width">Display physical width in pixels, ignoring the orientation setting.</param>
        /// <param name="height">Display physical height in lines, ignoring the orientation setting.</param>
        /// <param name="orientationDeg">Display orientation in degrees.</param>
        /// <param name="timing">The required timings from an LCD controller.</param>
        protected internal abstract void OnOnboardControllerDisplayConnected(string displayModel, int width, int height, int orientationDeg, Modules.Module.DisplayModule.TimingRequirements timing);

        /// <summary>
        /// Called when the onboard display controller's display is disconnected, so any resources used by the onboard display controller could be reclaimed. 
        /// </summary>
        protected internal virtual void OnOnboardControllerDisplayDisconnected() {}

        /// <summary>
        /// When overriden in a derived class, ensures that the pins on R, G and B sockets (which also have other socket types) are available for use for non-display purposes.
        /// If doing this requires rebooting, then the method must reboot and not return.
        /// If there is no onboard display controller, or it is not possible to disable the onboard display controller, then NotSupportedException must be thrown.
        /// </summary>
        public abstract void EnsureRgbSocketPinsAvailable();

        /// <summary>
        /// A delegate for serial peripheral interface (SPI) display support, which marshals a Bitmap into a target byte array, e.g. encoded as 16-bit color.
        /// </summary>
        /// <param name="bitmap">The bitmap to convert.</param>
        /// <param name="pixelBytes">The array of video bytes, which should be sized accordingly.</param>
        /// <param name="bpp">The encoding to use.</param>
        public delegate void BitmapConvertBPP(Bitmap bitmap, byte[] pixelBytes, BPP bpp);

        /// <summary>
        /// Native bitmap conversion functionality provided by the mainboard. Null if not available on this mainboard.
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
