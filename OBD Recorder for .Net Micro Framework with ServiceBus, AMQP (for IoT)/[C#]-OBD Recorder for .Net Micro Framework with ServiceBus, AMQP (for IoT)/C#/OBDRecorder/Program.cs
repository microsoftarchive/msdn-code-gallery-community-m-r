//  ------------------------------------------------------------------------------------
//  Copyright (c) Microsoft Corporation
//  All rights reserved. 
//  
//  Licensed under the Apache License, Version 2.0 (the ""License""); you may not use this 
//  file except in compliance with the License. You may obtain a copy of the License at 
//  http://www.apache.org/licenses/LICENSE-2.0  
//  
//  THIS CODE IS PROVIDED *AS IS* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, 
//  EITHER EXPRESS OR IMPLIED, INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OR 
//  CONDITIONS OF TITLE, FITNESS FOR A PARTICULAR PURPOSE, MERCHANTABLITY OR 
//  NON-INFRINGEMENT. 
// 
//  See the Apache Version 2.0 License for specific language governing permissions and 
//  limitations under the License.
//  ------------------------------------------------------------------------------------

#define SB
//#define OrderedMessagingDemo
using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Net;
using System.IO;

using Microsoft.SPOT;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Controls;
using Microsoft.SPOT.Presentation.Media;
using Microsoft.SPOT.Touch;

using Elm327.Core.ObdModes;
using Elm327.Core;

using GT = Gadgeteer;
using Gadgeteer.Modules.GHIElectronics;
using Gadgeteer.Modules.Seeed;

#if SB
using Microsoft.ServiceBus.Micro;
using Amqp;
using AmqpSendReceive;
#else
    public interface ILogger
    {
        void TraceLog(string text);
        void TraceLog(string text, bool bShowOnDisplay, bool bSendToServiceBus);
    }

#endif



namespace Microsoft.Samples.ObdRecorder
{
    public partial class Program : ILogger
    {
        Text _text1;
        Text _text2;
        Text _text3;
        Text _text4;
        Text _text5;
        Text _text6;
        Text _text7;
        Text _text8;
        Text _text9;
        Text _text10;
        Text _textLong;

        GT.Timer _timer;
        TimeSpan _targetTelemetryInterval = new TimeSpan(0, 0, 10);

        class DeviceProvisioningInfo
        {
            public int DeviceId { get; set; }
            public string baseUri { get; set; }
            public string InboxEntity { get; set; }
            public string OutboxEntity { get; set; }
            public string keyName { get; set; }
            public string keyValue { get; set; }
        }

        DeviceProvisioningInfo deviceInfo = new DeviceProvisioningInfo();
        
        // This information is obtained from the Reykjavik device provisioning tool and "burned" into the device

        private void GetDeviceProvisioningInfo()
        {
#if !OrderedMessagingDemo
            this.deviceInfo.DeviceId = 0x00002001;
            this.deviceInfo.baseUri = "<your namespace>.servicebus.windows.net";
            // TODO Wire up to command entity instead of subscription for loopback
            this.deviceInfo.InboxEntity = "out/t0000/Subscriptions/s0000"; // Command channel for this device 
            this.deviceInfo.OutboxEntity = "/in/t0000"; // Telemetry channel for this device
            this.deviceInfo.keyName = "owner";
            this.deviceInfo.keyValue = "<your key>";
#else
            this.deviceInfo.DeviceId = 0x00002001;
            this.deviceInfo.baseUri = "<your namespace>.servicebus.windows.net";
            // TODO Wire up to command entity instead of subscription for loopback
            this.deviceInfo.InboxEntity = "out/t0000/Subscriptions/s0000"; // Command channel for this device 
            this.deviceInfo.OutboxEntity = "demo4/0"; // Telemetry channel for this device
            this.deviceInfo.keyName = "demohubmanagerule";
            this.deviceInfo.keyValue = "<your key>";
#endif
        }

        DateTime _lastDeviceTemperatureHumidityTimestamp = DateTime.MinValue;
        double _lastDeviceTemperature = double.NaN;
        double _lastDeviceHumidity = double.NaN;

#if SB
        AmqpSender SBSender;
        AmqpReceiver SBReceiver;

        bool bSBViaHttp = false;
        bool bSBViaAmqp = true;

        bool bTraceAllToServiceBus = false;
#endif
        OBDIIReader OBDReader;
        TimeHelper TimeHelper;

#if MyMode01
        MyObdMode01 myMode01;
#endif

        // This method is run when the mainboard is powered up or reset.   
        void ProgramStarted()
        {
            /*******************************************************************************************
            Modules added in the Program.gadgeteer designer view are used by typing 
            their name followed by a period, e.g.  button.  or  camera.
            
            Many modules generate useful events. Type +=<tab><tab> to add a handler to an event, e.g.:
                button.ButtonPressed +=<tab><tab>
            
            If you want to do something periodically, use a GT.Timer and handle its Tick event, e.g.:
                GT.Timer timer = new GT.Timer(1000); // every second (1000ms)
                timer.Tick +=<tab><tab>
                timer.Start();
            *******************************************************************************************/
            this.OBDReader = new OBDIIReader(this, this.obd_II);
            this.TimeHelper = new TimeHelper(this);

            TraceLog("Program Started", false
#if SB
                , false
#endif
                );
#if !MF_EMULATOR            
            InitializeDisplay();

            this.temperatureHumidity.MeasurementComplete += new GT.Modules.Seeed.TemperatureHumidity.MeasurementCompleteEventHandler(temperatureHumidity_MeasurementComplete);

            this.camera.PictureCaptured += new Camera.PictureCapturedEventHandler(camera_PictureCaptured);

            temperatureHumidity.RequestMeasurement();

            button.ButtonPressed += new Button.ButtonEventHandler(button_ButtonPressed);
#endif
            _timer = new GT.Timer(_targetTelemetryInterval);
            _timer.Tick += new GT.Timer.TickEventHandler(timer_Tick);
            _timer.Start();

        }

        void InitializeDisplay()
        {
            var canvas = new Canvas();
            _text1 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 1");
            _text2 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 2");
            Canvas.SetTop(_text2, 0);
            Canvas.SetLeft(_text2, display_T35.WPFWindow.Width / 2);
            _text3 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 3");
            Canvas.SetTop(_text3, 20);
            _text4 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 4");
            Canvas.SetTop(_text4, 20);
            Canvas.SetLeft(_text4, display_T35.WPFWindow.Width / 2);
            _text5 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 5");
            Canvas.SetTop(_text5, 40);
            _text6 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 6");
            Canvas.SetTop(_text6, 40);
            Canvas.SetLeft(_text6, display_T35.WPFWindow.Width / 2);
            _text7 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 7");
            Canvas.SetTop(_text7, 60);
            _text8 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 8");
            Canvas.SetTop(_text8, 60);
            Canvas.SetLeft(_text8, display_T35.WPFWindow.Width / 2);
            _text9 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 9");
            Canvas.SetTop(_text9, 80);
            _text10 = new Text(Resources.GetFont(Resources.FontResources.NinaB), "Line 10");
            Canvas.SetTop(_text10, 80);
            Canvas.SetLeft(_text10, display_T35.WPFWindow.Width / 2);

            _textLong = new Text(Resources.GetFont(Resources.FontResources.small), "Log 1\nLog 2\nLog 3");
            Canvas.SetTop(_textLong, 100);
            _textLong.Height = 100;
            _textLong.Width = display_T35.WPFWindow.Width;
            _textLong.Height = display_T35.WPFWindow.Height - Canvas.GetTop(_textLong);
            _textLong.TextWrap = true;

            canvas.Children.Add(_text1);
            canvas.Children.Add(_text2);
            canvas.Children.Add(_text3);
            canvas.Children.Add(_text4);
            canvas.Children.Add(_text5);
            canvas.Children.Add(_text6);
            canvas.Children.Add(_text7);
            canvas.Children.Add(_text8);
            canvas.Children.Add(_text9);
            canvas.Children.Add(_text10);
            canvas.Children.Add(_textLong);
            display_T35.WPFWindow.Child = canvas;
        }
        
        void temperatureHumidity_MeasurementComplete(GT.Modules.Seeed.TemperatureHumidity sender, double temperature, double relativeHumidity)
        {
            if (this.TimeHelper.UpdateTime())
            {
                this._lastDeviceTemperatureHumidityTimestamp = DateTime.UtcNow;
            }
            else
            {
                this._lastDeviceTemperatureHumidityTimestamp = DateTime.MinValue;
            }
            this._lastDeviceTemperature = temperature;
            this._lastDeviceHumidity = relativeHumidity;
        }

        void timer_Tick(GT.Timer timer)
        {
            timer.Stop();
            timer.Interval = _targetTelemetryInterval;

            OBDIIReader.OBDIIData obdiiData = null;
            if (this.OBDReader.InitializeObd())
            {
                obdiiData = this.OBDReader.ReadObdIIData();
                UpdateDisplay(obdiiData);
            }

#if !MF_EMULATOR
            this.temperatureHumidity.RequestMeasurement();
#endif

#if SB
            if (this.TimeHelper.UpdateTime())
            {
                GetDeviceProvisioningInfo();
                if (this.SBSender == null)
                {
                    // TODO: Provision SB credentials properly
                    this.SBSender = new AmqpSender(this, this.deviceInfo.baseUri, this.deviceInfo.keyName, this.deviceInfo.keyValue, this.deviceInfo.OutboxEntity);
                    this.SBSender.StartSender();
#if !OrderedMessagingDemo
                    if (this.SBReceiver == null)
                    {
                        // TODO Use the AMQP session and connection of the sender to minimize socket usage/SSL overhead on both client and service
                        this.SBReceiver = new AmqpReceiver(this, this.deviceInfo.baseUri, this.deviceInfo.keyName, this.deviceInfo.keyValue, this.deviceInfo.InboxEntity, OnMessageCallback);
                        //this.SBReceiver = new AmqpReceiver(this, this.SBSender, this.deviceInfo.InboxEntity, OnMessageCallback);

                        this.SBReceiver.SenderForLatencies = this.SBSender;
                        this.SBReceiver.DeviceIdForLatencies = this.deviceInfo.DeviceId.ToString();
                        this.SBReceiver.Start();
                    }
#endif
                }
                else
                {
                    SendOBDToSB(obdiiData);

                    if (this._lastDeviceTemperatureHumidityTimestamp > DateTime.MinValue)
                    {
                        SendTemperatureToDB(this.deviceInfo.DeviceId, this._lastDeviceTemperatureHumidityTimestamp, this._lastDeviceTemperature, this._lastDeviceHumidity);
                    }
                }
            }
#endif
            timer.Start();
        }

        DateTime _showAlertUntil = DateTime.MinValue;
        string _alertText = "";
        void UpdateDisplay(OBDIIReader.OBDIIData obdiiData)
        {
            if (_showAlertUntil < DateTime.UtcNow)
            {
                string time = obdiiData.Time.ToString("hh:mm:ss");
#if !MF_EMULATOR
                _text1.TextContent = " " + time + " Speed: " + obdiiData.VehicleSpeed;
                _text2.TextContent = " RPM: " + obdiiData.RPM;
                _text3.TextContent = " Temp: " + obdiiData.AmbientAirTemp;
                _text4.TextContent = " Fuel: " + obdiiData.FuelLevel; // 01 2F
                _text5.TextContent = " Fuel Type: " + obdiiData.VehicleFuelType;
                _text6.TextContent = " VIN: " + obdiiData.VIN;
                _text7.TextContent = " Battery Voltage: " + obdiiData.BatteryVoltage;
                    _text8.TextContent = " Protocol Type: " + obdiiData.OBDProtocolType;
                _text9.TextContent = " Intake Air Temp: " + obdiiData.IntakeAirTemp;
                _text10.TextContent = " Throttle position: " + obdiiData.ThrottlePosition;
#endif
            }
            else
            {
                this._text1.TextContent = "";
                this._text2.TextContent = "";
                this._text3.TextContent = "";
                this._text4.TextContent = "";
                this._text5.TextContent = "  " + _alertText;
                this._text6.TextContent = "";
                this._text7.TextContent = "";
                this._text8.TextContent = "";
                this._text9.TextContent = "";
                this._text10.TextContent = "";

                this._timer.Interval = _targetTelemetryInterval;
            }
        }

#if SB
        void OnMessageCallback(ReceiverLink receiver, Message message)
        {
            this.SBReceiver.LogMessage(message);
            try
            {
                bool bAccept = true;
                switch (message.Properties.Subject)
                {
                    case "diagctrl":
                        {
                            var logMessageLatencies = message.ApplicationProperties["LogMessageLatencies"];
                            if (logMessageLatencies != null)
                            {
                                if (logMessageLatencies is bool)
                                {
                                    this.SBReceiver.LogMessageLatencies = (bool)logMessageLatencies;
                                    TraceLog("diagctrl command: log latency is now " + this.SBReceiver.LogMessageLatencies);
                                }
                                else
                                {
                                    TraceLog("Invalid diagctrl command: LogMessageLatencies property not of type bool");
                                    this.SBReceiver.RejectMessage(message);
                                    bAccept = false;
                                }
                            }

                            var sendMessageLatencies = message.ApplicationProperties["SendMessageLatencies"];
                            if (sendMessageLatencies != null)
                            {
                                if (sendMessageLatencies is bool)
                                {
                                    this.SBReceiver.SendMessageLatencies = (bool)sendMessageLatencies;
                                    TraceLog("diagctrl command: latency send is now " + this.SBReceiver.SendMessageLatencies);
                                }
                                else
                                {
                                    TraceLog("Invalid diagctrl command: SendMessageLatencies property not of type bool");
                                    this.SBReceiver.RejectMessage(message);
                                    bAccept = false;
                                }
                            }

                            var logMessageProperties = message.ApplicationProperties["LogMessageProperties"];
                            if (logMessageProperties != null)
                            {
                                if (logMessageProperties is bool)
                                {
                                    this.SBReceiver.LogMessageProperties = (bool)logMessageProperties;
                                    TraceLog("diagctrl command: log message properteis is now " + this.SBReceiver.LogMessageProperties);
                                }
                                else
                                {
                                    TraceLog("Invalid diagctrl command: LogMessageProperties property not of type bool");
                                    this.SBReceiver.RejectMessage(message);
                                    bAccept = false;
                                }
                            }

                            var traceAllToServiceBus = message.ApplicationProperties["TraceAll"];
                            if (traceAllToServiceBus != null)
                            {
                                if (traceAllToServiceBus is bool)
                                {
                                    this.bTraceAllToServiceBus = (bool)traceAllToServiceBus;
                                    TraceLog("diagctrl command: trace all to SB is now " + this.bTraceAllToServiceBus);
                                }
                                else
                                {
                                    TraceLog("Invalid diagctrl command: TraceAll property not of type bool");
                                    this.SBReceiver.RejectMessage(message);
                                    bAccept = false;
                                }
                            }

                            break;
                        }
                    case "txtalrt":
                        {
                            string alertText = (string)message.ApplicationProperties["Text"];
                            TraceLog(alertText);
                            _alertText = alertText;
                            _showAlertUntil = DateTime.UtcNow + new TimeSpan(0, 0, 20);
                            
                            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);

                            Message responseMessage = new Message();

                            responseMessage.Properties = new Amqp.Framing.Properties();
                            responseMessage.Properties.Subject = "txtalrtResponse";

                            responseMessage.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                            responseMessage.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = this.deviceInfo.DeviceId;
                            responseMessage.ApplicationProperties["RelatesTo"] = message.Properties.MessageId; // Ties the response back to the request
                            responseMessage.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = DateTime.UtcNow;
                            responseMessage.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

                            this.SBSender.SendOrEnqueueMessage(responseMessage);

                            break;
                        }
                    case "tlmctrl":
                        {
                            var freq = message.ApplicationProperties["FrequencyInSeconds"];
                            if (freq != null && freq is int)
                            {
                                this._targetTelemetryInterval = new TimeSpan(((int)freq) * 10000 * 1000);
                                this._timer.Interval = _targetTelemetryInterval;
                                TraceLog("tlmctrl command: sample frequency is now " + this._targetTelemetryInterval);

                                Message responseMessage = new Message();

                                responseMessage.Properties = new Amqp.Framing.Properties();
                                responseMessage.Properties.Subject = "tlmctrlResponse";

                                responseMessage.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                                responseMessage.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = this.deviceInfo.DeviceId;
                                responseMessage.ApplicationProperties["RelatesTo"] = message.Properties.MessageId; // Ties the reply back to the request
                                responseMessage.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = DateTime.UtcNow;
                                responseMessage.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

                                this.SBSender.SendOrEnqueueMessage(responseMessage);
                            
                            }
                            else
                            {
                                TraceLog("Invalid tlmctrl command: no FrequencyInSeconds property or property not of type long");
                                this.SBReceiver.RejectMessage(message);
                                bAccept = false;
                            }
                            break;
                        }
                    case "takepict":
                        this.TakePicture();
                        break;
                }
                if (bAccept)
                {
                    this.SBReceiver.AcceptMessage(message);
                }
            }
            catch (Exception e)
            {
                TraceLog("Error processing messsage: " + e.Message);
                // Leave message locked to avoid immediate resend and rely on SB's retry count to dead letter etc.
                // TODO distinguish between transient failures (allow retry) and permanent failures (bad message etc.)
                // Call receiver.Reject(message); for permanent failures
                // Call receiver.Release(message); for transient failures that should be retried immediately
                // Or do nothing to transient failures that should only be retried after the message lock expired
            }
        }

        public void SendOBDToSB(OBDIIReader.OBDIIData obdiiData)
        {
            if (bSBViaHttp && this.messagingClient != null)
            {
                try
                {
                    this.TraceLog("Sending OBDData to SB via HTTP");

                    SimpleMessage message = new SimpleMessage()
                    {
                        BrokerProperties = { { "SessionId", obdiiData.VIN }, { "Label", "OBDData" } },
                        Properties = 
                            { 
                                { AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId, this.deviceInfo.DeviceId },
                                { AmqpSendReceiveBase.strPropertyEventTimestamp, obdiiData.Time },

                                { "VIN", obdiiData.VIN},
                                { "Speed", obdiiData.VehicleSpeed },
                                { "RPM",  obdiiData.RPM},
                                { "ThrottlePosition", obdiiData.ThrottlePosition},
                                { "AmbientAirTemp", obdiiData.AmbientAirTemp},
                                { "IntakeAirTemp", obdiiData.IntakeAirTemp},
                                { "DistancePerGallon", obdiiData.DistancePerGallon },
#if MyMode01
                                { "EngineFuelRate", obdiiData.EngineFuelRate },
#endif
                            },
                    };
                    this.messagingClient.Send(message);

                    this.TraceLog("Sent OBDData to SB via HTTP");
                }
                catch (Exception e)
                {
                    this.TraceLog("Failure sending OBDData to SB via HTTP: " + e.Message);
                }
            }
            if (bSBViaAmqp && this.SBSender != null)
            {
                try
                {
#if !OrderedMessagingDemo
                    this.TraceLog("Sending OBDData to SB via AMQP");

                    Message message = new Message();

                    message.Properties = new Amqp.Framing.Properties();
                    message.Properties.Subject = "OBDData";
                    message.Properties.GroupId = obdiiData.VIN;

                    message.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = this.deviceInfo.DeviceId;
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = obdiiData.Time;
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

                    message.ApplicationProperties["VIN"] = obdiiData.VIN;
                    message.ApplicationProperties["Speed"] = obdiiData.VehicleSpeed;
                    message.ApplicationProperties["RPM"] = obdiiData.RPM;
                    message.ApplicationProperties["ThrottlePosition"] = obdiiData.ThrottlePosition;
                    message.ApplicationProperties["AmbientAirTemp"] = obdiiData.AmbientAirTemp;
                    message.ApplicationProperties["IntakeAirTemp"] = obdiiData.IntakeAirTemp;
                    message.ApplicationProperties["DistancePerGallon"] = obdiiData.DistancePerGallon;
                    message.ApplicationProperties["BatteryVoltage"] = obdiiData.BatteryVoltage;
#if MyMode01
                    message.ApplicationProperties["EngineFuelRate"]= obdiiData.EngineFuelRate;
#endif

                    this.SBSender.SendOrEnqueueMessage(message);

#else //if OrderedMessagingDemo // Use in demos to if ordering of messages from the same sender/partition are preserved
                    this.TraceLog("Sending Sentence to SB via AMQP");
                    string text = "Vehicle " + obdiiData.VIN + " going at " + obdiiData.VehicleSpeed + " mph with " + obdiiData.RPM + " RPM at " + obdiiData.Time.ToString();
                    foreach (var word in text.Split(' '))
                    {
                        var tmessage = new Message(word);
                        tmessage.Properties = new Amqp.Framing.Properties();
                        tmessage.Properties.Subject = "Sentences";
                        tmessage.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                        tmessage.ApplicationProperties["PartitionKey"] = obdiiData.VIN;

                        this.SBSender.SendOrEnqueueMessage(tmessage);
                    }
#endif
                }
                catch (Exception e)
                {
                    this.TraceLog("Failure sending OBDData to SB via AMQP: " + e.Message);
                }
            }
        }

        public void SendTemperatureToDB(int deviceId, DateTime timestamp, double temperature, double humidity)
        {
            if (bSBViaHttp && messagingClient != null)
            {
                try
                {
                    this.TraceLog("Sending Temperature data to SB via HTTP");
                    SimpleMessage message = new SimpleMessage()
                    {
                        BrokerProperties = { { "SessionId", deviceId }, { "Label", "Temperature" } },
                        Properties = { 
                                { AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId, deviceId },
                                { AmqpSendReceiveBase.strPropertyEventTimestamp, timestamp },
                                
                                { "DeviceTemperature", temperature },
                                { "DeviceRelativeHumidity", humidity },

                            },
                    };
                    this.messagingClient.Send(message);
                    this.TraceLog("Sent Temperature data to SB via HTTP");
                }
                catch (Exception e)
                {
                    this.TraceLog("Failure sending Temperature to SB via HTTP: " + e.Message);
                }
            }
            if (bSBViaAmqp && this.SBSender != null)
            {
                try
                {
                    this.TraceLog("Sending Temperature to SB via AMQP");

                    Message message = new Message();

                    message.Properties = new Amqp.Framing.Properties();
                    message.Properties.Subject = "Temperature";
                    message.Properties.GroupId = deviceId.ToString();

                    message.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = deviceId;
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = timestamp;
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

                    message.ApplicationProperties["DeviceTemperature"] = temperature;
                    message.ApplicationProperties["DeviceRelativeHumidity"] = humidity;

                    this.SBSender.SendOrEnqueueMessage(message);
                }
                catch (Exception e)
                {
                    this.TraceLog("Failure sending Temperature to SB via AMQP: " + e.Message);
                }
            }
        }

        public void SendPictureToSB(int deviceId, GT.Picture picture)
        {
            if (this.bSBViaAmqp)
            {
                try
                {
                    this.TraceLog("Sending picture to SB via AMQP. Size: "+picture.PictureData.Length);

                    //byte[] temp = new byte[100000];
                    //var pict = picture.PictureData;
                    //for (int i = 0; i < temp.Length; i++ )
                    //{
                    //    temp[i] =pict[i];
                    //}
                    //Message message = new Message(temp);

                    Message message = new Message(picture.PictureData);

                    message.Properties = new Amqp.Framing.Properties();
                    message.Properties.Subject = "picture";
                    message.Properties.GroupId = deviceId.ToString();

                    message.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                    message.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = deviceId;
                    //message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = DateTime.UtcNow;
                    //message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

                    this.SBSender.SendOrEnqueueMessage(message);
                }
                catch (Exception e)
                {
                    this.TraceLog("Failure sending picture to SB via AMQP: " + e.Message);
                }

            }
            if (this.bSBViaHttp)
            {
                if (messagingClient != null)
                {
                    try
                    {
                        SimpleMessage message = new SimpleMessage
                        {
                            BrokerProperties = { { "SessionId", "picture" }, { "Label", "Picture" } },
                            Properties = 
                        { 
                            { AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId, deviceId },
                            { AmqpSendReceiveBase.strPropertyEventTimestamp, DateTime.UtcNow },
                        },
                            BodyStream = new System.IO.MemoryStream(picture.PictureData),
                        };

                        this.TraceLog("Sending picture to SB");
                        try
                        {
                            messagingClient.Send(message);
                        }
                        catch (Exception ex)
                        {
                            this.TraceLog("Error sending picture to SB: " + ex.Message);
                        }
                        this.TraceLog("Sent picture to SB");
                    }
                    catch (Exception e)
                    {
                        this.TraceLog("Failure sending picture to SB: " + e.Message);
                    }

                }
                else
                {
                    this.TraceLog("Not ready to send picture to SB");
                }
            }
        }

        public void SendInitToSB(int deviceId)
        {
            if (bSBViaHttp)
            {
                try
                {
                    if (messagingClient != null)
                    {
                        this.TraceLog("Sending init message to SB");

                        this.messagingClient.Send(
                                  new SimpleMessage()
                                  {
                                      BrokerProperties = { { "SessionId", "Session001" } },
                                      Properties = { 
                                        { "DeviceId", deviceId },
                                        { "Time", DateTime.UtcNow },
                                        { "Init", DateTime.UtcNow },
                                      },
                                  });
                        this.TraceLog("Sent init message to SB");
                    }
                }
                catch (Exception ex)
                {
                    this.TraceLog("Error sending init message to SB: " + ex.Message);
                }
            }
        }
#endif

        void button_ButtonPressed(Button sender, Button.ButtonState state)
        {
#if SB
            Message message = new Message();

            message.Properties = new Amqp.Framing.Properties();
            message.Properties.Subject = "PanicButtonPressed";

            message.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
            message.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = this.deviceInfo.DeviceId;
            message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = DateTime.UtcNow;
            message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

            this.SBSender.SendOrEnqueueMessage(message);
#endif
        }

        void TakePicture()
        {
            if (this.TimeHelper.UpdateTime())
            {
                //Thread t2 = new Thread(GetDataFromWeb);
                //t2.Start();

                TraceLog("Taking picture");
                try
                {
                    this.camera.TakePicture();
                }
                catch (Exception e)
                {
                    TraceLog("Failure taking picture: " + e.Message);
                }

            }
        }

        void camera_PictureCaptured(Camera sender, GT.Picture picture)
        {
            TraceLog("Picture captured");
#if SB
            SendPictureToSB(this.deviceInfo.DeviceId, picture);
#endif
        }

#if SB
        MessagingClient _messagingClient = null;
        MessagingClient messagingClient
        {
            get
            {
                if (_messagingClient == null)
                {
                    // TODO: Provision SB credentials properly
                    //TokenProvider tp = new TokenProvider("owner", "Hv6RhvOkZSpNnpaesI7N0Pj/KvJRMjW/xH/SCTC35U8=");

                    //Endpoint=sb://markush-sn1.servicebus.windows.net/;SharedAccessKeyName=SendTestTopic01;SharedAccessKey=Ec2ZrCMoTjH7WXo29aRt2uVkX8ZK5MTtIC3RMSFrVAQ=
                    //SASTokenProvider tp = new SASTokenProvider("SendTestTopic01", "Ec2ZrCMoTjH7WXo29aRt2uVkX8ZK5MTtIC3RMSFrVAQ=");
                    SASTokenProvider tp = new SASTokenProvider("SendFromPhone", "W3FaV1we8zjqu2M0thpTM4+CY/Oa+MxusyJsRpvqdYI=");

                    _messagingClient = new MessagingClient(new Uri("https://markushdgwus0001test.servicebus.windows.net/in/t0000"), tp);
                }
                return _messagingClient;
            }
        }
#endif

        // Not really used, but useful when developing/debugging to determine if network/SSL/Time is working properly
        void GetDataFromWeb()
        {
            try
            {
#if SB
                if (this.TimeHelper.UpdateTime())
                {
                    SendInitToSB(this.deviceInfo.DeviceId);
                }
#endif

                TraceLog("Web: Requesting data");
                System.Net.HttpWebRequest rq = HttpWebRequest.Create("https://markush-sn1.servicebus.windows.net/testtopic01") as HttpWebRequest; //http://www.microsoft.com") 

                X509Certificate[] caCerts = 
                    new X509Certificate[] { 
                        new X509Certificate(Resources.GetBytes(Resources.BinaryResources.gte)),
                        new X509Certificate(Resources.GetBytes(Resources.BinaryResources.mia)),
                        new X509Certificate(Resources.GetBytes(Resources.BinaryResources.mssi)),
                    };
                rq.HttpsAuthentCerts = caCerts;
                rq.KeepAlive = true;

                using (HttpWebResponse resp = (HttpWebResponse)rq.GetResponse())
                {

                    TraceLog("Web Status: " + resp.StatusDescription);

                    using (var dataStream = resp.GetResponseStream())
                    {

                        using (var reader = new StreamReader(dataStream))
                        {

                            string responseText = reader.ReadToEnd();

                            if (responseText.Length > 100)
                            {
                                responseText = responseText.Substring(0, 100);
                            }

                            TraceLog("Web Response: "+responseText);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                TraceLog("Web Error: " + ex.Message);
            }
        }

        public void TraceLog(string text)
        {
            TraceLog(text, true
#if SB
                ,this.bTraceAllToServiceBus
#endif
                );
        }

        public void TraceLog(string text, bool bShowOnDisplay
#if SB
                                                    , bool bSendToSB
#endif
            )
        {
            string logText = DateTime.UtcNow + "[" + Thread.CurrentThread.ManagedThreadId + "] " + text;
            Debug.Print(logText);
            if (bShowOnDisplay && _textLong != null)
            {
                this.display_T35.WPFWindow.Dispatcher.Invoke(TimeSpan.MaxValue,
                    (textArg) =>
                    {
                        var newText = logText + "\n" + _textLong.TextContent;
                        if (newText.Length > 1000)
                        {
                            newText = newText.Substring(0, 1000);
                        }

                        _textLong.TextContent = newText;
                        return newText;
                    }, logText);
            }
#if SB
            if (
                this.bTraceAllToServiceBus && 
                bSendToSB)
            {
                this.TraceLogToSb(text);
            }
#endif
        }

#if SB
        public void TraceLogToSb(string text)
        {
            if (this.SBSender != null)
            {
                Message message = new Message();

                message.Properties = new Amqp.Framing.Properties();
                message.Properties.Subject = AmqpSendReceiveBase.strMsgSubjectTrace;
                message.Properties.GroupId = this.deviceInfo.DeviceId.ToString();

                message.ApplicationProperties = new Amqp.Framing.ApplicationProperties();
                message.ApplicationProperties[AmqpSendReceiveBase.strPropertyDeviceGatewayDeviceId] = this.deviceInfo.DeviceId;
                message.ApplicationProperties[AmqpSendReceiveBase.strPropertyEventTimestamp] = DateTime.UtcNow;
                message.ApplicationProperties[AmqpSendReceiveBase.strPropertySendTimestamp] = DateTime.UtcNow;

                message.ApplicationProperties["Text"] = text;

                this.SBSender.SendOrEnqueueMessage(message);
            }
        }
#endif
    }

}
