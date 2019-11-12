namespace RCE.Modules.Search.Security
{
    public class AkamaiCdnTokenGenerator : ICdnTokenGenerator
    {
        public ICdnToken Generate(string urlPath, long inWindow, string salt, string extract, long time, string inParam)
        {
            return new AkamaiCdnToken(urlPath, inWindow, salt, extract, time, inParam);
        }
    }
}
