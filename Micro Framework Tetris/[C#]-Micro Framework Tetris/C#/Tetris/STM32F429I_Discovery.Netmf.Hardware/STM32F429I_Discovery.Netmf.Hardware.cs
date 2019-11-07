/**
  ******************************************************************************
  * @file    STM32F429I_Discovery.Netmf.Hardware\STM32F429I_Discovery.Netmf.Hardware.cs
  * @author  MCD
  * @version V1.0.0
  * @date    24-Sep-2013
  * @brief   STM32F429I_Discovery managed library.
  ******************************************************************************
   * @attention
  *
  * <h2><center>&copy; COPYRIGHT 2013 STMicroelectronics</center></h2>
  *
  * Licensed under MCD-ST Liberty SW License Agreement V2, (the "License");
  * You may not use this file except in compliance with the License.
  * You may obtain a copy of the License at:
  *
  *        http://www.st.com/software_license_agreement_liberty_v2
  *
  * Unless required by applicable law or agreed to in writing, software 
  * distributed under the License is distributed on an "AS IS" BASIS, 
  * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
  * See the License for the specific language governing permissions and
  * limitations under the License.
  *
  * <h2><center>&copy; COPYRIGHT 2013 STMicroelectronics</center></h2>
  */

/* References ------------------------------------------------------------------*/
using System;
using Microsoft.SPOT;
using System.Threading;
using Microsoft.SPOT.Hardware;

/// <summary>
/// This namespace provides set of functions to manage main features
/// available on STM32F429I-DISCO Kit from STMicroelectronics.
/// </summary>
namespace STM32F429I_Discovery.Netmf.Hardware
{
    /// <summary>
    /// This class provides set of functions to manage LEDs
    /// on STM32F429I-DISCO Kit from STMicroelectronics.
    /// </summary>
    class LED
    {        
        const Cpu.Pin Led1 = (Cpu.Pin)(6 * 16 + 13); //PG13
        const Cpu.Pin Led2 = (Cpu.Pin)(6 * 16 + 14); //PG14

        /// <summary>
        /// Led 1 (green LED) connected to PG13
        /// </summary>
        public static OutputPort led1;
        /// <summary>
        /// Led 2 (red LED) connected to PG14
        /// </summary>
        public static OutputPort led2;

        /// <summary>
        /// Configures LED GPIO.
        /// </summary>
        public static void LEDInit()
        {
            led1 = new OutputPort(Led1, false);
            led2 = new OutputPort(Led2, false);
        }

        /// <summary>
        /// Turn on green LED
        /// </summary>
        public static void GreenLedOn()
        {
            led1.Write(true);    
        }

        /// <summary>
        /// Turn off green LED
        /// </summary>
        public static void GreenLedOff()
        {
            led1.Write(false);    
        }

        /// <summary>
        /// Toggle green LED
        /// </summary>
        public static void GreenLedToggle()
        {
            led1.Write(!led1.Read());   
        }

        /// <summary>
        /// Turn on red LED
        /// </summary>
        public static void RedLedOn()
        {
            led2.Write(true);    
        }

        /// <summary>
        /// Turn off red LED
        /// </summary>
        public static void RedLedOff()
        {
            led2.Write(false);    
        }

        /// <summary>
        /// Toggle red LED
        /// </summary>
        public static void RedLedToggle()
        {
            led2.Write(!led2.Read());    
        }
    }

    /// <summary>
    /// ADC Channels
    /// </summary>
    class ADC
    {
        /// <summary>
        /// Analog input channel0 (pin PA6)
        /// <para>Note: PA6 is also used by the DAC and LCD controller</para>
        /// </summary>
        public const Cpu.AnalogChannel Channel0_PA6 = Cpu.AnalogChannel.ANALOG_0;
        /// <summary>
        /// Analog input channel1 (pin PA7)
        /// </summary>
        public const Cpu.AnalogChannel Channel1_PA7 = Cpu.AnalogChannel.ANALOG_1;
        /// <summary>
        /// Analog input channel2 (pin PC1)
        /// <para>Note: PC1 is also used by the DAC and LCD controller</para>
        /// </summary>
        public const Cpu.AnalogChannel Channel2_PC1 = Cpu.AnalogChannel.ANALOG_2;
        /// <summary>
        /// Analog input channel3 (pin PC3)
        /// </summary>
        public const Cpu.AnalogChannel Channel3_PC3 = Cpu.AnalogChannel.ANALOG_3;
    }

    /// <summary>
    /// DAC Channels
    /// </summary>
    class DAC
    {
        /// <summary>
        /// Analog output channel0 (pin PA4)
        /// <para>Note: PA4 is also used by the ADC</para>
        /// </summary>
        public const Cpu.AnalogOutputChannel Channel0_PA4 = Cpu.AnalogOutputChannel.ANALOG_OUTPUT_0;
        /// <summary>
        /// Analog output channel1 (pin PA5)
        /// <para>Note: PA5 is also used by the ADC</para>
        /// </summary>
        public const Cpu.AnalogOutputChannel Channel1_PA5 = Cpu.AnalogOutputChannel.ANALOG_OUTPUT_1;
    }

    /// <summary>
    /// PWM output Channels
    /// </summary>
    class PWM_Channels
    {
        /// <summary>
        /// PWM channel0 (pin PA8) connected to TIM1_CH1 
        /// <para>Note: PA8 is also used by I2C3_SCL</para>
        /// </summary>
        public const Cpu.PWMChannel PWM0_PA8 = Cpu.PWMChannel.PWM_0;
        /// <summary>
        /// PWM channel1 (pin PA9) connected to TIM1_CH2
        /// </summary>
        public const Cpu.PWMChannel PWM1_PA9 = Cpu.PWMChannel.PWM_1;
        /// <summary>
        /// PWM channel2 (pin PA10) connected to TIM1_CH3
        /// </summary>
        public const Cpu.PWMChannel PWM2_PA10 = Cpu.PWMChannel.PWM_2;
        /// <summary>
        /// PWM channel3 (pin PA11) connected to TIM1_CH4
        /// <para>Note: PA11 is also used by the LCD driver</para>
        /// </summary>
        public const Cpu.PWMChannel PWM3_PA11 = Cpu.PWMChannel.PWM_3;
        /// <summary>
        /// PWM channel4 (pin PB6) connected to TIM4_CH1
        /// <para>Note: PB6 is also used by the SDRAM controller</para>
        /// <para>Cpu.PWMChannel.PWM_4 should not be used.</para>
        /// </summary>
        //public const Cpu.PWMChannel PWM4_PB6 = Cpu.PWMChannel.PWM_4;
        /// <summary>
        /// PWM channel4 (pin PB7) connected to TIM4_CH2
        /// </summary>
        public const Cpu.PWMChannel PWM4_PB7 = Cpu.PWMChannel.PWM_5;
        /// <summary>
        /// PWM channel5 (pin PB8) connected to TIM4_CH3
        /// <para>Note: PB8 is also used by the LCD driver</para>
        /// </summary>
        public const Cpu.PWMChannel PWM5_PB8 = Cpu.PWMChannel.PWM_6;
        /// <summary>
        /// PWM channel6 (pin PB9) connected to TIM4_CH4
        /// <para>Note: PB9 is also used by the LCD driver</para>
        /// </summary>
        public const Cpu.PWMChannel PWM6_PB9 = Cpu.PWMChannel.PWM_7;
    }

    /// <summary>
    /// Serial Ports
    /// <para>Only USART1 and UART5 are available since USART 2,3 </para>
    /// <para>and UART4 share pins with LCD controoler.</para>
    /// </summary>
    class SerialPorts
    {
        /// <summary>
        /// COM1 is mapped to on-chip UsART1.
        /// <para>RX Pin = PA10</para>
        /// <para>TX Pin = PA9</para>
        /// <para>Hardware flow control is not supported since cts pin is shared with LCD controoler</para>
        /// </summary>
        public const string SerialCOM1 = "COM1";

        /// <summary>
        /// COM2 is mapped to on-chip UART5.
        /// <para>RX Pin = PD2</para>
        /// <para>TX Pin = PC12</para>
        /// <para>Hardware flow control not supported.</para>
        /// </summary>
        public const string SerialCOM2 = "COM5";

    }
    /// <summary>
    /// L3GD20 data in DPS
    /// </summary>
    public struct AngDPS
    {
        public float x;
        public float y;
        public float z;
    }

    /// <summary>
    /// L3GD20_SPI
    /// </summary>
    class L3GD20_SPI
    {
        
/******************************************************************************/
/*************************** START REGISTER MAPPING  **************************/
/******************************************************************************/
    const Byte  L3GD20_WHO_AM_I_ADDR        = 0x0F;  /* device identification register */
    const Byte  L3GD20_CTRL_REG1_ADDR       = 0x20; /* Control register 1 */
    const Byte  L3GD20_CTRL_REG2_ADDR       = 0x21; /* Control register 2 */
    const Byte  L3GD20_CTRL_REG3_ADDR       = 0x22; /* Control register 3 */
    const Byte  L3GD20_CTRL_REG4_ADDR       = 0x23; /* Control register 4 */
    const Byte  L3GD20_CTRL_REG5_ADDR       = 0x24; /* Control register 5 */
    const Byte  L3GD20_REFERENCE_REG_ADDR   = 0x25; /* Reference register */
    const Byte  L3GD20_OUT_TEMP_ADDR        = 0x26; /* Out temp register */
    const Byte  L3GD20_STATUS_REG_ADDR      = 0x27; /* Status register */
    const Byte  L3GD20_OUT_X_L_ADDR         = 0x28; /* Output Register X */
    const Byte  L3GD20_OUT_X_H_ADDR         = 0x29; /* Output Register X */
    const Byte  L3GD20_OUT_Y_L_ADDR         = 0x2A; /* Output Register Y */
    const Byte  L3GD20_OUT_Y_H_ADDR         = 0x2B; /* Output Register Y */
    const Byte  L3GD20_OUT_Z_L_ADDR         = 0x2C; /* Output Register Z */
    const Byte  L3GD20_OUT_Z_H_ADDR         = 0x2D; /* Output Register Z */ 
    const Byte  L3GD20_FIFO_CTRL_REG_ADDR   = 0x2E; /* Fifo control Register */
    const Byte  L3GD20_FIFO_SRC_REG_ADDR    = 0x2F; /* Fifo src Register */

    const Byte  L3GD20_INT1_CFG_ADDR        = 0x30; /* Interrupt 1 configuration Register */
    const Byte  L3GD20_INT1_SRC_ADDR        = 0x31; /* Interrupt 1 source Register */
    const Byte  L3GD20_INT1_TSH_XH_ADDR     = 0x32; /* Interrupt 1 Threshold X register */
    const Byte  L3GD20_INT1_TSH_XL_ADDR     = 0x33; /* Interrupt 1 Threshold X register */
    const Byte  L3GD20_INT1_TSH_YH_ADDR     = 0x34; /* Interrupt 1 Threshold Y register */
    const Byte  L3GD20_INT1_TSH_YL_ADDR     = 0x35; /* Interrupt 1 Threshold Y register */
    const Byte  L3GD20_INT1_TSH_ZH_ADDR     = 0x36; /* Interrupt 1 Threshold Z register */
    const Byte  L3GD20_INT1_TSH_ZL_ADDR     = 0x37; /* Interrupt 1 Threshold Z register */
    const Byte  L3GD20_INT1_DURATION_ADDR   = 0x38; /* Interrupt 1 DURATION register */

/******************************************************************************/
/**************************** END REGISTER MAPPING  ***************************/
/******************************************************************************/

        const Byte  I_AM_L3GD20		= (0xD4);

        /* Power_Mode_selection */
        const Byte  L3GD20_MODE_POWERDOWN = (0x00);
        const Byte  L3GD20_MODE_ACTIVE    = (0x08);

        /* OutPut_DataRate_Selection */
        const Byte  L3GD20_OUTPUT_DATARATE_1 = (0x00);
        const Byte  L3GD20_OUTPUT_DATARATE_2 = (0x40);
        const Byte  L3GD20_OUTPUT_DATARATE_3 = (0x80);
        const Byte  L3GD20_OUTPUT_DATARATE_4 = (0xC0);

        /* Axes_Selection */
        const Byte  L3GD20_X_ENABLE      = (0x02);
        const Byte  L3GD20_Y_ENABLE      = (0x01);
        const Byte  L3GD20_Z_ENABLE      = (0x04);
        const Byte  L3GD20_AXES_ENABLE   = (0x07);
        const Byte  L3GD20_AXES_DISABLE  = (0x00);

        /* BandWidth_Selection */
        const Byte  L3GD20_BANDWIDTH_1   = (0x00);
        const Byte  L3GD20_BANDWIDTH_2   = (0x10);
        const Byte  L3GD20_BANDWIDTH_3   = (0x20);
        const Byte  L3GD20_BANDWIDTH_4   = (0x30);

        /* Full_Scale_Selection */
        const Byte  L3GD20_FULLSCALE_250   = (0x00);
        const Byte  L3GD20_FULLSCALE_500   = (0x10);
        const Byte  L3GD20_FULLSCALE_2000  = (0x20);
  
        /* Block_Data_Update  */  
        const Byte  L3GD20_BlockDataUpdate_Continous = (0x00);
        const Byte  L3GD20_BlockDataUpdate_Single = (0x80);
  
        /* Endian_Data_selection */  
        const Byte  L3GD20_BLE_LSB                = (0x00);
        const Byte  L3GD20_BLE_MSB	              = (0x40);

  
        /* High_Pass_Filter_status */   
        const Byte  L3GD20_HIGHPASSFILTER_DISABLE = (0x00);
        const Byte  L3GD20_HIGHPASSFILTER_ENABLE  = (0x10);

        /* INT1_Interrupt_status */   
        const Byte  L3GD20_INT1INTERRUPT_DISABLE = (0x00);
        const Byte  L3GD20_INT1INTERRUPT_ENABLE	 = (0x80);

        /* INT2_Interrupt_status */   
        const Byte  L3GD20_INT2INTERRUPT_DISABLE = (0x00);
        const Byte  L3GD20_INT2INTERRUPT_ENABLE	 = (0x08);

        /* INT1_Interrupt_ActiveEdge  */   
        const Byte  L3GD20_INT1INTERRUPT_LOW_EDGE  = (0x20);
        const Byte  L3GD20_INT1INTERRUPT_HIGH_EDGE = (0x00);
  
        /* Boot_Mode_selection */
        const Byte  L3GD20_BOOT_NORMALMODE       = (0x00);
        const Byte  L3GD20_BOOT_REBOOTMEMORY     = (0x80);
 
        /* High_Pass_Filter_Mode */   
        const Byte  L3GD20_HPM_NORMAL_MODE_RES   = (0x00);
        const Byte  L3GD20_HPM_REF_SIGNAL        = (0x10);
        const Byte  L3GD20_HPM_NORMAL_MODE       = (0x20);
        const Byte  L3GD20_HPM_AUTORESET_INT     = (0x30);

        /* High_Pass_CUT OFF_Frequency  */   
        const Byte  L3GD20_HPFCF_0 = 0x00;
        const Byte  L3GD20_HPFCF_1 = 0x01;
        const Byte  L3GD20_HPFCF_2 = 0x02;
        const Byte  L3GD20_HPFCF_3 = 0x03;
        const Byte  L3GD20_HPFCF_4 = 0x04;
        const Byte  L3GD20_HPFCF_5 = 0x05;
        const Byte  L3GD20_HPFCF_6 = 0x06;
        const Byte  L3GD20_HPFCF_7 = 0x07;
        const Byte  L3GD20_HPFCF_8 = 0x08;
        const Byte  L3GD20_HPFCF_9 = 0x09;

        const Byte L3GD20_AXES_INT_ENABLE = 0x3F;

        const float L3G_Sensitivity_250dps = 114.285f;  /*!< gyroscope sensitivity with 250 dps full scale [LSB/dps]  */
        const float L3G_Sensitivity_500dps = 57.1429f;  /*!< gyroscope sensitivity with 500 dps full scale [LSB/dps]  */
        const float L3G_Sensitivity_2000dps = 14.285f;  /*!< gyroscope sensitivity with 2000 dps full scale [LSB/dps] */       

        /* Read/Write command */
        const Byte READWRITE_CMD  = (0x80); 
        /* Multiple byte read/write command */
        const Byte MULTIPLEBYTE_CMD = (0x40);

        private SPI spi5;

        float X_BiasError = 0,Y_BiasError = 0,Z_BiasError = 0;
        public int BiasErrorSplNbr = 1000;
        bool IsCalibrated = false;
        public delegate void MEMS_Int1_Delegate(UInt32 port, UInt32 state, DateTime time);

        /// <summary>
        /// SPI interface and L3GD20 configuration
        /// </summary>
        public int Init()
        {
            /* Configure the SPI1 with:
             * chip select pin : PC1
             * clockrate : 5 MHZ
             * clock line is low if device is not selected (CPOL = 0)
             * data is sampled at rising edge of clock (CPHA = 1)
             * SPI1 Clock pin (SCK)  : PF7
             * SPI1 Input pin (MISO) : PF8
             * SPI1 Output pin (MOSI): PF9
             */
            SPI.Configuration config = new SPI.Configuration((Cpu.Pin)33,  false, 1, 1, false, true, 5000, (SPI.SPI_module)4);

            /* Initialize SPI interface */
            spi5 = new SPI(config);

            /* L3GD20 configuration*/
            Config();

            return 0;
        }

        /// <summary>
        /// L3GD20 configuration
        /// </summary>
        int Config()
        {
            Byte tmp = 0;

            /* Configure CTRL_REG1 register */
            Byte cr1 = (L3GD20_MODE_ACTIVE | L3GD20_OUTPUT_DATARATE_1 | L3GD20_AXES_ENABLE | L3GD20_BANDWIDTH_4);
            WriteRegister(L3GD20_CTRL_REG1_ADDR,cr1);

            /* Configure CTRL_REG4 register */
            Byte cr4 = (L3GD20_BlockDataUpdate_Continous | L3GD20_BLE_LSB | L3GD20_FULLSCALE_500);
            WriteRegister(L3GD20_CTRL_REG4_ADDR, cr4);
            
            /* High Pass Filter config */
            /* Read CTRL_REG2 register */
            tmp = (Byte)(ReadRegister(L3GD20_CTRL_REG2_ADDR) & 0x00);
  
            /* Configure MEMS: mode and cutoff frquency */
            tmp |= (Byte)(L3GD20_HPM_NORMAL_MODE_RES | L3GD20_HPFCF_0);                             

            /* Write value to MEMS CTRL_REG2 regsister */
            WriteRegister(L3GD20_CTRL_REG2_ADDR, tmp);

            /* Enable or Disable High Pass Filter */
            /* Read CTRL_REG5 register */
            tmp = (Byte)(ReadRegister(L3GD20_CTRL_REG5_ADDR) & 0xEF);

            tmp |= L3GD20_HIGHPASSFILTER_ENABLE;

            /* Write value to MEMS CTRL_REG5 regsister */
            WriteRegister(L3GD20_CTRL_REG5_ADDR, tmp);

            Debug.Print("Configuration done.");
            
            return 0;
        }

        /// <summary>
        /// Read the angular rates from the L3GD20
        /// <param name="Gyro">AngDPS struct that will hold the Gyroscope data</param>
        /// <para>Note: SimpleCalibration() should be called before reading angular rates</para>
        /// </summary>
        public void ReadAngRate(out AngDPS Gyro)
        {
            Int16[] RawAngData = new Int16[3];
            byte Byte1, Byte2, tmp;
            tmp = ReadRegister(L3GD20_CTRL_REG4_ADDR);

            /* Read Raw data from Gyro */
            for (int i = 0; i < 3; i++)
            {
                Byte1 = ReadRegister((Byte)(L3GD20_OUT_X_L_ADDR + 2 * i));
                Byte2 = ReadRegister((Byte)(L3GD20_OUT_X_L_ADDR + 2 * i + 1));
                RawAngData[i] = (Int16)(Byte1 + ((Byte2)<<8));
            }

            if (IsCalibrated)
            {
                Gyro.x = (RawAngData[0] / L3G_Sensitivity_500dps) - X_BiasError;
                Gyro.y = (RawAngData[1] / L3G_Sensitivity_500dps) - Y_BiasError;
                Gyro.z = (RawAngData[2] / L3G_Sensitivity_500dps) - Z_BiasError;
            }
            else
            {
                Gyro.x = (RawAngData[0] / L3G_Sensitivity_500dps);
                Gyro.y = (RawAngData[1] / L3G_Sensitivity_500dps);
                Gyro.z = (RawAngData[2] / L3G_Sensitivity_500dps);
            }
        }

        /// <summary>
        /// Calibrating the gyroscope by removing the bias error.
        /// </summary>
        public void SimpleCalibration()
        {
            AngDPS Gyro;
            for (int i = 0; i < BiasErrorSplNbr; i++)
            {
                ReadAngRate(out Gyro);
                X_BiasError += Gyro.x;
                Y_BiasError += Gyro.y;
                Z_BiasError += Gyro.z;
            }
            /* Set bias errors */
            X_BiasError /= BiasErrorSplNbr;
            Y_BiasError /= BiasErrorSplNbr;
            Z_BiasError /= BiasErrorSplNbr;

            IsCalibrated = true;
        }

        /// <summary>
        /// Write L3GD20 register via SPI interface
        /// </summary>
        bool WriteRegister(Byte address, Byte Val)
        {
            UInt16[] SPI_Data = {(UInt16)(Val + (address << 8))};

            /* Send register address and value */
            spi5.Write(SPI_Data);

            return true;
        }

        /// <summary>
        /// Read L3GD20 register via SPI interface
        /// </summary>
        Byte ReadRegister(Byte address)
        {
            UInt16[] SPI_Data = { (UInt16)((address | READWRITE_CMD) << 8) };
            UInt16[] Value = { 0 };

            /* Send register address and read its value */
            spi5.WriteRead(SPI_Data, Value);

            return (Byte)(Value[0] & 0xFF);
        }
    }
}
/******************* (C) COPYRIGHT STMicroelectronics *****END OF FILE****/