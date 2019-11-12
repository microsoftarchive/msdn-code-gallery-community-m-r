using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyCompany.Expenses.Model;
using MyCompany.Expenses.Web.Services;
using System.Collections.Generic;

namespace MyCompany.Expenses.Web.Tests
{
    [TestClass]
    public class NotificationServiceTests
    {
        [TestMethod]
        public void NewExpenseAdded_IfNotificationTypeIsWindowsStoreNotification_ShouldCallWindowsStoreService()
        {
            var called = false;

            var notifiersProvider = new MyCompany.Expenses.Web.Services.Fakes.StubINotifiersProvider();

            notifiersProvider.GetNotificators = () =>
            {
                var windowsStoreNotificationService = new MyCompany.Expenses.Web.Services.Fakes.StubIPushNotificationService();

                windowsStoreNotificationService.NotificationTypeGet = () => NotificationType.WindowsStoreNotification;

                windowsStoreNotificationService.SendNewExpenseAddedToastStringStringStringInt32 = (channelUri, toastMessage, key, value) =>
                {
                    called = true;
                    return string.Empty;
                };

                return new List<IPushNotificationService>{
                    windowsStoreNotificationService
                };
            };

            var target = new NotificationService(notifiersProvider);
            target.NewExpenseAdded(new Model.NotificationChannel
                {
                    NotificationType = Model.NotificationType.WindowsStoreNotification
                }, new Model.Expense
                {
                    Employee = new Model.Employee()
                });

            Assert.IsTrue(called);
        }

        [TestMethod]
        public void ExpenseStatusChanged_IfNotificationTypeIsWindowsStoreNotification_ShouldCallWindowsPhoneService()
        {
            var called = false;

            var notifiersProvider = new MyCompany.Expenses.Web.Services.Fakes.StubINotifiersProvider();

            notifiersProvider.GetNotificators = () =>
            {
                var windowsPhoneNotificationService = new MyCompany.Expenses.Web.Services.Fakes.StubIPushNotificationService();

                windowsPhoneNotificationService.NotificationTypeGet = () => NotificationType.WindowsPhoneNotification;

                windowsPhoneNotificationService.SendExpenseStatusChangedToastStringStringStringInt32 = (channelUri, toastMessage, key, value) =>
                {
                    called = true;
                    return string.Empty;
                };

                return new List<IPushNotificationService>{
                    windowsPhoneNotificationService
                };
            };

            var target = new NotificationService(notifiersProvider);
            target.ExpenseStatusChanged(new Model.NotificationChannel
            {
                NotificationType = Model.NotificationType.WindowsPhoneNotification
            }, new Model.Expense
            {
                Employee = new Model.Employee()
            });

            Assert.IsTrue(called);
        }
    }
}
