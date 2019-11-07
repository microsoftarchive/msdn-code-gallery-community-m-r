namespace RCE.Modules.Search.Security
{
    public interface ICdnToken
    {
        string Token { get; }

        string AssetUrlQueryString { get; }
    }
}