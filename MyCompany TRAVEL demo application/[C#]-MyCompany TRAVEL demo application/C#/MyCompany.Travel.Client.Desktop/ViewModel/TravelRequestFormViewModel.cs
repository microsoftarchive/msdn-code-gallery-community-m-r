namespace MyCompany.Travel.Client.Desktop.ViewModel
{
    using GalaSoft.MvvmLight;
    using GalaSoft.MvvmLight.Command;
    using GalaSoft.MvvmLight.Messaging;
    using MyCompany.Travel.Client.Desktop.Model;
    using MyCompany.Travel.Client.Desktop.Resources.Strings;
    using MyCompany.Travel.Client.Desktop.Services.Navigation;
    using MyCompany.Travel.Client.Desktop.Services.SampleData;
    using MyCompany.Travel.Client.Desktop.Services.Security;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Input;

    /// <summary>
    /// MainWindow ViewModel
    /// </summary>
    public class TravelRequestFormViewModel : ViewModelBase
    {
        private readonly INavigationService navService;
        private readonly ISampleDataService sampleDataService;
        private readonly IMyCompanyClient myCompanyClient;
        private TravelRequest travelRequest;
        private ObservableCollection<Employee> employeesList;
        private ObservableCollection<TravelAttachment> attachmentsList; 
        private bool editMode;
        private string minStartDate;
        private string newAttachmentName;
        private List<int> removedAttachments;
        private bool isRoundTrip;

        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;
        private RelayCommand getFileCommand;
        private RelayCommand<TravelAttachment> removeAttachmentCommand;
        private RelayCommand<TravelAttachment> downloadAttachmentCommand;
        private bool isDirty;

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public TravelRequestFormViewModel(INavigationService navService, ISampleDataService sampleDataService, IMyCompanyClient myCompanyClient)
        {
            this.navService = navService;
            this.sampleDataService = sampleDataService;
            this.myCompanyClient = myCompanyClient;
            SubscribeCommands();
        }

        void TravelRequestFormViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            isDirty = true;
        }

        /// <summary>
        /// Exposes the travel request
        /// </summary>
        public TravelRequest TravelRequest
        {
            get { return this.travelRequest; }
            set
            {
                this.travelRequest = value;
                RaisePropertyChanged(() => TravelRequest);
                RaisePropertyChanged(() => Depart);
                RaisePropertyChanged(() => SelectedEmployee);
                RaisePropertyChanged(() => Name);
                RaisePropertyChanged(() => FormTitle);
            }
        }

        /// <summary>
        /// Selected employee
        /// </summary>
        public Employee SelectedEmployee
        {
            get
            {
                if (this.travelRequest != null)
                    return this.travelRequest.Employee;
                else
                    return null;
            }
            set
            {
                if (this.travelRequest != null)
                {
                    this.travelRequest.Employee = value;
                    if (value != null)
                        this.travelRequest.EmployeeId = value.EmployeeId;
                    else
                        this.travelRequest.EmployeeId = 0;

                    RaisePropertyChanged(() => SelectedEmployee);
                    RaisePropertyChanged(() => TravelRequest);

                }
            }
        }

        /// <summary>
        /// Exposes the travel request depart date
        /// </summary>
        public DateTime Depart
        {
            get {
                if (this.travelRequest != null)
                    return this.travelRequest.Depart;

                return DateTime.MinValue;
            }
            set
            {
                if (this.travelRequest != null)
                {
                    this.travelRequest.Depart = value;
                    if (this.travelRequest.Return.CompareTo(value) < 0)
                        this.travelRequest.Return = value;

                    SetSelectableDates();
                    RaisePropertyChanged(() => TravelRequest);
                    RaisePropertyChanged(() => Depart);
                }
            }
        }

        /// <summary>
        /// Exposes the travel request depart date
        /// </summary>
        public String Name
        {
            get
            {
                if (this.travelRequest != null)
                    return this.travelRequest.Name;

                return String.Empty;
            }
            set
            {
                if (this.travelRequest != null)
                {
                    this.travelRequest.Name = value;
                    if (this.travelRequest.Name.CompareTo(value) < 0)
                        this.travelRequest.Name = value;

                    RaisePropertyChanged(() => TravelRequest);
                }
                RaisePropertyChanged(() => Name);
            }
        }

        /// <summary>
        /// Exposes the travel type
        /// </summary>
        public TravelType TravelType
        {
            get
            {
                if (this.travelRequest != null)
                    return this.travelRequest.TravelType;

                return TravelType.Roundtrip;
            }
            set
            {
                if (this.travelRequest != null)
                {
                    this.travelRequest.TravelType = value;

                    if (value == TravelType.Roundtrip)
                        IsRoundTrip = true;
                    else
                        IsRoundTrip = false;
                    RaisePropertyChanged(() => TravelRequest);
                }
                RaisePropertyChanged(() => TravelType);
            }
        }

        /// <summary>
        /// True if it's a round trip travel, false otherwise.
        /// </summary>
        public bool IsRoundTrip
        {
            get
            {
                if (this.travelRequest == null)
                    this.isRoundTrip = true;

                return this.isRoundTrip;
            }
            set
            {
                this.isRoundTrip = value;
                RaisePropertyChanged(() => IsRoundTrip);
            }
        }

        /// <summary>
        /// Employees list
        /// </summary>
        public ObservableCollection<Employee> EmployeesList
        {
            get
            {
                return this.employeesList;
            }
            set
            {
                this.employeesList = value;
                base.RaisePropertyChanged(() => EmployeesList);
            }
        }

        /// <summary>
        /// Exposes the name for a new attachment
        /// </summary>
        public String NewAttachmentName
        {
            get
            {
                return newAttachmentName;
            }
            set
            {
                newAttachmentName = value;
                base.RaisePropertyChanged(() => NewAttachmentName);
                base.RaisePropertyChanged(() => HasNewAttachmentName);                
            }
        }

        /// <summary>
        /// Exposes a boolean indicating if attachement name is filled
        /// </summary>
        public bool HasNewAttachmentName
        {
            get
            {
                return !String.IsNullOrWhiteSpace(newAttachmentName);
            }
        }

        /// <summary>
        /// Attachments list
        /// </summary>
        public ObservableCollection<TravelAttachment> AttachmentsList
        {
            get
            {
                return this.attachmentsList;
            }
            set
            {
                this.attachmentsList = value;
                base.RaisePropertyChanged(() => AttachmentsList);
            }
        }

        /// <summary>
        /// Mininmum Start Date
        /// </summary>
        public string MinStartDate
        {
            get
            {
                return this.minStartDate;
            }
            set
            {
                this.minStartDate = value;
                base.RaisePropertyChanged(() => MinStartDate);
                base.RaisePropertyChanged(() => MinEndDate);
            }
        }

        /// <summary>
        /// Minimum End Date
        /// </summary>
        public string MinEndDate
        {
            get
            {
                if (this.minStartDate != null && this.travelRequest.Depart.CompareTo(DateTime.Parse(this.minStartDate)) > 0)
                    return this.travelRequest.Depart.ToString("yyyy/MM/dd");

                return this.minStartDate;
            }
        }

        /// <summary>
        /// FormTitle
        /// </summary>
        public string FormTitle
        {
            get
            {
                if (this.travelRequest != null && this.travelRequest.TravelRequestId > 0)
                    return Resources.Strings.StringResources.EditTravelTitle;

                return Resources.Strings.StringResources.NewTravelTitle;
            }
        }
        

        /// <summary>
        /// Command to save travel request form
        /// </summary>
        public ICommand SaveCommand
        {
            get { return saveCommand; }
        }

        /// <summary>
        /// Command to cancel changes
        /// </summary>
        public ICommand CancelCommand
        {
            get { return cancelCommand; }
        }

        /// <summary>
        /// Command to get a file to attach
        /// </summary>
        public ICommand GetFileCommand
        {
            get { return getFileCommand; }
        }

        /// <summary>
        /// Command to remove an attachment
        /// </summary>
        public ICommand RemoveAttachmentCommand
        {
            get { return removeAttachmentCommand; }
        }
      
        /// <summary>
        /// Command to download an attachment
        /// </summary>
        public ICommand DownloadAttachmentCommand
        {
            get { return downloadAttachmentCommand; }
        }     
        

        /// <summary>
        /// Initialize form data
        /// </summary>
        /// <param name="travelRequest">travel request to edit</param>
        public async Task InitializeData(TravelRequest travelRequest)
        {
            Messenger.Default.Send(new LoadingMessage(true));
            try
            {
                this.PropertyChanged -= TravelRequestFormViewModel_PropertyChanged;
                isDirty = false;

                editMode = travelRequest.TravelRequestId > 0;
                TravelRequest = travelRequest;

                this.removedAttachments = new List<int>();

                var employees = await myCompanyClient.EmployeeService.GetEmployees();
                EmployeesList = new ObservableCollection<Employee>(employees);
                AttachmentsList = new ObservableCollection<TravelAttachment>();

                NewAttachmentName = String.Empty;

                if (editMode)
                {
                    this.TravelRequest = await myCompanyClient.TravelRequestService.Get(travelRequest.TravelRequestId, PictureType.Small);
                    if (TravelRequest.TravelAttachments != null)
                        AttachmentsList = new ObservableCollection<TravelAttachment>(TravelRequest.TravelAttachments);
                }

                SetSelectableDates();
                this.PropertyChanged += TravelRequestFormViewModel_PropertyChanged;
                this.TravelRequest.PropertyChanged += TravelRequestFormViewModel_PropertyChanged;
            }
            catch (Exception)
            {
                CustomDialogMessage message = new CustomDialogMessage(() => { }, StringResources.UnexpectedError, Visibility.Collapsed);
                Messenger.Default.Send<CustomDialogMessage>(message);
            }
            finally
            {
                Messenger.Default.Send(new LoadingMessage(false));
            }
        }

        private void SetSelectableDates()
        {
            DateTime minDate = DateTime.Now;
            if (editMode && this.travelRequest.Depart.CompareTo(minDate) < 0)
                minDate = this.travelRequest.Depart;
            MinStartDate = minDate.ToString("yyyy/MM/dd");
        }

        private bool ValidateForm()
        {
            if (String.IsNullOrWhiteSpace(travelRequest.Name) ||
                String.IsNullOrWhiteSpace(travelRequest.From) ||
                String.IsNullOrWhiteSpace(travelRequest.To) ||
                String.IsNullOrWhiteSpace(travelRequest.Description) ||
                travelRequest.Depart == DateTime.MinValue ||
                travelRequest.Return == DateTime.MinValue ||
                travelRequest.EmployeeId <= 0)
                return false;
            else
                return true;
        }

        private async void SaveCommandExecute()
        {
            try
            {
                if (!ValidateForm())
                    return;

                Messenger.Default.Send(new LoadingMessage(true));
                if (editMode)
                    await myCompanyClient.TravelRequestService.Update(this.travelRequest);
                else
                    TravelRequest.TravelRequestId = await myCompanyClient.TravelRequestService.Add(this.travelRequest);

                await UploadNewAttachments();

                foreach (int attachmentId in removedAttachments)
                    await myCompanyClient.TravelAttachmentService.Delete(attachmentId);

                navService.NavigateToTravelRequestList();

            }
            catch (Exception)
            {
                Messenger.Default.Send(new LoadingMessage(false));
                CustomDialogMessage message = new CustomDialogMessage(() => { }, StringResources.UnexpectedError, Visibility.Collapsed);
                Messenger.Default.Send<CustomDialogMessage>(message);
            }
        }

        private async Task UploadNewAttachments()
        {
            IEnumerable<TravelAttachment> newAttachments = AttachmentsList.Where(at => at.TravelAttachmentId == 0);

            foreach (TravelAttachment attachment in newAttachments)
            {
                attachment.TravelRequestId = TravelRequest.TravelRequestId;
                await myCompanyClient.TravelAttachmentService.Add(attachment);
            }
        }

        private void CancelCommandExecute()
        {
            if (isDirty)
            {
                CustomDialogMessage message = new CustomDialogMessage(() =>
                {
                    navService.NavigateToTravelRequestList();
                }, String.Format(Resources.Strings.StringResources.ConfirmCancelMessage), Visibility.Visible);

                Messenger.Default.Send<CustomDialogMessage>(message);
            }
            else
                navService.NavigateToTravelRequestList();
        }

        private void GetFileCommandExecute()
        {
            // Get a file with OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                Messenger.Default.Send(new LoadingMessage(true));
                // Create new attachment with file
                Stream file = dlg.OpenFile();
                byte[] fileContent = new byte[file.Length];
                file.Read(fileContent, 0, (int)file.Length);

                AttachmentsList.Add(new TravelAttachment()
                    {
                        TravelAttachmentId = 0,
                        TravelRequestId = travelRequest.TravelRequestId,
                        Content = fileContent,
                        FileName = Path.GetFileName(dlg.FileName),
                        Name = NewAttachmentName,
                    });

                NewAttachmentName = String.Empty;
                RaisePropertyChanged(() => AttachmentsList);
                Messenger.Default.Send(new LoadingMessage(false));
            }
        }

        private void RemoveAttachmentCommandExecute(TravelAttachment attachment)
        {
            if (attachment.TravelAttachmentId > 0)
            {
                removedAttachments.Add(attachment.TravelAttachmentId);
            }

            AttachmentsList.Remove(attachment);
            RaisePropertyChanged(() => AttachmentsList);
        }

        private async void DownloadAttachmentCommandExecute(TravelAttachment attachment)
        {
            Messenger.Default.Send(new LoadingMessage(true));
            try
            {
                TravelAttachment travelAttachment = await myCompanyClient.TravelAttachmentService.Get(attachment.TravelAttachmentId);

                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string filePath = Path.Combine(folderPath, travelAttachment.FileName);
                FileStream fileStream = File.Create(filePath);
                fileStream.Write(travelAttachment.Content, 0, travelAttachment.Content.Length);
                fileStream.Flush();
                fileStream.Close();
                Messenger.Default.Send(new LoadingMessage(false));
                System.Diagnostics.Process.Start(filePath);
            }
            catch (Exception)
            {
                Messenger.Default.Send(new LoadingMessage(false));
                CustomDialogMessage message = new CustomDialogMessage(() => { }, StringResources.UnexpectedError, Visibility.Collapsed);
                Messenger.Default.Send<CustomDialogMessage>(message);
            }
        }

        private void SubscribeCommands()
        {
            saveCommand = new RelayCommand(SaveCommandExecute);
            cancelCommand = new RelayCommand(CancelCommandExecute);
            getFileCommand = new RelayCommand(GetFileCommandExecute);
            removeAttachmentCommand = new RelayCommand<TravelAttachment>(RemoveAttachmentCommandExecute);
            downloadAttachmentCommand = new RelayCommand<TravelAttachment>(DownloadAttachmentCommandExecute);
        }
    }
}