//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using Windows.Foundation;
using Windows.Media.Protection.PlayReady;
using PlayReadyUAP;

namespace PlayReadyUAP
{
    public class RequestChain
    {
        private IPlayReadyServiceRequest _serviceRequest = null;
        ReportResultDelegate _reportResult = null;

        IndivAndReportResult _indivAndReportResult  = null;
        LAAndReportResult _licenseAcquisition       = null;
        DomainJoinAndReportResult _domainJoinAndReportResult = null;
        DomainLeaveAndReportResult _domainLeaveAndReportResult = null;
        RevocationAndReportResult _revocationAndReportResult = null;

        ServiceRequestConfigData _requestConfigData = null;
        public ServiceRequestConfigData RequestConfigData  
        {  
            set { this._requestConfigData=  value; }  
            get { return this._requestConfigData; } 
        }

        public RequestChain( IPlayReadyServiceRequest serviceRequest)
        {
            _serviceRequest = serviceRequest;
        }

        public void FinishAndReportResult(ReportResultDelegate callback)
        {
            _reportResult = callback;
            HandleServiceRequest();
        }
        
        void HandleServiceRequest()
        {
            if( _serviceRequest is PlayReadyIndividualizationServiceRequest )
            {
                HandleIndivServiceRequest( (PlayReadyIndividualizationServiceRequest)_serviceRequest);
            }
            else if ( _serviceRequest is PlayReadyLicenseAcquisitionServiceRequest )
            {
                HandleLicenseAcquisitionServiceRequest((PlayReadyLicenseAcquisitionServiceRequest)_serviceRequest);
            }
            else if ( _serviceRequest is PlayReadyDomainJoinServiceRequest )
            {
                HandleDomainJoinServiceRequest((PlayReadyDomainJoinServiceRequest)_serviceRequest);
            }
            else if ( _serviceRequest is PlayReadyDomainLeaveServiceRequest )
            {
                HandleDomainLeaveServiceRequest((PlayReadyDomainLeaveServiceRequest)_serviceRequest);
            }
            else if ( _serviceRequest is PlayReadyRevocationServiceRequest )
            {
                HandleRevocationServiceRequest((PlayReadyRevocationServiceRequest)_serviceRequest);
            }
            else
            {
                TestLogger.LogError("ERROR: Unsupported serviceRequest " + _serviceRequest.GetType() );
            }
        }
        
        void HandleServiceRequest_Finished(bool bResult, object resultContext)
        {
            TestLogger.LogMessage("Enter RequestChain.HandleServiceRequest_Finished()" );
            
            _reportResult( bResult, null );
            
            TestLogger.LogMessage("Leave RequestChain.HandleServiceRequest_Finished()" );
        }

        void HandleIndivServiceRequest(PlayReadyIndividualizationServiceRequest serviceRequest)
        {
            TestLogger.LogMessage(" " );
            TestLogger.LogMessage("Enter RequestChain.HandleIndivServiceRequest()" );
            
            _indivAndReportResult = new IndivAndReportResult( new ReportResultDelegate(HandleServiceRequest_Finished));
            _indivAndReportResult.RequestConfigData = _requestConfigData;
            _indivAndReportResult.IndivReactively( serviceRequest );
            
            TestLogger.LogMessage("Leave RequestChain.HandleIndivServiceRequest()" );
        }

        void HandleLicenseAcquisitionServiceRequest(PlayReadyLicenseAcquisitionServiceRequest serviceRequest)
        {
            TestLogger.LogMessage(" " );
            TestLogger.LogMessage("Enter RequestChain.HandleLicenseAcquisitionServiceRequest()" );
            
            _licenseAcquisition = new LAAndReportResult( new ReportResultDelegate(HandleServiceRequest_Finished));
            _licenseAcquisition.RequestConfigData = _requestConfigData;
            _licenseAcquisition.AcquireLicenseReactively( serviceRequest);

            TestLogger.LogMessage("Leave RequestChain.HandleLicenseAcquisitionServiceRequest()" );
        }
        void HandleDomainJoinServiceRequest(PlayReadyDomainJoinServiceRequest serviceRequest)
        {
            TestLogger.LogMessage(" ");
            TestLogger.LogMessage("Enter RequestChain.HandleDomainJoinServiceRequest()");

            _domainJoinAndReportResult = new DomainJoinAndReportResult(new ReportResultDelegate(HandleServiceRequest_Finished));
            _domainJoinAndReportResult.RequestConfigData = _requestConfigData;
            _domainJoinAndReportResult.DomainJoinReactively(serviceRequest);

            TestLogger.LogMessage("Leave RequestChain.HandleDomainJoinServiceRequest()");
        }

        void HandleDomainLeaveServiceRequest(PlayReadyDomainLeaveServiceRequest serviceRequest)
        {
            TestLogger.LogMessage(" ");
            TestLogger.LogMessage("Enter RequestChain.HandleDomainLeaveServiceRequest()");

            _domainLeaveAndReportResult = new DomainLeaveAndReportResult(new ReportResultDelegate(HandleServiceRequest_Finished));
            _domainLeaveAndReportResult.RequestConfigData = _requestConfigData;
            _domainLeaveAndReportResult.DomainLeaveReactively(serviceRequest);

            TestLogger.LogMessage("Leave RequestChain.HandleDomainLeaveServiceRequest()");
        }

        void HandleRevocationServiceRequest(PlayReadyRevocationServiceRequest serviceRequest)
        {
            TestLogger.LogMessage(" ");
            TestLogger.LogMessage("Enter RequestChain.HandleRevocationServiceRequest()");

            _revocationAndReportResult = new RevocationAndReportResult(new ReportResultDelegate(HandleServiceRequest_Finished));
            _revocationAndReportResult.RequestConfigData = _requestConfigData;
            _revocationAndReportResult.HandleRevocationReactively(serviceRequest);

            TestLogger.LogMessage("Leave RequestChain.HandleRevocationServiceRequest()");
        }
    }
}
