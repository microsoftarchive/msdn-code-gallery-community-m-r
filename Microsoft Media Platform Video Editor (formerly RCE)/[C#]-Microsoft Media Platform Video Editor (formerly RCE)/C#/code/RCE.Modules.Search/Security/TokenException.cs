namespace RCE.Modules.Search.Security
{
    using System;

    public class TokenException : Exception
    {
        public TokenException()
        {
        }

        public TokenException(string msg)
            : base(msg)
        {
        }
    }
}