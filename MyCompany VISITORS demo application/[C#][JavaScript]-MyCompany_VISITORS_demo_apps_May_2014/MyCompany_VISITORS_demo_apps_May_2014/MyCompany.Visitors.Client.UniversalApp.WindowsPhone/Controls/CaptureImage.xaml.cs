using MyCompany.Visitors.Client.UniversalApp.WindowsPhone.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MyCompany.Visitors.Client.UniversalApp.WindowsPhone.Controls
{
    public sealed partial class CaptureImage : UserControl
    {
        private VMCaptureImage vm;
        
        public StorageFile CapturedImage
        {
            get { return (StorageFile)GetValue(CapturedImageProperty); }
            set { SetValue(CapturedImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CapturedImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CapturedImageProperty =
            DependencyProperty.Register("CapturedImage", typeof(StorageFile), typeof(CaptureImage), new PropertyMetadata(null, (d, val) =>
        {
            if (val.NewValue != null)
            {
                VMCaptureImage vm = ((d as CaptureImage).DataContext as VMCaptureImage);
                if (vm.CapturedImage != (val.NewValue as StorageFile))
                    vm.CapturedImage = (val.NewValue as StorageFile);
            }
        }));

        public CaptureImage()
        {
            this.InitializeComponent();
            this.Loaded += CaptureImage_Loaded;
            this.Unloaded += CaptureImage_Unloaded;            
            
            vm = (VMCaptureImage)this.DataContext;
            vm.PropertyChanged += VmPropertyChanged;
        }

        async void CaptureImage_Loaded(object sender, RoutedEventArgs e)
        {
             capturePreview.Source = await vm.Initialize();
        } 

        async void CaptureImage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.Loaded -= CaptureImage_Loaded;
            this.Unloaded -= CaptureImage_Unloaded;
            await vm.Dispose();
        }

        private void VmPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "CapturedImage")
            {
                CapturedImage = (sender as VMCaptureImage).CapturedImage;
            }          
        }
    }
}
