using System;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace Reversi.Common
{
    /// <summary>
    /// Provides a simple toast notification service.
    /// </summary>
    public static class Toast
    {
        /// <summary>
        /// Shows the specified text in a toast notification if notifications are enabled.
        /// </summary>
        /// <param name="text">The text to show.</param>
        public static void Show(string text)
        {
            const string template = 
                "<toast duration='short'><visual><binding template='ToastText01'>" +
                "<text id='1'>{0}</text></binding></visual></toast>";
            var toastXml = new XmlDocument();
            toastXml.LoadXml(String.Format(template, text));
            var toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
