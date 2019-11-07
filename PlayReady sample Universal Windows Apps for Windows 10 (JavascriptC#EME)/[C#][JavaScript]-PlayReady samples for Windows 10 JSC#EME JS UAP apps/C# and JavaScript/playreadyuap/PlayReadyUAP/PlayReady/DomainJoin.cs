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
    public class DomainJoin : ServiceRequest
    {

        protected virtual void DomainJoinServiceRequestCompleted(PlayReadyDomainJoinServiceRequest sender, Exception hrCompletionStatus)
        {
            TestLogger.LogImportantMessage("DomainJoinServiceRequestCompleted");

            if (hrCompletionStatus != null)
            {
                TestLogger.LogError("DomainJoinServiceRequestCompleted failed with " + hrCompletionStatus.HResult);
            }
        }

        void HandleIndivServiceRequest_Finished(bool bResult, object resultContext)
        {
            TestLogger.LogMessage("Enter DomainJoin.HandleIndivServiceRequest_Finished()");

            TestLogger.LogMessage("HandleIndivServiceRequest_Finished(): " + bResult.ToString());
            if (bResult)
            {
                DomainJoinProactively();
            }

            TestLogger.LogMessage("Leave DomainJoin.HandleIndivServiceRequest_Finished()");
        }

        public void DomainJoinProactively()
        {
            TestLogger.LogMessage("Enter DomainJoin.DomainJoinProactively()");
            try
            {
                PlayReadyDomainJoinServiceRequest domainJoinRequest = new PlayReadyDomainJoinServiceRequest();
                domainJoinRequest.DomainServiceId = RequestConfigData.DomainServiceId;
                domainJoinRequest.DomainAccountId = RequestConfigData.DomainAccountId;
                domainJoinRequest.Uri = RequestConfigData.DomainUri;

                DomainJoinReactively(domainJoinRequest);
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
                    TestLogger.LogImportantMessage("DomainJoinProactively failed:" + ex.HResult);
                }
            }

            TestLogger.LogMessage("Leave DomainJoin.DomainJoinProactively");
        }

        public void DumpDomainJoinServiceRequest(PlayReadyDomainJoinServiceRequest domainJoinRequest)
        {
            if (domainJoinRequest!= null)
            {
                TestLogger.LogMessage("DomainAccountId      = " + domainJoinRequest.DomainAccountId.ToString());
                TestLogger.LogMessage("DomainServiceId      = " + domainJoinRequest.DomainServiceId.ToString());
                TestLogger.LogMessage("DomainFriendlyName   = " + domainJoinRequest.DomainFriendlyName.ToString());
            }
           
        }

        async public void DomainJoinReactively(PlayReadyDomainJoinServiceRequest domainJoinRequest)
        {
            TestLogger.LogImportantMessage("Enter DomainJoin.DomainJoinReactively()");
            Exception exception = null;

            try
            {
                _serviceRequest = domainJoinRequest;

                TestLogger.LogMessage("DomainJoinRequest values before challenge:");
                DumpDomainJoinServiceRequest(domainJoinRequest);

                if (RequestConfigData.ManualEnabling)
                {
                    TestLogger.LogMessage("Manually posting the request...");

                    HttpHelper httpHelper = new HttpHelper(domainJoinRequest);
                    await httpHelper.GenerateChallengeAndProcessResponse();
                }
                else
                {
                    TestLogger.LogMessage("Begin domain join service request...");
                    await domainJoinRequest.BeginServiceRequest();
                }
            }
            catch (Exception ex)
            {
                TestLogger.LogMessage("Saving exception..");
                exception = ex;
            }
            finally
            {
                TestLogger.LogMessage("Post-DomainJoin Values:");
                DumpDomainJoinServiceRequest(domainJoinRequest);
                if (exception == null)
                {
                    TestLogger.LogMessage("ResponseCustomData   = " + domainJoinRequest.ResponseCustomData);
                }

                DomainJoinServiceRequestCompleted(domainJoinRequest, exception);
            }

            TestLogger.LogImportantMessage("Leave DomainJoin.DomainJoinReactively()");
        }
    }

    public class DomainJoinAndReportResult : DomainJoin
    {
        ReportResultDelegate _reportResult = null;
        string _strExpectedError = null;

        public string ExpectedError
        {
            set { this._strExpectedError = value; }
            get { return this._strExpectedError; }
        }

        public DomainJoinAndReportResult(ReportResultDelegate callback)
        {
            _reportResult = callback;
        }

        protected override void DomainJoinServiceRequestCompleted(PlayReadyDomainJoinServiceRequest sender, Exception hrCompletionStatus)
        {
            TestLogger.LogMessage("Enter DomainJoinAndReportResult.DomainJoinServiceRequestCompleted()");

            if (hrCompletionStatus == null)
            {
                TestLogger.LogImportantMessage("***Domain Join succeeded***");
                _reportResult(true, null);
            }
            else
            {
                if (!PerformEnablingActionIfRequested(hrCompletionStatus))
                {
                    TestLogger.LogError("DomainJoinServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                    _reportResult(false, null);
                }
            }

            TestLogger.LogMessage("Leave DomainJoinAndReportResult.DomainJoinServiceRequestCompleted()");
        }

        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter DomainJoinAndReportResult.EnablingActionCompleted()");

            _reportResult(bResult, null);

            TestLogger.LogMessage("Leave DomainJoinAndReportResult.EnablingActionCompleted()");
        }

    }

}
