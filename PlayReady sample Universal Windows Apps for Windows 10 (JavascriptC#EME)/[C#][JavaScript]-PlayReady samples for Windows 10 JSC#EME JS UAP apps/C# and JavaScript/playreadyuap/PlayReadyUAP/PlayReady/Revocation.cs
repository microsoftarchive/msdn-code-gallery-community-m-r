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
    public class Revocation :ServiceRequest
    {
        protected virtual void RevocationServiceRequestCompleted( PlayReadyRevocationServiceRequest  sender, Exception hrCompletionStatus ) 
        {
            TestLogger.LogImportantMessage("RevocationServiceRequestCompleted");

            if (hrCompletionStatus != null)
            {
                TestLogger.LogError("RevocationServiceRequestCompleted failed with " + hrCompletionStatus.HResult);
            }
        }

        void HandleIndivServiceRequest_Finished(bool bResult, object resultContext)
        {
            TestLogger.LogMessage("Enter Revocation.HandleIndivServiceRequest_Finished()");

            TestLogger.LogMessage("HandleIndivServiceRequest_Finished(): " + bResult.ToString());
            if (bResult)
            {
                HandleRevocationProactively();
            }

            TestLogger.LogMessage("Leave Revocation.HandleIndivServiceRequest_Finished()");
        }

        public void  HandleRevocationProactively()
        {
            try
            {
                PlayReadyRevocationServiceRequest request = new PlayReadyRevocationServiceRequest();
                HandleRevocationReactively(request);
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
                    TestLogger.LogImportantMessage("HandleRevocationProactively failed:" + ex.HResult);
                }
            }
        }
        async public void  HandleRevocationReactively(PlayReadyRevocationServiceRequest request)
        {
            TestLogger.LogMessage("Enter Revocation.HandleRevocationReactively()" );
            Exception exception = null;
            
            try
            {
                _serviceRequest = request;

                TestLogger.LogMessage("Begin revocation service request..." );
                await request.BeginServiceRequest();
            }
            catch ( Exception ex )
            {
                TestLogger.LogMessage("Saving exception.." );
                exception = ex;
            }
            finally
            {
                TestLogger.LogMessage("Post-RevocationServiceRequest Values:");
                if( exception == null )
                {
                    TestLogger.LogMessage("ResponseCustomData = " + request.ResponseCustomData);
                    TestLogger.LogMessage("ProtectionSystem   = " + request.ProtectionSystem.ToString());
                    TestLogger.LogMessage("Type = " + request.Type.ToString());
                }
                
                RevocationServiceRequestCompleted( request, exception );
            }
            
            TestLogger.LogMessage("Leave Revocation.HandleRevocationReactively()" );
        }

    }

    public class RevocationAndReportResult : Revocation
    {
        ReportResultDelegate _reportResult = null;
        public RevocationAndReportResult( ReportResultDelegate callback)
        {
            _reportResult = callback;
        }
        
        protected override void RevocationServiceRequestCompleted( PlayReadyRevocationServiceRequest  sender, Exception hrCompletionStatus )
        {
            TestLogger.LogMessage("Enter RevocationAndReportResult.RevocationServiceRequestCompleted()" );

            if( hrCompletionStatus == null )
            {
                TestLogger.LogMessage("********************************************Revocation handling succeeded**************************************************");
                _reportResult( true, null );
            }
            else
            {
                if( !PerformEnablingActionIfRequested(hrCompletionStatus) )
                {
                    TestLogger.LogError( "RevocationServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                   _reportResult( false, null );
                }
            }
            
            TestLogger.LogMessage("Leave RevocationAndReportResult.RevocationServiceRequestCompleted()" );
        }
        
        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter RevocationAndReportResult.EnablingActionCompleted()" );

            _reportResult( bResult, null );
            
            TestLogger.LogMessage("Leave RevocationAndReportResult.EnablingActionCompleted()" );
        }
        
    }

}
