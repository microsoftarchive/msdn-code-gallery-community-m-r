using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace MultiWindowingUWPAppLikeMicrosoftEdge
{
    interface IPage
    {
        int ViewId { get; set; }
        void RemoveItem();
        CoreDispatcher Dispatcher { get; }
    }
}
