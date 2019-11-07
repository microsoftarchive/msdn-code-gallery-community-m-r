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
    public delegate void ReportResultDelegate( bool bResult, object resultContext );

    public class Indiv :ServiceRequest
    {
        protected virtual void IndivServiceRequestCompleted( PlayReadyIndividualizationServiceRequest  sender, Exception hrCompletionStatus ) 
        {
        }

        public void  IndivProactively()
        {
            PlayReadyIndividualizationServiceRequest indivRequest = new PlayReadyIndividualizationServiceRequest();
            IndivReactively(indivRequest);
        }
        public async void IndivReactively(PlayReadyIndividualizationServiceRequest indivRequest)
        {
            TestLogger.LogMessage("Enter Indiv.IndivReactively()" );
            Exception exception = null;
            
            try
            {
                _serviceRequest = indivRequest;
                SerivceRequestStatistics.IncIndivCount();

                TestLogger.LogMessage("Begin indiv service request..." );
                await indivRequest.BeginServiceRequest();
            }
            catch ( Exception ex )
            {
                TestLogger.LogMessage("Saving exception.." );
                exception = ex;
            }
            finally
            {
                IndivServiceRequestCompleted( indivRequest, exception );
            }
            
            TestLogger.LogMessage("Leave Indiv.IndivReactively()" );
        }

    }


    public class IndivAndReportResult : Indiv
    {
        ReportResultDelegate _reportResult = null;
        public IndivAndReportResult( ReportResultDelegate callback)
        {
            _reportResult = callback;
        }
        
        protected override void IndivServiceRequestCompleted( PlayReadyIndividualizationServiceRequest  sender, Exception hrCompletionStatus )
        {
            TestLogger.LogMessage("Enter IndivAndReportResult.IndivServiceRequestCompleted()" );

            if( hrCompletionStatus == null )
            {
                TestLogger.LogImportantMessage("***Indiv succeeded***");
                TestLogger.LogImportantMessage("PlayReady security version " + Windows.Media.Protection.PlayReady.PlayReadyStatics.PlayReadySecurityVersion);
                _reportResult( true, null );
            }
            else
            {
                //needed for LA revoke->Re-Indiv->LA sequence
                if( !PerformEnablingActionIfRequested(hrCompletionStatus) )
                {
                    TestLogger.LogError( "IndivServiceRequestCompleted ERROR: " + hrCompletionStatus.ToString());
                   _reportResult( false, null );
                }
            }
            
            TestLogger.LogMessage("Leave IndivAndReportResult.IndivServiceRequestCompleted()" );
        }
        
        protected override void EnablingActionCompleted(bool bResult)
        {
            TestLogger.LogMessage("Enter IndivAndReportResult.EnablingActionCompleted()" );

            _reportResult( bResult, null );
            
            TestLogger.LogMessage("Leave IndivAndReportResult.EnablingActionCompleted()" );
        }
        
    }

}
