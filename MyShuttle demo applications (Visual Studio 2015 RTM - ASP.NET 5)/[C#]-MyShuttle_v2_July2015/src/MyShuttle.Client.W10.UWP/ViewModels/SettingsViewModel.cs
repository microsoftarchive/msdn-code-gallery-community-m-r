using Chance.MvvmCross.Plugins.UserInteraction;
using Cirrious.MvvmCross.Plugins.Messenger;
using MyShuttle.Client.Core.Infrastructure.Abstractions.Services;

namespace MyShuttle.Client.W10.UniversalApp.ViewModels
{
    public class SettingsViewModel : Core.ViewModels.SettingsViewModel
    {
        public SettingsViewModel(IApplicationSettingServiceSingleton applicationSettingService,
            IMvxMessenger messenger, IUserInteraction userInteraction) : base(applicationSettingService,
                messenger, userInteraction)
        {
        }
    }
}
