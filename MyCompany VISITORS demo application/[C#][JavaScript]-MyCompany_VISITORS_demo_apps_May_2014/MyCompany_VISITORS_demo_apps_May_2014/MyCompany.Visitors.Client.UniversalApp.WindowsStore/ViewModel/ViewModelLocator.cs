using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using MyCompany.Visitors.Client.UniversalApp.Services.Navigation;
using MyCompany.Visitors.Client.UniversalApp.Services.NFC;
using MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCompany.Visitors.Client.UniversalApp.ViewModel
{
    public partial class ViewModelLocator
    {
        partial void RegisterWindowsStore(ISimpleIoc container) 
        {
           
            container.Register<VMCropPicture>();          
            
            container.Register<VMNewVisitPage>();
            container.Register<VMSearchVisitorPage>();
            container.Register<VMSearchEmployeePage>();
            container.Register<VMNewVisitorPage>();
        }       

        /// <summary>
        /// Cropping picture viewmodel instance.
        /// </summary>
        public VMCropPicture CropPictureViewModel
        {
            get { return ServiceLocator.Current.GetInstance<VMCropPicture>(); }
        }       

      

        /// <summary>
        /// New visit viewmodel instance.
        /// </summary>
        public VMNewVisitPage NewVisitViewModel
        {
            get { return ServiceLocator.Current.GetInstance<VMNewVisitPage>(); }
        }

        /// <summary>
        /// Search visitor viewmodel instance.
        /// </summary>
        public VMSearchVisitorPage SearchVisitorViewModel
        {
            get { return ServiceLocator.Current.GetInstance<VMSearchVisitorPage>(); }
        }

        /// <summary>
        /// Search employee viewmodel instance.
        /// </summary>
        public VMSearchEmployeePage SearchEmployeeViewModel
        {
            get { return ServiceLocator.Current.GetInstance<VMSearchEmployeePage>(); }
        }

        /// <summary>
        /// New visitor viewmodel instance.
        /// </summary>
        public VMNewVisitorPage NewVisitorViewModel
        {
            get { return ServiceLocator.Current.GetInstance<VMNewVisitorPage>(); }
        }       


    }
}
