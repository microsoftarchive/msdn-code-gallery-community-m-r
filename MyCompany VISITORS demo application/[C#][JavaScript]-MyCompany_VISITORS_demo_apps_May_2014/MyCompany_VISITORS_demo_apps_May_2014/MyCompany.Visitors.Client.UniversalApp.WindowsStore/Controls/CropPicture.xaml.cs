namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Controls
{
    using MyCompany.Visitors.Client.UniversalApp.WindowsStore.ViewModel;
    using Windows.Storage;
    using Windows.UI.Xaml;

    /// <summary>
    /// Crop Picture control.
    /// </summary>
    public sealed partial class CropPicture
    {
        private static DependencyProperty ImageCroppedProperty = DependencyProperty.Register("ImageCropped", typeof (StorageFile), typeof (CropPicture), null);
        private static DependencyProperty ImageToCropProperty = DependencyProperty.Register("ImageToCrop", typeof(StorageFile), typeof(CropPicture),new PropertyMetadata(null, (d, val) =>
        {
            if (val.NewValue != null)
            {
                VMCropPicture vm = ((d as CropPicture).DataContext as VMCropPicture);
                if (vm.ImageToCrop != (val.NewValue as StorageFile))
                    vm.ImageToCrop = (val.NewValue as StorageFile);
            }
        }));

        /// <summary>
        /// Image Cropped Property.
        /// </summary>
        public StorageFile ImageCropped
        {
            get { return (StorageFile)this.GetValue(ImageCroppedProperty); }
            set { this.SetValue(ImageCroppedProperty,value); }
        }

        /// <summary>
        /// Image to Crop Property.
        /// </summary>
        public StorageFile ImageToCrop
        {
            get { return (StorageFile)this.GetValue(ImageToCropProperty); }
            set { this.SetValue(ImageToCropProperty, value); }
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public CropPicture()
        {
            this.InitializeComponent();
            VMCropPicture vm = (this.DataContext as VMCropPicture);

            vm.InitializeData();
            vm.IsEnabledConfirmButtom = false;
            vm.PropertyChanged += VmPropertyChanged;
        }

        private void VmPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ImageCropped")
            {
                ImageCropped = (sender as VMCropPicture).ImageCropped;
            }
        }
    }
}
