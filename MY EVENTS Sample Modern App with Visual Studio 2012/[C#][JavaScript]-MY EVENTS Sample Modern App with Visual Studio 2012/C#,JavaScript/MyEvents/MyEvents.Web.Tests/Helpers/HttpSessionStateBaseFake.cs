using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyEvents.Web.Tests.Helpers
{
    public class HttpSessionStateBaseFake : HttpSessionStateBase
    {
        Dictionary<string, object> sessionValues = new Dictionary<string,object>();

        /// <summary>
        /// HttpSessionStateBaseFake constructor.
        /// </summary>
        public HttpSessionStateBaseFake() { 
        
        }

        /// <summary>
        /// Adds new value to the session.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public override void Add(string name, object value)
        {
            sessionValues.Add(name, value);
        }

        /// <summary>
        /// Removes a value from the session.
        /// </summary>
        /// <param name="name"></param>
        public override void Remove(string name)
        {
            sessionValues.Remove(name);
        }
    }
}
