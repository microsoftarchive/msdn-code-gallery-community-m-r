//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.Media.Protection.PlayReady;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

using PlayReadyUAP;

namespace PlayReadyUAP
{

    public class Metering : ServiceRequest
    {
        byte[] _meteringCert = null;

        public byte[] GetMeteringCertificate()
        {
            return _meteringCert;
        }

        public void SetMeteringCertificate(byte[] meteringCert)
        {
            _meteringCert = meteringCert;
        }
        protected virtual void MeteringServiceRequestCompleted( PlayReadyMeteringReportServiceRequest  sender, Exception hrCompletionStatus )
        {
            TestLogger.LogImportantMessage("MeteringServiceRequestCompleted");

            if (hrCompletionStatus != null)
            {
                TestLogger.LogError("MeteringServiceRequestCompleted failed with " + hrCompletionStatus.HResult);
            }
        }

        void HandleIndivServiceRequest_Finished(bool bResult, object resultContext)
        {
            TestLogger.LogMessage("Enter Revocation.HandleIndivServiceRequest_Finished()");

            TestLogger.LogMessage("HandleIndivServiceRequest_Finished(): " + bResult.ToString());

            if (bResult)
            {
                MeteringReportProactively();
            }

            TestLogger.LogMessage("Leave Revocation.HandleIndivServiceRequest_Finished()");
        }

        public void  MeteringReportProactively()
        {
            TestLogger.LogMessage("Enter Metering.MeteringReportProactively()" );
            try
            {
                TestLogger.LogMessage("Creating metering report service request...");
                PlayReadyMeteringReportServiceRequest meteringRequest = new PlayReadyMeteringReportServiceRequest();
                MeteringReportReactively(meteringRequest);
            }
            catch (Exception ex)
            {
                if (ex.HResult == ServiceRequest.MSPR_E_NEEDS_INDIVIDUALIZATION)
                {
                    PlayReadyIndividualizationServiceRequest indivServiceRequest = new PlayReadyIndividualizationServiceRequest();

                    RequestChain requestChain = new RequestChain(indivServiceRequest);
                    requestChain.FinishAndReportResult(new ReportResultDelegate(HandleIndivServiceRequest_Finished));
                }
                else
                {
                    TestLogger.LogImportantMessage("MeteringReportProactively failed:" + ex.HResult);
                }
            }

            TestLogger.LogMessage("Leave Metering.MeteringReportProactively()" );
        }

        void ConfigureServiceRequest()
        {
            PlayReadyMeteringReportServiceRequest meteringRequest = _serviceRequest as PlayReadyMeteringReportServiceRequest;
            
            TestLogger.LogMessage(" " );
            TestLogger.LogMessage("Configure metering request to these values:" );
            if( RequestConfigData.Uri != null )
            {
                TestLogger.LogMessage("URL       :" + RequestConfigData.Uri.ToString() );
                meteringRequest.Uri = RequestConfigData.Uri;
            }
            
            if( RequestConfigData.ChallengeCustomData != null && RequestConfigData.ChallengeCustomData != String.Empty )
            {
                TestLogger.LogMessage("ChallengeCustomData:" + RequestConfigData.ChallengeCustomData );
                meteringRequest.ChallengeCustomData = RequestConfigData.ChallengeCustomData;
            }

            meteringRequest.MeteringCertificate = GetMeteringCertificate();

            TestLogger.LogMessage(" ");
        }
        
        async public void  MeteringReportReactively(PlayReadyMeteringReportServiceRequest meteringRequest)
        {
            TestLogger.LogMessage("Enter Metering.MeteringReportReactively()" );
            Exception exception = null;
            
            try
            {   
                _serviceRequest = meteringRequest;
                ConfigureServiceRequest();

                TestLogger.LogMessage("ChallengeCustomData = " + meteringRequest.ChallengeCustomData);
                if( RequestConfigData.ManualEnabling )
                {
                    TestLogger.LogMessage("Manually posting the request..." );
                    
                    HttpHelper httpHelper = new HttpHelper( meteringRequest );
                    await httpHelper.GenerateChallengeAndProcessResponse();
                }
                else
                {
                    TestLogger.LogMessage("Begin metering service request..." );
                    await meteringRequest.BeginServiceRequest();
                }
            }
            catch( Exception ex )
            {
                TestLogger.LogMessage("Saving exception.." );
                exception = ex;
            }
            finally
            {
                TestLogger.LogMessage("Post-Metering Values:");
                if( exception == null )
                {
                    TestLogger.LogMessage("ResponseCustomData = " + meteringRequest.ResponseCustomData);
                    TestLogger.LogMessage("ProtectionSystem   = " + meteringRequest.ProtectionSystem.ToString());
                    TestLogger.LogMessage("Type = " + meteringRequest.Type.ToString());
                }
                
                MeteringServiceRequestCompleted( meteringRequest, exception );
            }
            
            TestLogger.LogMessage("Leave Metering.MeteringReportReactively()" );
        }
        
    }

    public class MeteringAndReportResult : Metering
    {
        ReportResultDelegate _reportResult = null;
        bool _bExpectError = false;
        uint  _expectedPlayCount = 0;
        uint _actualPlayCount = 0;
        public uint PlayCount
        {
            set { this._actualPlayCount = value; }
            get { return this._actualPlayCount; }
        }  

        public MeteringAndReportResult( ReportResultDelegate callback, bool bExpectError , uint expectedPlayCount )
        {
            _reportResult = callback;
            _bExpectError = bExpectError;
            _expectedPlayCount = expectedPlayCount;
        }
        
        protected override void MeteringServiceRequestCompleted( PlayReadyMeteringReportServiceRequest  meteringRequest, Exception hrCompletionStatus )
        {
            TestLogger.LogMessage("Enter MeteringAndReportResult.MeteringServiceRequestCompleted()" );

            if( hrCompletionStatus == null )
            {
                string strMeteringReportXml = XmlConvert.DecodeName( meteringRequest.ResponseCustomData );
                TestLogger.LogMessage("Metering report Xml = " + strMeteringReportXml);

                uint actualPlayCount = 0;
                bool bFound = false;

                if(strMeteringReportXml.Contains("meteringRecord"))
                {
                    //ResponseCustomData format on server http://playready.directtaps.net
                    string [] dataList = strMeteringReportXml.Split(' ');
                    foreach (var data in dataList)
                    {
                        if (data.Contains("Play:"))
                        {
                            bFound = true;
                            string strplayCount = data.Trim().Substring(5);
                            actualPlayCount = Convert.ToUInt32(Regex.Match(strplayCount, @"\d+").Value);
                        }
                    }
                }
                else
                {
                    //otherwise, ResponseCustomData format on server http://capprsvr05/I90playreadymain/rightsmanager.asmx
                    XElement xElement = XElement.Parse(strMeteringReportXml);
                    actualPlayCount = (from item in xElement.Descendants("Action")
                                      where (string)item.Attribute("Name") == "Play"
                                      select (uint)item.Attribute("Value")
                                        ).First();
                    bFound = true;
                }

                if (!bFound)
                {
                    throw new Exception("unrecoganized meteringRequest.ResponseCustomData");
                }

                PlayCount = actualPlayCount;

                if (actualPlayCount == _expectedPlayCount)
                {
                    TestLogger.LogMessage("Actual PlayCount = " + actualPlayCount + " from  metering processed report.");
                    TestLogger.LogMessage("************************************    MeteringReport succeeded       ****************************************");
                   _reportResult( true, null );
                }
                else
                {
                    TestLogger.LogMessage("!!!!!!Actual PlayCount = " + actualPlayCount + "but expected = " + _expectedPlayCount);
                   _reportResult( false, null );
                }
            }
            else
            {
                if( PerformEnablingActionIfRequested(hrCompletionStatus) || HandleExpectedError(hrCompletionStatus))
                {
                    TestLogger.LogMessage( "Exception handled." );
                }
                else
                {
                    TestLogger.LogError( "MeteringServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                   _reportResult( false, null );
                }
            }
                
            TestLogger.LogMessage("Leave MeteringAndReportResult.MeteringServiceRequestCompleted()" );
        }
        
        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter MeteringAndReportResult.EnablingActionCompleted()" );

            _reportResult( bResult, null );
            
            TestLogger.LogMessage("Leave MeteringAndReportResult.EnablingActionCompleted()" );
        }

        protected override bool HandleExpectedError(Exception ex)
        {
            TestLogger.LogMessage("Enter MeteringAndReportResult.HandleExpectedError() _bExpectError=" + _bExpectError );
            bool bHandled = false;
            
            if( _bExpectError )
            {
                TestLogger.LogMessage(" ex.HResult= " + ex.HResult );
                if ( ex.HResult == MSPR_E_NO_METERING_DATA_AVAILABLE )
                {
                    TestLogger.LogMessage("MeteringAndReportResult.HandleExpectedError : Received MSPR_E_NO_METERING_DATA_AVAILABLE as expected" );
                    bHandled = true;
                    _reportResult( true, null );
                }
            }
            
            TestLogger.LogMessage("Leave MeteringAndReportResult.HandleExpectedError()" );
            return bHandled;
        }   
    }
}
