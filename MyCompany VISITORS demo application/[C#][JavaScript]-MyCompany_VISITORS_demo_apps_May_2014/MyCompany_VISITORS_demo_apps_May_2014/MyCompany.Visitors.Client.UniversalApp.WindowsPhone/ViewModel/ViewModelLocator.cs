using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.ViewModel;

namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    public partial class ViewModelLocator
    {
        partial void RegisterWindowsPhone(ISimpleIoc container) 
        {
           container.Register<VMCaptureImage>(); 
        }       

        /// <summary>
        /// Capture Image viewmodel instance.
        /// </summary>
        public VMCaptureImage CaptureImageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<VMCaptureImage>(); }
        }  
    }
}
