using MuPDFWinRT;
using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PDFReader
{
    public sealed partial class MainPage
    {
        DocumentPage currentPage;
        Document pdfDocument;
        ScrollViewer scrollViewer;
        readonly List<DocumentPage> pages = new List<DocumentPage>();

        public MainPage()
        {
            InitializeComponent();
        }

        private async void Page_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Loading the document
            var file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync(@"test.pdf");
            using (var stream = await file.OpenReadAsync())
            {
                IBuffer readBuffer;
                using (IInputStream inputStreamAt = stream.GetInputStreamAt(0))
                using (var dataReader = new DataReader(inputStreamAt))
                {
                    uint bufferSize = await dataReader.LoadAsync((uint)stream.Size);
                    readBuffer = dataReader.ReadBuffer(bufferSize);
                }

                pdfDocument = Document.Create(readBuffer, DocumentType.PDF, (int)Windows.Graphics.Display.DisplayProperties.LogicalDpi);
            }

            if (pdfDocument.PageCount == 0)
                return;

            for (var index = 0; index < pdfDocument.PageCount; index++)
            {
                pages.Add(new DocumentPage(pdfDocument, index, ActualHeight));
            }

            flipView.SelectionChanged += flipView_SelectionChanged;
            flipView.Loaded += flipView_Loaded;
            flipView.ItemsSource = pages;
        }

        void flipView_Loaded(object sender, RoutedEventArgs e)
        {
            OnSelectionChanged();
        }

        void flipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectionChanged();
        }

        private void OnSelectionChanged()
        {
            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged -= scrollViewer_ViewChanged;
            }

            if (flipView.SelectedIndex < 0)
            {
                return;
            }

            var dataItem = flipView.ItemContainerGenerator.ContainerFromItem(flipView.SelectedItem);

            if (dataItem == null)
            {
                return;
            }

            scrollViewer = dataItem.GetDescendants().OfType<ScrollViewer>().FirstOrDefault();

            if (scrollViewer != null)
            {
                scrollViewer.ViewChanged += scrollViewer_ViewChanged;

                currentPage = (DocumentPage)flipView.SelectedItem;
            }
        }

        async void scrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {
            if (scrollViewer.ZoomFactor != currentPage.ZoomFactor)
            {
                if (!e.IsIntermediate)
                {
                    currentPage.ZoomFactor = scrollViewer.ZoomFactor;
                    await currentPage.RefreshImageAsync();
                }
            }
        }
    }
}
