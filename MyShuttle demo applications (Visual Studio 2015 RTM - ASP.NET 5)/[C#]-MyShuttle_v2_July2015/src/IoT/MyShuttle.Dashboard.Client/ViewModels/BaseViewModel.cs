using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Microsoft.Practices.Prism.Mvvm;
using MyShuttle.Dashboard.Client.Services.Abstract;

namespace MyShuttle.Dashboard.Client.ViewModels
{
    public abstract class BaseViewModel : ViewModel
    {
        private readonly IAlertMessageService _alertMessageService;

        protected BaseViewModel(IAlertMessageService alertMessageService)
        {
            _alertMessageService = alertMessageService;
        }

        public override async void OnNavigatedTo(object navigationParameter, NavigationMode navigationMode,
            Dictionary<string, object> viewModelState)
        {
            try
            {
                await LoadData(navigationParameter);

            }
            catch (Exception ex)
            {
                var errorMessage = string.Format(CultureInfo.CurrentCulture,
                    "The following error messages were received from the service: {0} {1}",
                    Environment.NewLine, ex.Message);

                // HACK C# 6 Feature - Using await in a catch or finally block
                await _alertMessageService.ShowAsync(errorMessage, "Service is unreachable.");
            }

            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);
        }

        protected abstract Task LoadData(object navigationParameter);
        
    }
}
