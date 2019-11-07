namespace MyCompany.Visitors.Client.UniversalApp.Services.NFC
{
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using System;

    /// <summary>
    /// Contract for NFC Service
    /// </summary>
    public interface INfcService
    {
        /// <summary>
        /// When a new visitor is received by nfc this event is raised.
        /// </summary>
        event EventHandler<VisitorEventArgs> VisitorReceived;

        /// <summary>
        /// Prepare the app to receive NFC y Bluetooth data.
        /// </summary>
        void WaitForDataAsync();
    }
}
