using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.ViewModels;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;
using MyShuttle.Client.Core.Messages;
using MyShuttle.Client.Core.ViewModels.Base;
using MyShuttle.Client.Core.ViewModels.InterfacesForDependencyInjection;
using System;
using System.Windows.Input;

namespace MyShuttle.Client.Core.ViewModels
{
    public class SettingsViewModel : NavegableViewModel, ISettingsViewModel
    {
        private readonly IApplicationSettingServiceSingleton _applicationSettingService;
        private readonly IMvxMessenger _messenger;
        private readonly IUserInteraction _userInteraction;

        private string _url;

        private ICommand _saveSettingsCommand;
        private ICommand _cancelSettingsCommand;

        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                RaisePropertyChanged(() => Url);
            }
        }

        public ICommand SaveSettingsCommand
        {
            get { return this._saveSettingsCommand; }
        }

        public ICommand CancelSettingsCommand
        {
            get { return this._cancelSettingsCommand; }
        }

        public SettingsViewModel(
            IApplicationSettingServiceSingleton applicationSettingService, 
            IMvxMessenger messenger,IUserInteraction userInteraction)
        {
            
            if (applicationSettingService == null)
            {
                throw new ArgumentNullException("applicationSettingService");
            }

            if (messenger == null)
            {
                throw new ArgumentNullException("messenger");
            }

            if (userInteraction == null)
            {
                throw new ArgumentNullException("userInteraction");
            }

            _applicationSettingService = applicationSettingService;
            _messenger = messenger;
            _userInteraction = userInteraction;

            this.InitializeCommands();
            this.InitializeActions();
        }

        private void InitializeActions()
        {
            this.Url = this._applicationSettingService.UrlPrefix;
        }

        private void InitializeCommands()
        {
            this._saveSettingsCommand = new MvxCommand(this.SaveSettings);
            this._cancelSettingsCommand = new MvxCommand(this.CancelSettings);
        }

        private void CancelSettings()
        {
            this.Close(this);
        }

        private async void SaveSettings()
        {
            if (string.IsNullOrWhiteSpace(this._url) || !Uri.IsWellFormedUriString(this._url, UriKind.Absolute))
            {
                await this._userInteraction.AlertAsync(CoreResources.Err_Invalid_URL);
                return;
            }

            this._applicationSettingService.UrlPrefix = this._url;

            var reloadDataMessage = new ReloadDataMessage(this);
            this._messenger.Publish(reloadDataMessage);

            this.Close(this);
        }
    }
}
