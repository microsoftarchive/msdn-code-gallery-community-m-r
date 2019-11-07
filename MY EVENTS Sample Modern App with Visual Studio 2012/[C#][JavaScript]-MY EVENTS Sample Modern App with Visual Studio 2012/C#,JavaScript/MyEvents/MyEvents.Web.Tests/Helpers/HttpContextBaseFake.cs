using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyEvents.Web.Tests.Helpers
{
    public class HttpContextBaseFake : HttpContextBase
    {
        HttpSessionStateBaseFake _sessionState;
        /// <summary>
        /// HttpContextBaseFake contructor
        /// </summary>
        /// <param name="sessionState"></param>
        public HttpContextBaseFake(HttpSessionStateBaseFake sessionState)
        {
            _sessionState = sessionState;
        }

        /// <summary>
        /// Gets the session
        /// </summary>
        public override HttpSessionStateBase Session
        {
            get
            {
                return _sessionState;
            }
        }
    }
}
