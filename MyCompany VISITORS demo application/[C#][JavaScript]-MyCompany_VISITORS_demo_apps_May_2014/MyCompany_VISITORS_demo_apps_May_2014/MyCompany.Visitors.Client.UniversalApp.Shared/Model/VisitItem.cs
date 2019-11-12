namespace MyCompany.Visitors.Client.UniversalApp.Model
{
    using GalaSoft.MvvmLight.Command;
using MyCompany.Visitors.Client.DocumentResponse;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Windows.Storage.Streams;
using Windows.System;
using Windows.UI.Xaml.Media.Imaging;

    /// <summary>
    /// Represents a visit to bind to the UI.
    /// </summary>
    public class VisitItem
    {
        private const string NOIMAGE_PATH = "ms-appx:///Assets/no_photo_big.png";
        private const string NOIMAGE2_PATH = "ms-appx:///Assets/no_photo2_big.png";
        private RelayCommand launchLyncCommand;
        private Visit visit;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="visit"></param>
        /// <param name="groupId"></param>
        public VisitItem(Visit visit, int groupId)
        {
            GroupId = groupId;
            this.visit = visit;

            this.launchLyncCommand = new RelayCommand(LaunchLyncCommandExecute, CanLaunchLyncCommandExecute);
        }

        /// <summary>
        /// Return the visit identification.
        /// </summary>
        public int VisitId {
            get
            {
                return this.visit.VisitId;
            }
        }

        /// <summary>
        /// Return the visitor identification.
        /// </summary>
        public int VisitorId
        {
            get { return this.visit.VisitorId; }
        }

        /// <summary>
        /// Return if visitor has arrived.
        /// </summary>
        public bool HasArrived
        {
            get { return this.visit.Status == VisitStatus.Arrived; }
        }

        /// <summary>
        /// Group id to show this visit on.
        /// </summary>
        public int GroupId { get; set; }

        /// <summary>
        /// Full visitor name.
        /// </summary>
        public string VisitorName 
        {
            get
            {
                return string.Format("{0} {1}", this.visit.Visitor.FirstName, this.visit.Visitor.LastName);
            }
        }

        /// <summary>
        /// Full employee name.
        /// </summary>
        public string EmployeeName
        {
            get
            {
                return string.Format("{0} {1}", this.visit.Employee.FirstName, this.visit.Employee.LastName);
            }
        }

        /// <summary>
        /// Visitor company name.
        /// </summary>
        public string CompanyName 
        {
            get { return this.visit.Visitor.Company; }
        }

        /// <summary>
        /// VisitDate.
        /// </summary>
        public DateTime VisitDate
        {
            get { return this.visit.VisitDateTime; }
        }

        /// <summary>
        /// Plate.
        /// </summary>
        public string Plate
        {
            get { return this.visit.Plate; }
        }

        /// <summary>
        /// Has car.
        /// </summary>
        public bool HasCar
        {
            get { return this.visit.HasCar; }
        }

        /// <summary>
        /// VisitTime.
        /// </summary>
        public string VisitTime
        {
            get { return this.visit.VisitDateTime.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern); }
        }

        /// <summary>
        /// Visit formatted date in current culture.
        /// </summary>
        public string VisitFormattedDate
        {
            get { return this.visit.VisitDateTime.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern); }
        }

        /// <summary>
        /// Visitr formatted hour in current culture.
        /// </summary>
        public string VisitFormattedTime
        {
            get { return this.visit.VisitDateTime.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern); }
        }

        /// <summary>
        /// Visit comment.
        /// </summary>
        public string Comment 
        {
            get { return this.visit.Comments; }
        }

        /// <summary>
        /// Visitor photo.
        /// </summary>
        public BitmapImage VisitorPhoto 
        {
            get
            {
                if((this.visit.Visitor.VisitorPictures != null) && (this.visit.Visitor.VisitorPictures.Any()))
                {
                    using (var ms = new InMemoryRandomAccessStream())
                    {
                        using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                        {
                            writer.WriteBytes((byte[]) this.visit.Visitor.VisitorPictures.First().Content);
                            writer.StoreAsync().GetResults();
                        }
                        var image = new BitmapImage();
                        image.SetSource(ms);
                        return image;
                    }
                }
                return new BitmapImage(new Uri(NOIMAGE_PATH));
            }
        }

        /// <summary>
        /// Visitor photo original bytes.
        /// </summary>
        public byte[] VisitorPhotoBytes
        {
            get
            {
                if((this.visit.Visitor.VisitorPictures != null) && (this.visit.Visitor.VisitorPictures.Any()))
                {
                    return this.visit.Visitor.VisitorPictures.First().Content;
                }
                return null;
            }
        }

        /// <summary>
        /// Visitor small photo original bytes.
        /// </summary>
        public byte[] VisitorSmallPhotoBytes
        {
            get
            {
                if ((this.visit.Visitor.VisitorPictures != null) && (this.visit.Visitor.VisitorPictures.Any(p => p.PictureType == PictureType.Small))
                    && this.visit.Visitor.VisitorPictures.Any(v => v.PictureType == PictureType.Small))
                {
                    return this.visit.Visitor.VisitorPictures.First(v => v.PictureType == PictureType.Small).Content;
                }
                return null;
            }
        }

        /// <summary>
        /// Visitor photo.
        /// </summary>
        public BitmapImage VisitorBigPhoto
        {
            get
            {
                if ((this.visit.Visitor.VisitorPictures != null) && (this.visit.Visitor.VisitorPictures.Any(p => p.PictureType == PictureType.Big)))
                {
                    using (var ms = new InMemoryRandomAccessStream())
                    {
                        using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                        {
                            writer.WriteBytes(this.visit.Visitor.VisitorPictures.First(p => p.PictureType == PictureType.Big).Content);
                            writer.StoreAsync().GetResults();
                        }
                        var image = new BitmapImage();
                        image.SetSource(ms);
                        return image;
                    }
                }
                return new BitmapImage(new Uri(NOIMAGE2_PATH));
            }
        }

        /// <summary>
        /// Employee photo.
        /// </summary>
        public BitmapImage EmployeePhoto
        {
            get
            {
                if ((this.visit.Employee.EmployeePictures != null) && (this.visit.Employee.EmployeePictures.Any(e => e.PictureType == PictureType.Big)))
                {
                    using (var ms = new InMemoryRandomAccessStream())
                    {
                        using (var writer = new DataWriter(ms.GetOutputStreamAt(0)))
                        {
                            writer.WriteBytes((byte[]) this.visit.Employee.EmployeePictures.First(e => e.PictureType == PictureType.Big).Content);
                            writer.StoreAsync().GetResults();
                        }
                        var image = new BitmapImage();
                        image.SetSource(ms);
                        return image;
                    }
                }
                return new BitmapImage(new Uri(NOIMAGE_PATH));
            }
        }

        /// <summary>
        /// Gets the visitor
        /// </summary>
        public Visitor Visitor
        {
            get
            {
                return this.visit.Visitor;
            }
        }

        /// <summary>
        /// Tell employee his visit has arrive.
        /// </summary>
        public ICommand LaunchLyncCommand
        {
            get { return this.launchLyncCommand; }
        }

        public void ChangePhotos(ICollection<VisitorPicture> visitorPictures) {
            this.visit.Visitor.VisitorPictures = visitorPictures;            
        }

        private bool CanLaunchLyncCommandExecute()
        {
            return !string.IsNullOrWhiteSpace(this.visit.Employee.Email);
        }

        private async void LaunchLyncCommandExecute()
        { 
            string uri = string.Format("sip:{0}", this.visit.Employee.Email);
            await Launcher.LaunchUriAsync(new Uri(uri));
        }
    }
}
