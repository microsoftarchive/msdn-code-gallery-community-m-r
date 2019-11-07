//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using Windows.Foundation;
using Windows.Media.Protection;
using Windows.Media.Protection.PlayReady;
using PlayReadyUAP;
using PlayReadyUAP.Data;

namespace PlayReadyUAP
{
    public class LicenseAcquisition : ServiceRequest
    {
        private bool bPersistent = false;
        private PlayReadyLicenseSession licenseSession;

        public bool Persistent
        {
            set { this.bPersistent = value; }
            get { return this.bPersistent; }
        }

        protected virtual void LAServiceRequestCompleted( IPlayReadyLicenseAcquisitionServiceRequest  sender, Exception hrCompletionStatus )
        {
        }

        void HandleIndivServiceRequest_Finished(bool bResult,object resultContext)
        {
            TestLogger.LogMessage("Enter LicenseAcquisition.HandleIndivServiceRequest_Finished()");

            TestLogger.LogMessage("HandleIndivServiceRequest_Finished(): " + bResult.ToString());
            if (bResult)
            {
                AcquireLicenseProactively();
            }

            TestLogger.LogMessage("Leave LicenseAcquisition.HandleIndivServiceRequest_Finished()");
        }

        public PlayReadyLicenseSession createLicenseSession ()
        {
            TestLogger.LogMessage("Enter createLicenseSession");

            //A setting to tell MF that we are using PlayReady.
           var propSet = new Windows.Foundation.Collections.PropertySet();
            propSet["Windows.Media.Protection.MediaProtectionSystemId"] = "{F4637010-03C3-42CD-B932-B48ADF3A6A54}";

            var cpsystems = new Windows.Foundation.Collections.PropertySet();
            cpsystems["{F4637010-03C3-42CD-B932-B48ADF3A6A54}"] = "Windows.Media.Protection.PlayReady.PlayReadyWinRTTrustedInput"; //Playready TrustedInput Class Name
            propSet["Windows.Media.Protection.MediaProtectionSystemIdMapping"] = cpsystems;

            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (localSettings.Containers.ContainsKey("PlayReady") &&
                localSettings.Containers["PlayReady"].Values.ContainsKey("SoftwareOverride"))
            {
                int UseSoftwareProtectionLayer = (int)localSettings.Containers["PlayReady"].Values["SoftwareOverride"];

                if (UseSoftwareProtectionLayer == 1)
                {
                    TestLogger.LogMessage(" ");
                    TestLogger.LogMessage("***** Use Software Protection Layer for createLicenseSession ******");
                    propSet["Windows.Media.Protection.UseSoftwareProtectionLayer"] = true;
                }
            }

            //Create the MF media session that the license will be tied to
            var pmpServer = new Windows.Media.Protection.MediaProtectionPMPServer(propSet);

            var propSet2 = new Windows.Foundation.Collections.PropertySet();
            //Set the property for the LicenseSession. This tells PlayReady to tie the license to that particular media session
            propSet2["Windows.Media.Protection.MediaProtectionPMPServer"] = pmpServer;

            this.licenseSession = new Windows.Media.Protection.PlayReady.PlayReadyLicenseSession(propSet2);
            TestLogger.LogMessage("Exit createLicenseSession");
            return this.licenseSession;
        }
        public void configMediaProtectionManager(MediaProtectionManager mediaProtectionManager)
        {
            //This handles the proactive LA of in memory license. 
            if (this.licenseSession == null)
            {
                createLicenseSession();
            }

            //LicenseSession will set the proper setting in the media protection manager so that
            //MF knows which existing media session to use and to use PlayReady as the DRM system
            this.licenseSession.ConfigureMediaProtectionManager(mediaProtectionManager);
        }

        public void  AcquireLicenseProactively()
        {
            IPlayReadyLicenseAcquisitionServiceRequest licenseRequest;

            try
            {
                PlayReadyContentHeader contentHeader = new PlayReadyContentHeader(  0,
                                                                                    RequestConfigData.KeyIds,
                                                                                    null,
                                                                                    RequestConfigData.EncryptionAlgorithm,
                                                                                    null,
                                                                                    null,
                                                                                    String.Empty,
                                                                                    RequestConfigData.DomainServiceId);

                TestLogger.LogMessage("Creating license acquisition service request...");
                
                if(bPersistent)
                {   // persistent license
                    licenseRequest = (IPlayReadyLicenseAcquisitionServiceRequest) new PlayReadyLicenseAcquisitionServiceRequest();
                }
                else
                {
                    if (this.licenseSession == null)
                    {
                        throw new ArgumentNullException("licenseSession can not be null");
                    }

                    //in-memory license, use license session to create a license service request
                    //this way, the acquired license will be tied to the media session associated with the license session
                    licenseRequest = this.licenseSession.CreateLAServiceRequest();
                }
                licenseRequest.ContentHeader = contentHeader;
                licenseRequest.Uri = RequestConfigData.Uri;
                AcquireLicenseReactively(licenseRequest);
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
                    //TestLogger.LogImportantMessage("AcquireLicenseProactively failed:" + ex.HResult);
                    TestLogger.LogError("AcquireLicenseProactively failed:" + ex.HResult);
                    licenseRequest = (IPlayReadyLicenseAcquisitionServiceRequest)new PlayReadyLicenseAcquisitionServiceRequest();
                    LAServiceRequestCompleted(licenseRequest, ex);
                }
            }
            
        }

        static public void DumpContentHeaderValues(PlayReadyContentHeader contentHeader)
        {
            TestLogger.LogMessage(" " );
            TestLogger.LogImportantMessage("Content header values:" );
            if( contentHeader == null )
            {
                return;
            }
            TestLogger.LogMessage("CustomAttributes :" + contentHeader.CustomAttributes);
            TestLogger.LogMessage("DecryptorSetup   :" + contentHeader.DecryptorSetup.ToString());
            TestLogger.LogMessage("DomainServiceId  :" + contentHeader.DomainServiceId.ToString());
            TestLogger.LogMessage("EncryptionType   :" + contentHeader.EncryptionType.ToString());
            for (int i = 0; i < contentHeader.KeyIds.Length; i++)
            {
                TestLogger.LogImportantMessage("KeyId " + i + "       :" + contentHeader.KeyIds[i].ToString());
                TestLogger.LogImportantMessage("KeyIdString " + i + " :" + contentHeader.KeyIdStrings[i]);
            }
            
            if (contentHeader.LicenseAcquisitionUrl != null)
            {
                TestLogger.LogImportantMessage("LicenseAcquisitionUrl :" + contentHeader.LicenseAcquisitionUrl.ToString());
            }          
        }

        void ConfigureServiceRequest()
        {
            PlayReadyLicenseAcquisitionServiceRequest licenseRequest = _serviceRequest as PlayReadyLicenseAcquisitionServiceRequest;
                        
            TestLogger.LogMessage(" " );
            TestLogger.LogMessage("Configure license request to these values:" );

             if( RequestConfigData.Uri != null )
            {
                TestLogger.LogMessage("LA URL       :" + RequestConfigData.Uri.ToString() );
                licenseRequest.Uri = RequestConfigData.Uri;
            }

             if (RequestConfigData.ChallengeCustomData != null && RequestConfigData.ChallengeCustomData != String.Empty)
             {
                 TestLogger.LogMessage("ChallengeCustomData:" + RequestConfigData.ChallengeCustomData);
                 licenseRequest.ChallengeCustomData = RequestConfigData.ChallengeCustomData;
             }
            
            TestLogger.LogMessage(" " );
        }
        
        async public void  AcquireLicenseReactively(IPlayReadyLicenseAcquisitionServiceRequest licenseRequest)
        {
            TestLogger.LogImportantMessage("Enter LicenseAcquisition.AcquireLicenseReactively()" );
            Exception exception = null;
            
            try
            {   
                _serviceRequest = licenseRequest;
                ConfigureServiceRequest();
                SerivceRequestStatistics.IncLicenseAcquisitionCount();

                TestLogger.LogMessage("ChallengeCustomData = " + licenseRequest.ChallengeCustomData);
                if( RequestConfigData.ManualEnabling )
                {
                    TestLogger.LogMessage("Manually posting the request..." );
                    
                    HttpHelper httpHelper = new HttpHelper( licenseRequest );
                    await httpHelper.GenerateChallengeAndProcessResponse();
                }
                else
                {
                    TestLogger.LogImportantMessage("Begin license acquisition service request..." );
                    await licenseRequest.BeginServiceRequest();
                }

                TestLogger.LogMessage("Post-LicenseAcquisition Values:");
                TestLogger.LogMessage("DomainServiceId          = " + licenseRequest.DomainServiceId.ToString());
                DumpContentHeaderValues(licenseRequest.ContentHeader);
            }
            catch( Exception ex )
            {
                TestLogger.LogMessage("Saving exception.. " + ex.ToString() );

                exception = ex;
            }
            finally
            {
                if( exception == null )
                {
                    TestLogger.LogMessage("ResponseCustomData       = " + licenseRequest.ResponseCustomData);
                }               
                LAServiceRequestCompleted( licenseRequest, exception );
            }
            
            TestLogger.LogImportantMessage("Leave LicenseAcquisition.AcquireLicenseReactively()" );
        }
        
    }

    public class LAAndReportResult : LicenseAcquisition
    {
        ReportResultDelegate _reportResult = null;
        string _strExpectedError = null;
        SampleDataItem sampleDataItem = null;
        
        public string ExpectedError  
        {  
            set { this._strExpectedError =  value; }  
            get { return this._strExpectedError; } 
        }
        
        public LAAndReportResult( ReportResultDelegate callback)
        {
            _reportResult = callback;
        }

        public LAAndReportResult(ReportResultDelegate callback, SampleDataItem item )
        {
            sampleDataItem = item;
            _reportResult = callback;
        }

        protected override void LAServiceRequestCompleted( IPlayReadyLicenseAcquisitionServiceRequest  sender, Exception hrCompletionStatus )
        {
            TestLogger.LogMessage("Enter LAAndReportResult.LAServiceRequestCompleted()" );

            if( hrCompletionStatus == null )
            {
                TestLogger.LogImportantMessage("***License acquisition succeeded***");
               _reportResult( true, sampleDataItem);
            }
            else
            {
                if (PerformEnablingActionIfRequested(hrCompletionStatus) || HandleExpectedError(hrCompletionStatus))
                {
                    TestLogger.LogMessage("Exception handled.");
                }
                else
                {
                    TestLogger.LogError("LAServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                    TestLogger.LogError("hrCompletionStatus.HResult=" +  hrCompletionStatus.HResult.ToString());
                    _reportResult(false, sampleDataItem);
                }
            }
                
            TestLogger.LogMessage("Leave LAAndReportResult.LAServiceRequestCompleted()" );
        }
        
        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter LAAndReportResult.EnablingActionCompleted()" );

            _reportResult( bResult, sampleDataItem);
            
            TestLogger.LogMessage("Leave LAAndReportResult.EnablingActionCompleted()" );
        }

        protected override bool HandleExpectedError(Exception ex)
        {
            TestLogger.LogImportantMessage("Enter LAAndReportResult.HandleExpectedError()" );
            
            if( string.IsNullOrEmpty( _strExpectedError ) )
            {
                TestLogger.LogMessage("Setting error code to " + RequestConfigData.ExpectedLAErrorCode );
                _strExpectedError = RequestConfigData.ExpectedLAErrorCode;
            }
            
            bool bHandled = false;
            if( !string.IsNullOrEmpty(_strExpectedError) )
            {
                if ( ex.Message.ToLower().Contains( _strExpectedError.ToLower() ) )
                {
                    TestLogger.LogImportantMessage( "'" + ex.Message + "' Contains " + _strExpectedError + "  as expected" );
                    bHandled = true;
                    _reportResult( true, sampleDataItem );
                }
            }
            
            TestLogger.LogImportantMessage("Leave LAAndReportResult.HandleExpectedError()" );
            return bHandled;
        }
        
    }

}
