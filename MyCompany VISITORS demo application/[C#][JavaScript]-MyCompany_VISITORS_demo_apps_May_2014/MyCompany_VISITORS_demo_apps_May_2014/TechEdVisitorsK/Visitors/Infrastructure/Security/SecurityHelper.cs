using System;

namespace Visitors
{

    public class SecurityHelper
    {
        public static bool RequestIsNoAuthRoute()
        {
            return true;
        }
    }

    public class DemoSecurityHelper : ISecurityHelper
    {
        public string GetUser()
        {
            return "scottha@mycompanydemos.com";
        }       
    }
}