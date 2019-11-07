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
    public class DomainLeave : ServiceRequest
    {

        protected virtual void DomainLeaveServiceRequestCompleted(PlayReadyDomainLeaveServiceRequest sender, Exception hrCompletionStatus)
        {
            TestLogger.LogImportantMessage("DomainLeaveServiceRequestCompleted");

            if (hrCompletionStatus != null)
            {
                TestLogger.LogError("DomainLeaveServiceRequestCompleted failed with " + hrCompletionStatus.HResult);
            }
        }

        void HandleIndivServiceRequest_Finished(bool bResult, object resultContext )
        {
            TestLogger.LogMessage("Enter DomainLeave.HandleIndivServiceRequest_Finished()");

            TestLogger.LogMessage("HandleIndivServiceRequest_Finished(): " + bResult.ToString());
            if (bResult)
            {
                DomainLeaveProactively();
            }

            TestLogger.LogMessage("Leave DomainLeave.HandleIndivServiceRequest_Finished()");
        }

        public void DomainLeaveProactively()
        {
            TestLogger.LogMessage("Enter DomainLeave.DomainLeaveProactively()");
            try
            {
                PlayReadyDomainLeaveServiceRequest domainLeaveRequest = new PlayReadyDomainLeaveServiceRequest();

                DomainLeaveReactively(domainLeaveRequest);
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
                    TestLogger.LogMessage("DomainLeaveProactively failed:" + ex.HResult);
                }
            }

            TestLogger.LogMessage("Leave DomainLeave.DomainLeaveProactively()");
        }

        async public void DomainLeaveReactively(PlayReadyDomainLeaveServiceRequest domainLeaveRequest)
        {
            TestLogger.LogImportantMessage("Enter DomainLeave.DomainLeaveReactively()");
            Exception exception = null;

            try
            {
                _serviceRequest = domainLeaveRequest;
                domainLeaveRequest.DomainServiceId = RequestConfigData.DomainServiceId;
                domainLeaveRequest.DomainAccountId = RequestConfigData.DomainAccountId;
                domainLeaveRequest.Uri = RequestConfigData.DomainUri;

                if (RequestConfigData.ManualEnabling)
                {
                    TestLogger.LogMessage("Manually posting the request...");

                    HttpHelper httpHelper = new HttpHelper(domainLeaveRequest);
                    await httpHelper.GenerateChallengeAndProcessResponse();
                }
                else
                {
                    TestLogger.LogImportantMessage("Begin domain leave service request...");
                    await domainLeaveRequest.BeginServiceRequest();
                }
            }
            catch (Exception ex)
            {
                TestLogger.LogImportantMessage("Saving exception..");
                exception = ex;
            }
            finally
            {
                DomainLeaveServiceRequestCompleted(domainLeaveRequest, exception);
            }

            TestLogger.LogImportantMessage("Leave DomainLeave.DomainLeaveReactively()");
        }
    }

    public class DomainLeaveAndReportResult : DomainLeave
    {
        ReportResultDelegate _reportResult = null;
        string _strExpectedError = null;

        public string ExpectedError
        {
            set { this._strExpectedError = value; }
            get { return this._strExpectedError; }
        }

        public DomainLeaveAndReportResult(ReportResultDelegate callback)
        {
            _reportResult = callback;
        }

        protected override void DomainLeaveServiceRequestCompleted(PlayReadyDomainLeaveServiceRequest sender, Exception hrCompletionStatus)
        {
            TestLogger.LogMessage("Enter DomainLeaveAndReportResult.DomainLeaveServiceRequestCompleted()");

            if (hrCompletionStatus == null)
            {
                TestLogger.LogImportantMessage("***Domain Leave succeeded***");
                _reportResult(true, null);
            }
            else
            {
                if (!PerformEnablingActionIfRequested(hrCompletionStatus))
                {
                    TestLogger.LogError("DomainLeaveServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                    _reportResult(false, null);
                }
            }

            TestLogger.LogMessage("Leave DomainLeaveAndReportResult.DomainLeaveServiceRequestCompleted()");
        }

        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter DomainLeaveAndReportResult.EnablingActionCompleted()");

            _reportResult(bResult, null);

            TestLogger.LogMessage("Leave DomainLeaveAndReportResult.EnablingActionCompleted()");
        }

    }

}
