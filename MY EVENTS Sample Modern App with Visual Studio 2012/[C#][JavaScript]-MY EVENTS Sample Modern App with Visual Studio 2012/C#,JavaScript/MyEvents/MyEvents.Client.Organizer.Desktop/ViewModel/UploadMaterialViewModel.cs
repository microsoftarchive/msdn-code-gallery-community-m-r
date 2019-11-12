using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.Services.Navigation;

namespace MyEvents.Client.Organizer.Desktop.ViewModel
{
    /// <summary>
    /// View model that holds the uploaded materials
    /// </summary>
    public class UploadMaterialViewModel : ViewModelBase
    {
        private Lazy<RelayCommand> _openFileSelectorCommand;
        private Lazy<RelayCommand<Material>> _deleteMaterialCommand;
        private Lazy<RelayCommand> _navigateBackCommand;
        private Session _currentSession;
        private INavigationService _navService;
        private IMyEventsClient _eventsClient;
        private bool _isBusy = false;

        /// <summary>
        /// Default constructor for the UploadMaterialViewModel  
        /// </summary>
        public UploadMaterialViewModel(INavigationService navService, IMyEventsClient eventsClient)
        {
            _navService = navService;
            _eventsClient = eventsClient;
            Materials = new ObservableCollection<Material>();
            _eventsClient.SetAccessToken(Credentials.UserCredentials.Current.MyEventsToken);
            SuscribeCommands();
        }

        private void SuscribeCommands()
        {
            _openFileSelectorCommand = new Lazy<RelayCommand>(() => { return new RelayCommand(OpenFileSelectorExecute); });
            _deleteMaterialCommand = new Lazy<RelayCommand<Material>>(() => { return new RelayCommand<Material>(DeleteMaterialExecute); });
            _navigateBackCommand = new Lazy<RelayCommand>(() => new RelayCommand(NavigateBackExecute));
        }

        private void DeleteMaterialExecute(Material materialToDelete)
        {
            _eventsClient.MaterialService.DeleteMaterialAsync(materialToDelete.MaterialId, (result) =>
            {
                App.Current.Dispatcher.BeginInvoke(new Action(delegate()
                {
                    Materials.Remove(materialToDelete);
                }));

            });
        }

        private void OpenFileSelectorExecute()
        {
            Stream myStream;
            OpenFileDialog openMaterialDialog = new OpenFileDialog();

            openMaterialDialog.FilterIndex = 2;
            openMaterialDialog.RestoreDirectory = true;

            if (openMaterialDialog.ShowDialog().Value == true)
            {
                if ((myStream = openMaterialDialog.OpenFile()) != null)
                {
                    IsBusy = true;
                    var bytes = File.ReadAllBytes(openMaterialDialog.FileName);

                    var newMaterial = new Material()
                    {
                        Content = bytes,
                        Name = openMaterialDialog.SafeFileName,
                        SessionId = CurrentSession.SessionId,
                        Session = null,
                        ContentType = "application/octet-stream"
                    };
                    _eventsClient.MaterialService.AddMaterialAsync(newMaterial, (result) =>
                    {
                        App.Current.Dispatcher.BeginInvoke(new Action(delegate()
                            {
                                IsBusy = false;
                                Materials.Add(newMaterial);
                            }));
                    });
                }
            }
        }

        private void NavigateBackExecute()
        {
            _navService.NavigateBack();
        }

        /// <summary>
        /// Command to navigate to last page.
        /// </summary>
        public ICommand NavigateBackCommand
        {
            get { return _navigateBackCommand.Value; }
        }

        /// <summary>
        /// Command to delete the uploaded material
        /// </summary>
        public ICommand DeleteMaterialCommand
        {
            get { return _deleteMaterialCommand.Value; }
        }

        /// <summary>
        /// Command to open the file selector command
        /// </summary>
        public ICommand OpenFileSelectorCommand
        {
            get { return _openFileSelectorCommand.Value; }
        }

        /// <summary>
        /// Property used to show a progressbar when the app is busy retrieving data
        /// </summary>
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        /// <summary>
        /// List of materials been uploaded 
        /// </summary>
        public ObservableCollection<Material> Materials { get; set; }

        /// <summary>
        /// Property to store the session in wich the materials are going to be uploaded to
        /// </summary>
        public Session CurrentSession
        {
            get { return _currentSession; }
            set
            {
                _eventsClient.MaterialService.GetAllMaterialsAsync(value.SessionId, (result) =>
                {
                    App.Current.Dispatcher.BeginInvoke(new Action(delegate()
                    {
                        Materials.Clear();
                        foreach (var material in result.OrderBy(m => m.Name))
                        {
                            Materials.Add(material);
                        }

                    }));
                });
                _currentSession = value;
                RaisePropertyChanged(() => CurrentSession);
            }
        }

    }
}
