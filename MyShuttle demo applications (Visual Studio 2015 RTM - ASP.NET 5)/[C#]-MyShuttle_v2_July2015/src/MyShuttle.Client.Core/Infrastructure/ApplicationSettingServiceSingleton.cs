using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Repositories;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.Messages;
using System;

namespace MyShuttle.Client.Core.Infrastructure
{
    public class ApplicationSettingServiceSingleton : IApplicationSettingServiceSingleton
    {
        // Services
        private readonly IApplicationDataRepository _applicationDataRepository;
        private readonly IMvxMessenger _messenger;

        private readonly MvxSubscriptionToken _token;

        // Fields
        private const string UrlPrefixKey = "URLPREFIXKEY";
        private const string DefaultUrlPrefixValue = "http://YOUR_SITE.azurewebsites.net/";
        private string _urlPrefix;

        private const string AuthenticationEnabledKey = "AUTHENTICATIONENABLEDKEY";
        private readonly bool? _defaultAuthenticationEnabledValue = null;
        private bool? _authenticationEnabled;

        private const string BingMapsTokenKey = "BINGMAPSTOKENKEY";
        private const string DefaultBingMapsTokenValue = "YOUR_BING_MAPS_TOKEN_VALUE";
        private string _bingMapsToken;

        private const string UniversalAppBingMapsTokenKey = "UNIVERSALAPPBINGMAPSTOKENKEY";
        private const string DefaultUniversalAppBingMapsTokenValue = "YOUR_UNIVERSAL_BING_MAPS_TOKEN_VALUE";

        private const string TopListItemsCountKey = "TOPLISTITEMSCOUNTKEY";
        private readonly int? _defaultTopListItemsCountValue = 7;
        private int? _topListItemsCount;

        private const bool LocationFixedValue = true;
        private const double LocationFixedLatitudeValue = 47.641944;
        private const double LocationFixedLongitudeValue = -122.127222;

        public string UrlPrefix
        {
            get
            {
                if (string.IsNullOrEmpty(_urlPrefix))
                {
                    _urlPrefix = _applicationDataRepository.GetStringFromApplicationData(UrlPrefixKey, DefaultUrlPrefixValue);
                }

                return _urlPrefix;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == _urlPrefix)
                {
                    return;
                }

                _applicationDataRepository.SetStringToApplicationData(UrlPrefixKey, value);
                _urlPrefix = value;
            }
        }

        public bool? AuthenticationEnabled
        {
            get
            {
                if (!_authenticationEnabled.HasValue)
                {
                    _authenticationEnabled = _applicationDataRepository.GetOptionalBooleanFromApplicationData(AuthenticationEnabledKey, _defaultAuthenticationEnabledValue);
                }
                return _authenticationEnabled.Value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string BingMapsToken
        {
            get
            {
                if (string.IsNullOrEmpty(_bingMapsToken))
                {
                    _bingMapsToken = _applicationDataRepository.GetStringFromApplicationData(BingMapsTokenKey, DefaultBingMapsTokenValue);
                }
                return _bingMapsToken;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string UniversalAppBingMapsToken
        {
            get
            {
                if (string.IsNullOrEmpty(_bingMapsToken))
                {
                    _bingMapsToken = _applicationDataRepository.GetStringFromApplicationData(UniversalAppBingMapsTokenKey, DefaultUniversalAppBingMapsTokenValue);
                }
                return _bingMapsToken;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int TopListItemsCount
        {
            get
            {
                if (!_topListItemsCount.HasValue)
                {
                    _topListItemsCount = _applicationDataRepository.GetOptionalIntegerFromApplicationData(TopListItemsCountKey, _defaultTopListItemsCountValue);
                }
                return _topListItemsCount.Value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public int TopListLessItemsCount
        {
            get
            {
                if (!_topListItemsCount.HasValue)
                {
                    _topListItemsCount = _applicationDataRepository.GetOptionalIntegerFromApplicationData(TopListItemsCountKey, _defaultTopListItemsCountValue);
                }
                return _topListItemsCount.Value;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool LocationFixed
        {
            get { return LocationFixedValue; }
        }

        public double LocationFixedLatitude
        {
            get { return LocationFixedLatitudeValue; }
        }

        public double LocationFixedLongitude
        {
            get { return LocationFixedLongitudeValue; }
        }

        public ApplicationSettingServiceSingleton(
            IApplicationDataRepository applicationDataRepository,
            IMvxMessenger messenger)
        {
            if (applicationDataRepository == null)
            {
                throw new ArgumentNullException("applicationDataRepository");
            }

            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }

            _applicationDataRepository = applicationDataRepository;
            _messenger = messenger;

            _token = _messenger.Subscribe<ReloadDataMessage>(_ => MarkUrlPrefixAsDirty());
        }

        public void Refresh()
        {
            _urlPrefix = null;
            _authenticationEnabled = null;
            _topListItemsCount = null;
        }

        private void MarkUrlPrefixAsDirty()
        {
            this._urlPrefix = null;
        }
    }
}
