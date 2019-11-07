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

    public class SecureStop : ServiceRequest
    {
        byte[] _SecureStopCert = null;

        public byte[] GetSecureStopCertificate()
        {
            return _SecureStopCert;
        }

        public void SetSecureStopCertificate(byte[] SecureStopCert)
        {
            _SecureStopCert = SecureStopCert;
        }

        protected virtual void SecureStopServiceRequestCompleted(PlayReadySecureStopServiceRequest sender, Exception hrCompletionStatus)
        {
            TestLogger.LogImportantMessage("SecureStopServiceRequestCompleted");

            if (hrCompletionStatus != null)
            {
                TestLogger.LogError("SecureStopServiceRequestCompleted failed with " + hrCompletionStatus.HResult);
            }
        }

        void HandleIndivServiceRequest_Finished(bool bResult, object resultContext)
        {
            TestLogger.LogMessage("Enter SecureStop.HandleIndivServiceRequest_Finished()");

            TestLogger.LogMessage("HandleIndivServiceRequest_Finished(): " + bResult.ToString());

            if (bResult)
            {
                SecureStopProactively();
            }

            TestLogger.LogMessage("Leave SecureStop.HandleIndivServiceRequest_Finished()");
        }

        public void SecureStopProactively()
        {
            TestLogger.LogMessage("Enter SecureStop.SecureStopReportProactively()");
            try
            {
                TestLogger.LogMessage("Creating SecureStop report service request...");

                PlayReadySecureStopIterable secureStopIterable = new PlayReadySecureStopIterable(_SecureStopCert);

                PlayReadySecureStopServiceRequest SecureStopRequest = secureStopIterable.First() as PlayReadySecureStopServiceRequest;

                //PlayReadySecureStopServiceRequest SecureStopRequest = new PlayReadySecureStopServiceRequest(_SecureStopCert);
                SecureStopReactively(SecureStopRequest);
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
                    TestLogger.LogImportantMessage("SecureStopProactively failed:" + ex.HResult);
                }
            }

            TestLogger.LogMessage("Leave SecureStop.SecureStopReportProactively()");
        }

        void ConfigureServiceRequest()
        {
            PlayReadySecureStopServiceRequest SecureStopRequest = _serviceRequest as PlayReadySecureStopServiceRequest;

            TestLogger.LogMessage(" ");
            TestLogger.LogMessage("Configure SecureStop request to these values:");
            if (RequestConfigData.Uri != null)
            {
                TestLogger.LogMessage("URL       :" + RequestConfigData.Uri.ToString());
                SecureStopRequest.Uri = RequestConfigData.Uri;
            }

            if (RequestConfigData.ChallengeCustomData != null && RequestConfigData.ChallengeCustomData != String.Empty)
            {
                TestLogger.LogMessage("ChallengeCustomData:" + RequestConfigData.ChallengeCustomData);
                SecureStopRequest.ChallengeCustomData = RequestConfigData.ChallengeCustomData;
            }

            TestLogger.LogMessage(" ");
        }

        async public void SecureStopReactively(PlayReadySecureStopServiceRequest SecureStopRequest)
        {
            TestLogger.LogMessage("Enter SecureStop.SecureStopReportReactively()");
            Exception exception = null;

            try
            {
                _serviceRequest = SecureStopRequest;
                ConfigureServiceRequest();

                TestLogger.LogMessage("ChallengeCustomData = " + SecureStopRequest.ChallengeCustomData);
                if (RequestConfigData.ManualEnabling)
                {
                    TestLogger.LogMessage("Manually posting the request...");

                    HttpHelper httpHelper = new HttpHelper(SecureStopRequest);
                    await httpHelper.GenerateChallengeAndProcessResponse();
                }
                else
                {
                    TestLogger.LogMessage("Begin SecureStop service request...");
                    await SecureStopRequest.BeginServiceRequest();
                }
            }
            catch (Exception ex)
            {
                TestLogger.LogMessage("Saving exception..");
                exception = ex;
            }
            finally
            {
                TestLogger.LogMessage("Post-SecureStop Values:");
                if (exception == null)
                {
                    TestLogger.LogMessage("ResponseCustomData = " + SecureStopRequest.ResponseCustomData);
                    TestLogger.LogMessage("ProtectionSystem   = " + SecureStopRequest.ProtectionSystem.ToString());
                    TestLogger.LogMessage("Type = " + SecureStopRequest.Type.ToString());
                }

                SecureStopServiceRequestCompleted(SecureStopRequest, exception);
            }

            TestLogger.LogMessage("Leave SecureStop.SecureStopReportReactively()");
        }

    }

    public class SecureStopAndReportResult : SecureStop
    {
        ReportResultDelegate _reportResult = null;
        bool _bExpectError = false;
        
        public SecureStopAndReportResult(ReportResultDelegate callback, bool bExpectError)
        {
            _reportResult = callback;
            _bExpectError = bExpectError;
        }

        protected override void SecureStopServiceRequestCompleted(PlayReadySecureStopServiceRequest SecureStopRequest, Exception hrCompletionStatus)
        {
            TestLogger.LogMessage("Enter SecureStopAndReportResult.SecureStopServiceRequestCompleted()");

            if (hrCompletionStatus != null)
            {
                if (PerformEnablingActionIfRequested(hrCompletionStatus) || HandleExpectedError(hrCompletionStatus))
                {
                    TestLogger.LogMessage("Exception handled.");
                }
                else
                {
                    TestLogger.LogError("SecureStopServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                    _reportResult(false, null);
                }
            }

            TestLogger.LogMessage("Leave SecureStopAndReportResult.SecureStopServiceRequestCompleted()");
        }

        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter SecureStopAndReportResult.EnablingActionCompleted()");

            _reportResult(bResult, null);

            TestLogger.LogMessage("Leave SecureStopAndReportResult.EnablingActionCompleted()");
        }

        protected override bool HandleExpectedError(Exception ex)
        {
            TestLogger.LogMessage("Enter SecureStopAndReportResult.HandleExpectedError() _bExpectError=" + _bExpectError);
            bool bHandled = false;

            if (_bExpectError)
            {
                TestLogger.LogMessage(" ex.HResult= " + ex.HResult);
                if (ex.HResult == DRM_E_NOMORE_DATA)
                {
                    TestLogger.LogMessage("SecureStopAndReportResult.HandleExpectedError : Received DRM_E_NOMORE_DATA as expected");
                    bHandled = true;
                    _reportResult(true, null);
                }
            }
                
            TestLogger.LogMessage("Leave SecureStopAndReportResult.HandleExpectedError()");
            return bHandled;
        }
    }
}
