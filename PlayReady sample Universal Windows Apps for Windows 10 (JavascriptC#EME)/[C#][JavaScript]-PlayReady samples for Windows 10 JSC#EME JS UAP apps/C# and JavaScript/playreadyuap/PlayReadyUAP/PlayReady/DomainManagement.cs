//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Protection.PlayReady;
using PlayReadyUAP;

namespace PlayReadyUAP
{

    sealed public class DomainManagement 
    {
        private DomainManagement() { }

        static public void DumpDomainValues(PlayReadyDomain domain)
        {
            TestLogger.LogMessage(" " );
            TestLogger.LogMessage("Domain values:" );
            
            TestLogger.LogMessage("AccountId  :" + domain.AccountId.ToString() );
            TestLogger.LogMessage("ServiceId   :" + domain.ServiceId.ToString() );
            
            TestLogger.LogMessage("Revision  :" + domain.Revision );
            TestLogger.LogMessage("FriendlyName  :" + domain.FriendlyName );

            Uri uri = domain.DomainJoinUrl;
            if( uri != null )
            {
                TestLogger.LogMessage("DomainJoinUrl :" + uri.ToString() );
            }
            TestLogger.LogMessage(" " );
            
        }
        
        static public  PlayReadyDomain FindSingleDomain( Guid guidAccountId )
        {
            TestLogger.LogMessage("Enter DomainManagement.FindSingleDomain()" );
                        
            TestLogger.LogMessage("Creating PlayReadyDomainIterable..." );
            PlayReadyDomainIterable domainIterable = new PlayReadyDomainIterable( guidAccountId );
            foreach( PlayReadyDomain dom in domainIterable )
            {
                DumpDomainValues( dom );
            }
            
            PlayReadyDomain domain = null;
            IEnumerable<IPlayReadyDomain> domainEnumerable = domainIterable;
            
            int domainCount = Enumerable.Count<IPlayReadyDomain>( domainEnumerable );
            TestLogger.LogMessage("domain count  :" + domainCount );
            if( domainCount > 0 )
            {
                domain = Enumerable.ElementAt<IPlayReadyDomain>( domainEnumerable, 0 ) as PlayReadyDomain;
            }
            
            TestLogger.LogMessage("Leave DomainManagement.FindSingleDomain()" );
            
            return domain;
        }
        
        static public  IPlayReadyDomain[] FindMultipleDomains( Guid guidAccountId )
        {
            TestLogger.LogMessage("Enter DomainManagement.FindMultipleDomains()" );
            
            TestLogger.LogMessage("Creating PlayReadyDomainIterable..." );
            PlayReadyDomainIterable domainIterable = new PlayReadyDomainIterable( guidAccountId );
            foreach( PlayReadyDomain dom in domainIterable )
            {
                DumpDomainValues( dom );
            }
            
            IPlayReadyDomain[] domains = null;
            IEnumerable<IPlayReadyDomain> domainEnumerable = domainIterable;
            
            int domainCount = Enumerable.Count<IPlayReadyDomain>( domainEnumerable );
            TestLogger.LogMessage("domain count  :" + domainCount );
            if( domainCount > 0 )
            {
                domains = Enumerable.ToArray<IPlayReadyDomain>( domainEnumerable );
            }
            
            TestLogger.LogMessage("Leave DomainManagement.FindMultipleDomains()" );
            
            return domains;
        }

    }


}
