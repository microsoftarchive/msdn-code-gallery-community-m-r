using Microsoft.SPOT;
using System;
using GT = Gadgeteer;
using GTI = Gadgeteer.SocketInterfaces;
using GTM = Gadgeteer.Modules;

namespace Gadgeteer.Modules.GHIElectronics
{
    /// <summary>
    /// A Gyro module for Microsoft .NET Gadgeteer.
    /// </summary>
    public class Gyro : GTM.Module
    {
        private GT.Timer timer;
        private GTI.I2CBus i2c;
        private GTI.InterruptInput interruptInput;
        private bool ready;
        private byte[] readBuffer1;
        private byte[] writeBuffer1;
        private byte[] writeBuffer2;
        private byte[] readBuffer8;
        private double offsetX;
        private double offsetY;
        private double offsetZ;

        /// <summary>Constructs a new instance.</summary>
        /// <param name="socketNumber">The socket that this module is plugged in to.</param>
        public Gyro(int socketNumber)
        {
            Socket socket = Socket.GetSocket(socketNumber, true, this, null);
            socket.EnsureTypeIsSupported('I', this);
            
            this.readBuffer1 = new byte[1];
            this.writeBuffer1 = new byte[1];
            this.writeBuffer2 = new byte[2];
            this.readBuffer8 = new byte[8];

            this.offsetX = 0;
            this.offsetY = 0;
            this.offsetZ = 0;

            this.ready = false;
            this.timer = new GT.Timer(200);
            this.timer.Tick += (a) => this.TakeMeasurement();

            this.i2c = GTI.I2CBusFactory.Create(socket, 0x68, 100, this);
            
            this.interruptInput = GTI.InterruptInputFactory.Create(socket, GT.Socket.Pin.Three, GTI.GlitchFilterMode.Off, GTI.ResistorMode.Disabled, GTI.InterruptMode.RisingEdge, this);
            this.interruptInput.Interrupt += this.OnInterrupt;

            this.SetFullScaleRange();
        }

        private enum Register : byte
        {
            WHO_AM_I = 0x00,  
            SMPLRT_DIV = 0x15,
            DLPF_FS = 0x16,   
            INT_CFG = 0x17,   
            INT_STATUS = 0x1A,
            TEMP_OUT_H = 0x1B,
            TEMP_OUT_L = 0x1C,
            GYRO_OUT_XOUT_H = 0x1D,
            GYRO_OUT_XOUT_L = 0x1E,
            GYRO_OUT_YOUT_H = 0x1F,
            GYRO_OUT_YOUT_L = 0x20,
            GYRO_OUT_ZOUT_H = 0x21,
            GYRO_OUT_ZOUT_L = 0x22,
            PWR_MGM = 0x3E
        }

        /// <summary>
        /// Available low pass filter bandwidth settings.
        /// </summary>
        public enum Bandwidth
        {
            /// <summary>
            /// 256Hz
            /// </summary>
            Hertz256 = 0,
            /// <summary>
            /// 188Hz
            /// </summary>
            Hertz188 = 1,
            /// <summary>
            /// 98Hz
            /// </summary>
            Hertz98 = 2,
            /// <summary>
            /// 42Hz
            /// </summary>
            Hertz42 = 3,
            /// <summary>
            /// 20Hz
            /// </summary>
            Hertz20 = 4,
            /// <summary>
            /// 10Hz
            /// </summary>
            Hertz10 = 5,
            /// <summary>
            /// 5Hz
            /// </summary>
            Hertz5 = 6
        }

        /// <summary>
        /// The low pass filter configuration. Note that setting the low pass filter to 256Hz results in a maximum internal sample rate
        /// of 8kHz. Any other setting results in a maximum sample rate of 1kHz. The sample rate can be further divided by using SampleRateDivider.
        /// </summary>
        public Bandwidth LowPassFilter
        {
            get
            {
                return (Bandwidth)(this.Read(Register.DLPF_FS) & 0x7);
            }
            set
            {
                this.Write(Register.DLPF_FS, (byte)((byte)value | 0x18));
            }
        }

        /// <summary>
        /// the internal sample rate divider. The gyro outputs are sampled internally at either 8kHz (if the LowPassFilter is set to 256Hz) or 1kHz for any other LowPassFilter settings. This setting can be used to further divide the sample rate.
        /// </summary>
        public byte SampleRateDivider
        {
            get
            {
                return this.Read(Register.SMPLRT_DIV);
            }
            set
            {
                this.Write(Register.SMPLRT_DIV, value);
            }
        }

        private void SetFullScaleRange()
        {
            this.Write(Register.DLPF_FS, (byte)(this.Read(Register.DLPF_FS) | 0x18));
        }

        private void EnableInterruptOnDataReady()
        {
            this.Write(Register.INT_CFG, 0x21);
            this.Read(Register.INT_STATUS);
        }

        private void DisableInterruptOnDataReady()
        {
            this.Write(Register.INT_CFG, 0x20);
        }

        private byte Read(Register register)
        {
            this.writeBuffer1[0] = (byte)register;
            this.i2c.WriteRead(this.writeBuffer1, this.readBuffer1);
            return this.readBuffer1[0];
        }

        private void Read(Register register, byte[] readBuffer)
        {
            this.writeBuffer1[0] = (byte)register;
            this.i2c.WriteRead(this.writeBuffer1, readBuffer);
        }

        private void Write(Register register, byte value)
        {
            this.writeBuffer2[0] = (byte)register;
            this.writeBuffer2[1] = (byte)value;
            this.i2c.Write(this.writeBuffer2);
        }

        private void OnInterrupt(GTI.InterruptInput sender, bool value)
        {
            this.Read(Register.INT_STATUS);

            this.ready = true;
        }

        private void TakeMeasurement()
        {
            if (!this.ready)
                return;

            this.ready = false;

            this.Read(Register.TEMP_OUT_H, this.readBuffer8);
            int rawT = (this.readBuffer8[0] << 8) | this.readBuffer8[1];
            int rawX = (this.readBuffer8[2] << 8) | this.readBuffer8[3];
            int rawY = (this.readBuffer8[4] << 8) | this.readBuffer8[5];
            int rawZ = (this.readBuffer8[6] << 8) | this.readBuffer8[7];

            rawT = (((rawT >> 15) == 1) ? -32767 : 0) + (rawT & 0x7FFF);
            rawX = (((rawX >> 15) == 1) ? -32767 : 0) + (rawX & 0x7FFF);
            rawY = (((rawY >> 15) == 1) ? -32767 : 0) + (rawY & 0x7FFF);
            rawZ = (((rawZ >> 15) == 1) ? -32767 : 0) + (rawZ & 0x7FFF);

            double x = (rawX / 14.375) + this.offsetX;
            double y = (rawY / 14.375) + this.offsetY;
            double z = (rawZ / 14.375) + this.offsetZ;
            double t = (rawT + 13200) / 280.0 + 35;

            this.OnMeasurementComplete(this, new MeasurementCompleteEventArgs(x, y, z, t));

            this.ready = true;
        }

        /// <summary>
        /// Calibrates the gyro values. Ensure that the sensor is not moving when calling this method.
        /// </summary>
        public void Calibrate()
        {
            this.Read(Register.TEMP_OUT_H, this.readBuffer8);
            int rawT = (this.readBuffer8[0] << 8) | this.readBuffer8[1];
            int rawX = (this.readBuffer8[2] << 8) | this.readBuffer8[3];
            int rawY = (this.readBuffer8[4] << 8) | this.readBuffer8[5];
            int rawZ = (this.readBuffer8[6] << 8) | this.readBuffer8[7];

            rawT = (((rawT >> 15) == 1) ? -32767 : 0) + (rawT & 0x7FFF);
            rawX = (((rawX >> 15) == 1) ? -32767 : 0) + (rawX & 0x7FFF);
            rawY = (((rawY >> 15) == 1) ? -32767 : 0) + (rawY & 0x7FFF);
            rawZ = (((rawZ >> 15) == 1) ? -32767 : 0) + (rawZ & 0x7FFF);

            this.offsetX = rawX / -14.375;
            this.offsetY = rawY / -14.375;
            this.offsetZ = rawZ / -14.375;
        }

        /// <summary>
        /// Obtains a single measurement and raises the event when complete.
        /// </summary>
        public void RequestSingleMeasurement()
        {
            if (this.timer.IsRunning) throw new InvalidOperationException("You cannot request a single measurement while continuous measurements are being taken.");

            this.EnableInterruptOnDataReady();
            this.timer.Behavior = Timer.BehaviorType.RunOnce;
            this.timer.Start();
        }

        /// <summary>
        /// Starts taking measurements and fires MeasurementComplete when a new measurement is available.
        /// </summary>
        public void StartTakingMeasurements()
        {
            this.EnableInterruptOnDataReady();
            this.timer.Behavior = Timer.BehaviorType.RunContinuously;
            this.timer.Start();
        }

        /// <summary>
        /// Stops taking measurements.
        /// </summary>
        public void StopTakingMeasurements()
        {
            this.DisableInterruptOnDataReady();
            this.timer.Stop();
        }

        /// <summary>
        /// The interval at which measurements are taken.
        /// </summary>
        public TimeSpan MeasurementInterval
        {
            get
            {
                return this.timer.Interval;
            }
            set
            {
                var wasRunning = this.timer.IsRunning;

                this.timer.Stop();
                this.timer.Interval = value;

                if (wasRunning)
                    this.timer.Start();
            }
        }

        /// <summary>
        /// Event arguments for the MeasurementComplete event.
        /// </summary>
        public class MeasurementCompleteEventArgs : Microsoft.SPOT.EventArgs
        {
            /// <summary>
            /// Angular rate around the X axis (roll) in degree per second.
            /// </summary>
            public double X { get; private set; }

            /// <summary>
            /// Angular rate around the Y axis (pitch) in degree per second.
            /// </summary>
            public double Y { get; private set; }

            /// <summary>
            /// Angular rate around the Z axis (yaww) in degree per second.
            /// </summary>
            public double Z { get; private set; }

            /// <summary>
            /// Temperature in degree celsius.
            /// </summary>
            public double Temperature { get; private set; }

            internal MeasurementCompleteEventArgs(double x, double y, double z, double temperature)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.Temperature = temperature;
            }

            /// <summary>
            /// Provides a string representation of the instance.
            /// </summary>
            /// <returns>A string describing the values contained in the object.</returns>
            public override string ToString()
            {
                return "X: " + X.ToString("f2") + " Y: " + Y.ToString("f2") + " Z: " + Z.ToString("f2") + " Temperature: " + Temperature.ToString("f2");
            }
        }

        /// <summary>
        /// Represents the delegate used for the MeasurementComplete event.
        /// </summary>
        /// <param name="sender">The object that raised the event.</param>
        /// <param name="e">The event arguments.</param>
        public delegate void MeasurementCompleteEventHandler(Gyro sender, MeasurementCompleteEventArgs e);

        /// <summary>
        /// Raised when a measurement reading is complete.
        /// </summary>
        public event MeasurementCompleteEventHandler MeasurementComplete;

        private MeasurementCompleteEventHandler onMeasurementComplete;

        private void OnMeasurementComplete(Gyro sender, MeasurementCompleteEventArgs e)
        {
            if (this.onMeasurementComplete == null)
                this.onMeasurementComplete = this.OnMeasurementComplete;

            if (Program.CheckAndInvoke(this.MeasurementComplete, this.onMeasurementComplete, sender, e))
                this.MeasurementComplete(sender, e);
        }
    }
}
