using System;
using Windows.ApplicationModel.Resources;
using Windows.UI.Popups;

namespace MyEvents.Client.Organizer.Services.UserInterface
{
    public class UIService : IUIService
    {
        public async void ShowMessage(string resourceStringName)
        {
            ResourceLoader loader = new ResourceLoader();

            string msg = loader.GetString(resourceStringName);

            MessageDialog dialog = new MessageDialog(msg);
            await dialog.ShowAsync();
        }
    }
}
