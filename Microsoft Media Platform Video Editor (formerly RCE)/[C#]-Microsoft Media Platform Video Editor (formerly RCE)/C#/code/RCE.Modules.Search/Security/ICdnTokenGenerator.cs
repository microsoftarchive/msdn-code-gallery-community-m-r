namespace RCE.Modules.Search.Security
{
    public interface ICdnTokenGenerator
    {
        ICdnToken Generate(string urlPath, long inWindow, string salt, string extract, long time, string inParam);
    }
}
