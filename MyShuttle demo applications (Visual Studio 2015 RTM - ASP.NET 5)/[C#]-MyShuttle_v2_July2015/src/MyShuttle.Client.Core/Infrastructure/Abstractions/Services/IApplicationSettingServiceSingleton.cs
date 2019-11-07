namespace MyShuttle.Client.Core.Infrastructure.Abstractions.Services
{
    public interface IApplicationSettingServiceSingleton
    {
        string UrlPrefix { get; set; }

        bool? AuthenticationEnabled { get; set; }

        string BingMapsToken { get; set; }

        string UniversalAppBingMapsToken { get; set; }

        int TopListItemsCount { get; set; }

        bool LocationFixed { get; }

        double LocationFixedLatitude { get; }

        double LocationFixedLongitude { get; }

        void Refresh();
    }
}
