using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Credentials;
using MyEvents.Client.Organizer.Helper;
using MyEvents.Client.Organizer.Services.Navigation;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using MyEvents.Client.Organizer.ViewModel.FakeViewModel;

namespace MyEvents.Client.Organizer.ViewModel
{
    /// <summary>
    /// Session details view model.
    /// </summary>
    public class SessionDetailViewModelFake
    {
        /// <summary>
        /// Selected session
        /// </summary>
        public Session Session
        {
            get
            {
                return FakeDataHelper.GetFakeSession();
            }
        }

        /// <summary>
        /// Session registered attendees
        /// </summary>
        public ObservableCollection<SessionUserDetailsViewModel> Attendees
        {
            get
            {
                return new ObservableCollection<SessionUserDetailsViewModel>(FakeDataHelper.GetFakeUserDetailsViewModels());
            }
        }

        /// <summary>
        /// Selected comment
        /// </summary>
        public Comment SelectedComment
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Controls the visibility of the delete comment button
        /// </summary>
        public bool IsCommentSelected
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Actual comments count
        /// </summary>
        public string CommentCount
        {
            get
            {
                return "5";
            }
        }

        /// <summary>
        /// Actual attendee count
        /// </summary>
        public string AttendeeCount
        {
            get
            {
                return "5";
            }
        }

        /// <summary>
        /// Submited voles
        /// </summary>
        public string SubmittedVotes
        {
            get
            {
                return "5";
            }
        }

        /// <summary>
        /// Pending votes
        /// </summary>
        public string PendingVotes
        {
            get
            {
                return "5";
            }
        }

        /// <summary>
        /// Indicate if we are loading data.
        /// </summary>
        public bool IsLoading
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Indicate if we have app bar open
        /// </summary>
        public bool IsAppBarOpen
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        /// Indicate if we have app bar sticked.
        /// </summary>
        public bool IsAppBarSticky
        {
            get
            {
                return false;
            }
        }
    }
}
