//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

using Windows.Foundation.Collections;
using System.Text;

using Windows.Foundation;
using Windows.Media.Protection;
using Windows.Media.Protection.PlayReady;
using PlayReadyUAP;

namespace PlayReadyUAP
{

    sealed public class SerivceRequestStatistics
    {
        static private uint IndivCount = 0;
        static private uint LicenseAcquisitionCount = 0;
        
        private SerivceRequestStatistics() { }

        static public void Reset()
        {
            IndivCount = 0;
            LicenseAcquisitionCount = 0;
        }
        static public void IncIndivCount()
        {
            IndivCount++;
        }

        static public void IncLicenseAcquisitionCount()
        {
            LicenseAcquisitionCount++;
        }

        static public uint GetIndivCount()
        {
            return IndivCount;
        }

        static public uint GetLicenseAcquisitionCount()
        {
            return LicenseAcquisitionCount;
        }

    }
    
    public class ServiceRequestConfigData
    {

        Guid []    _guidKeyIds;
        string []  _strKeyIdStrings;
        Guid    _guidDomainServiceId = Guid.Empty;
        Guid    _guidDomainAccountId = Guid.Empty;
        Uri     _domainUri = null;
        
        Uri     _Uri = null;
        string  _strChallengeCustomData = String.Empty;
        string  _strResponseCustomData = String.Empty;
        PlayReadyEncryptionAlgorithm  _encryptionAlgorithm;
        string  _strExpectedLAErrorCode = String.Empty;

        bool    _manualEnabling = false;
        public bool ManualEnabling  
        {  
            set { this._manualEnabling=  value; }  
            get { return this._manualEnabling; } 
        }
        public Guid [] KeyIds  
        {  
            set { this._guidKeyIds=  value; }  
            get { return this._guidKeyIds; } 
        }
        public string [] KeyIdStrings  
        {  
            set { this._strKeyIdStrings=  value; }  
            get { return this._strKeyIdStrings; } 
        }

        public Uri Uri  
        {  
            set { this._Uri=  value; }  
            get { return this._Uri; } 
        }  
        public string ChallengeCustomData  
        {  
            set { this._strChallengeCustomData =  value; }  
            get { return this._strChallengeCustomData; } 
        }  
        public string ResponseCustomData  
        {  
            set { this._strResponseCustomData =  value; }  
            get { return this._strResponseCustomData; } 
        }  
        
        //
        //  Domain related config
        //
        public Guid DomainServiceId
        {  
            set { this._guidDomainServiceId =  value; }  
            get { return this._guidDomainServiceId; } 
        }  
        public Guid DomainAccountId
        {  
            set { this._guidDomainAccountId =  value; }  
            get { return this._guidDomainAccountId; } 
        }  
        public Uri DomainUri  
        {  
            set { this._domainUri=  value; }  
            get { return this._domainUri; } 
        }  

        //
        // License acquisition related config
        //
        public PlayReadyEncryptionAlgorithm EncryptionAlgorithm  
        {  
            set { this._encryptionAlgorithm =  value; }  
            get { return this._encryptionAlgorithm; } 
        }
        public string ExpectedLAErrorCode  
        {  
            set { this._strExpectedLAErrorCode =  value; }  
            get { return this._strExpectedLAErrorCode; } 
        }
        
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051: Do not declare visible instance fields", Justification = "this is application code")]  
    public class ServiceRequest
    {
        ServiceRequestConfigData _requestConfigData = null;
        protected IPlayReadyServiceRequest _serviceRequest = null;
        RequestChain   _requestChain = null;
        
        public const int MSPR_E_CONTENT_ENABLING_ACTION_REQUIRED = -2147174251;
        public const int MSPR_E_NO_METERING_DATA_AVAILABLE = -2147174244; //( 0x8004B89C )
        public const int DRM_E_NOMORE_DATA = -2147024637; //( 0x80070103 )
        public const int MSPR_E_NEEDS_INDIVIDUALIZATION = -2147174366; // (0x8004B822)
             
        public ServiceRequestConfigData RequestConfigData  
        {  
            set { this._requestConfigData =  value; }  
            get { 
                    if( this._requestConfigData == null )
                        return new ServiceRequestConfigData();
                    else
                        return this._requestConfigData;
                } 
        }

        protected bool IsEnablingActionRequested(Exception ex)
        {
            bool bRequested = false;
            
            if ( ex != null && ex.HResult == MSPR_E_CONTENT_ENABLING_ACTION_REQUIRED )
            {
                bRequested = true;
            }

            return bRequested;
        }

        protected virtual void EnablingActionCompleted(bool bResult)
        {
            
        }

        protected virtual bool HandleExpectedError(Exception ex)
        {
            return false;
        }
        protected bool PerformEnablingActionIfRequested(Exception ex)
        {
            TestLogger.LogMessage("Enter ServiceRequest.PerformEnablingActionIfRequested()" );
            bool bPerformed = false;
            
            if ( IsEnablingActionRequested(ex) ) 
            {
                TestLogger.LogMessage("!!!NextServiceRequest is needed!!!");
                IPlayReadyServiceRequest nextServiceRequest = _serviceRequest.NextServiceRequest();
                if( nextServiceRequest != null )
                {
                    TestLogger.LogMessage("!!!Servicing next request..." );
                    
                    _requestChain = new RequestChain( nextServiceRequest);
                    _requestChain.RequestConfigData = _requestConfigData;
                    _requestChain.FinishAndReportResult( new ReportResultDelegate(RequestChain_Finished));
                    
                    bPerformed = true;
                }
            }
            
            TestLogger.LogMessage("Leave ServiceRequest.PerformEnablingActionIfRequested()" );
            return bPerformed;
        }
        
        void RequestChain_Finished(bool bResult, object resultContext)
        {
            TestLogger.LogMessage("Enter ServiceRequest.RequestChain_Finished()" );
            
            EnablingActionCompleted( bResult );
            
            TestLogger.LogMessage("Leave ServiceRequest.RequestChain_Finished()" );
        }
        
    }
}
