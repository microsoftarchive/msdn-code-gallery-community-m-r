namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    using GalaSoft.MvvmLight;
    using MyCompany.Visitors.Client.UniversalApp.Settings;
    using System;
    using System.Linq;
    using Windows.Storage.Streams;
    using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Authenticated employee control viewmodel.
    /// </summary>
    public class VMAuthenticatedEmployee : ViewModelBase
    {
        private readonly IMyCompanyClient clientService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public VMAuthenticatedEmployee(IMyCompanyClient clientService)
        {
            this.clientService = clientService;
            if(!base.IsInDesignMode)
            {
                InitializeData();
            }
        }

        /// <summary>
        /// Employee photo.
        /// </summary>
        public BitmapImage Image
        {
            get 
            {
                if (AppSettings.EmployeeInformation == null)
                    return null;

                using (InMemoryRandomAccessStream ms = new InMemoryRandomAccessStream())
                {
                    using (DataWriter writer = new DataWriter(ms.GetOutputStreamAt(0)))
                    {
                        writer.WriteBytes((byte[])AppSettings.EmployeeInformation.EmployeePictures.Where(p => p.PictureType == PictureType.Small).First().Content);
                        writer.StoreAsync().GetResults();
                    }
                    var image = new BitmapImage();
                    image.SetSource(ms);
                    return image;
                }
            }
        }

        /// <summary>
        /// Employee username.
        /// </summary>
        public string Username
        {
            get 
            {
                if (AppSettings.EmployeeInformation == null)
                    return string.Empty;

                return string.Format("{0} {1}", AppSettings.EmployeeInformation.FirstName, AppSettings.EmployeeInformation.LastName); 
            }
        }

        /// <summary>
        /// Initialize Data Function
        /// </summary>
        private async void InitializeData()
        {
            try
            {
                if (AppSettings.EmployeeInformation == null)
                    AppSettings.EmployeeInformation = await clientService.EmployeeService.GetLoggedEmployeeInfo(PictureType.Small);

                if (AppSettings.EmployeeInformation != null)
                {
                    RaisePropertyChanged(() => Image);
                    RaisePropertyChanged(() => Username);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
