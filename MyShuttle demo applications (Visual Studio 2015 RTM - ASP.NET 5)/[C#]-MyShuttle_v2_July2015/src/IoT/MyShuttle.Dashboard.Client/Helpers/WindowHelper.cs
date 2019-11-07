using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace MyShuttle.Dashboard.Client.Helpers
{
    public static class WindowHelper
    {
        public static bool IsSmallResolution()
        {
            return Window.Current.Bounds.Height < 1080;
        }
    }
}
