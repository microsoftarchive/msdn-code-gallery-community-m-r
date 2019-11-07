using System.Threading.Tasks;
using MuPDFWinRT;
using System.ComponentModel;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace PDFReader
{
    public class DocumentPage : INotifyPropertyChanged
    {
        WriteableBitmap writeableBitmap;
        public Document Document
        {
            get;
            private set;
        }

        public int PageNumber
        {
            get;
            private set;
        }

        public double Width
        {
            get;
            internal set;
        }

        public double Height
        {
            get;
            internal set;
        }

        public float ZoomFactor
        {
            get;
            set;
        }

        public WriteableBitmap Image
        {
            get
            {
                if (writeableBitmap == null)
                {
                    RefreshImageAsync();
                }
                return writeableBitmap;
            }
        }

        public bool IsDoublePage
        {
            get
            {
                return PageNumber > 0 && PageNumber < Document.PageCount - 2;
            }
        }

        public DocumentPage(Document document, int pageNumber, double displayHeight)
        {
            Document = document;
            PageNumber = pageNumber;

            var size = document.GetPageSize(pageNumber);

            Width = IsDoublePage ? size.X * 2 : size.X;
            Height = displayHeight;

            ZoomFactor = (float)(size.Y / displayHeight);
        }

        public async Task RefreshImageAsync()
        {
            writeableBitmap = await ConstructPageAsync();
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Image"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        async Task<WriteableBitmap> ConstructPageAsync()
        {
            var size = Document.GetPageSize(PageNumber);
            var width = (int)(size.X * ZoomFactor);
            var height = (int)(size.Y * ZoomFactor);

            var image = new WriteableBitmap(IsDoublePage ? width * 2 : width, height);
            IBuffer buf = new Buffer(image.PixelBuffer.Capacity);
            buf.Length = image.PixelBuffer.Length;

            if (IsDoublePage)
            {
                await Task.Run(() => Document.DrawFirtPageConcurrent(PageNumber, buf, width, height));
                await Task.Run(() => Document.DrawSecondPageConcurrent(PageNumber + 1, buf, width, height));
            }
            else
            {
                Document.DrawPage(PageNumber, buf, 0, 0, width, height, false);
            }

            // copy the buffer to the WriteableBitmap ( UI Thread )
            using (var stream = buf.AsStream())
            {
                await stream.CopyToAsync(image.PixelBuffer.AsStream());
            }

            return image;
        }
    }
}
