//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;
using PlayReadyUAP;

namespace PlayReadyUAP
{
    public class HttpHelper
    {
        protected IPlayReadyServiceRequest _serviceRequest = null;
        Uri  _uri = null;

        public HttpHelper( IPlayReadyServiceRequest serviceRequest)
        {
            _serviceRequest = serviceRequest;
        }
        
        public async Task GenerateChallengeAndProcessResponse()
        {
            TestLogger.LogMessage(" " );
            TestLogger.LogMessage("Enter HttpHelper.GenerateChallengeAndProcessResponse()" );

            TestLogger.LogMessage("Generating challenge.." );
            PlayReadySoapMessage soapMessage = _serviceRequest.GenerateManualEnablingChallenge();
            if( _uri == null )
            {
                _uri = soapMessage.Uri;
            }

            TestLogger.LogMessage("Getting message body.." );
            byte[] messageBytes = soapMessage.GetMessageBody();
            HttpContent httpContent = new ByteArrayContent( messageBytes );

            IPropertySet propertySetHeaders = soapMessage.MessageHeaders;
            TestLogger.LogMessage("Http Headers:-");
            foreach( string strHeaderName in propertySetHeaders.Keys )
            {
                string strHeaderValue = propertySetHeaders[strHeaderName].ToString();
                TestLogger.LogMessage(strHeaderName + " : " + strHeaderValue);
                
                // The Add method throws an ArgumentException try to set protected headers like "Content-Type"
                // so set it via "ContentType" property
                if ( strHeaderName.Equals( "Content-Type", StringComparison.OrdinalIgnoreCase ) )
                {
                    httpContent.Headers.ContentType = MediaTypeHeaderValue.Parse(strHeaderValue);
                }
                else
                {
                    httpContent.Headers.Add(strHeaderName.ToString(), strHeaderValue);
                }
                
            }
            
            TestLogger.LogMessage("Http Body:-" );
            TestLogger.LogMessage(new System.Text.UTF8Encoding().GetString( messageBytes, 0, messageBytes.Length ));

            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.PostAsync( _uri, httpContent );
            string strResponse = await response.Content.ReadAsStringAsync();
            
            TestLogger.LogMessage("Processing Response.." );
            Exception exResult = _serviceRequest.ProcessManualEnablingResponse( await response.Content.ReadAsByteArrayAsync()) ;
            if( exResult != null)
            {
                throw exResult;
            }
            
            TestLogger.LogMessage("Leave HttpHelper.GenerateChallengeAndProcessResponse()" );
        }


    }


}
