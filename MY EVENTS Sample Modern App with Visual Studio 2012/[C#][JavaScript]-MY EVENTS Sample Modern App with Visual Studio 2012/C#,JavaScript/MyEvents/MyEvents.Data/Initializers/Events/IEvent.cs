using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEvents.Data.Initializers.Events
{
    interface IEvent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void Create(MyEventsContext context);
    }
}
