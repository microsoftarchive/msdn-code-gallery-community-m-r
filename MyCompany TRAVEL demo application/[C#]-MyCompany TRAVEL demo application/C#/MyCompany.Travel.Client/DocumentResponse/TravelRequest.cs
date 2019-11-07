
namespace MyCompany.Travel.Client
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Travel Entity
    /// </summary>
    public class TravelRequest : INotifyPropertyChanged
    {
        private int _travelRequestId;
        /// <summary>
        /// UniqueId
        /// </summary>
        public int TravelRequestId {
            get { return _travelRequestId; }
            set
            {
                _travelRequestId = value;
                RaisePropertyChanged();
            }
        }

        private string _name;
        /// <summary>
        /// Name
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private string _description;
        /// <summary>
        /// Description
        /// </summary>
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                RaisePropertyChanged();
            }
        }

        private TravelType _travelType;
        /// <summary>
        /// Travel Type
        /// </summary>
        public TravelType TravelType
        {
            get { return _travelType; }
            set
            {
                _travelType = value;
                RaisePropertyChanged();
            }
        }

        private string _from;
        /// <summary>
        /// From city
        /// </summary>
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                RaisePropertyChanged();
            }
        }

        private string _to;
        /// <summary>
        /// To city
        /// </summary>
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _depart;
        /// <summary>
        /// Depart date
        /// </summary>
        public DateTime Depart
        {
            get { return _depart; }
            set
            {
                _depart = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _return;
        /// <summary>
        /// Return date
        /// </summary>
        public DateTime Return
        {
            get { return _return; }
            set
            {
                _return = value;
                RaisePropertyChanged();
            }
        }

        private string _relatedProject;
        /// <summary>
        /// Related Project
        /// </summary>
        public string RelatedProject
        {
            get { return _relatedProject; }
            set
            {
                _relatedProject = value;
                RaisePropertyChanged();
            }
        }

        private string _transportationNeed;
        /// <summary>
        /// Transportation
        /// </summary>
        public string TransportationNeed
        {
            get { return _transportationNeed; }
            set
            {
                _transportationNeed = value;
                RaisePropertyChanged();
            }
        }

        private string _accommodationNeed;
        /// <summary>
        /// Accommodation
        /// </summary>
        public string AccommodationNeed
        {
            get { return _accommodationNeed; }
            set
            {
                _accommodationNeed = value;
                RaisePropertyChanged();
            }
        }

        private string _comments;
        /// <summary>
        /// Comments
        /// </summary>
        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _creationDate;
        /// <summary>
        /// CreationDate
        /// </summary>
        public DateTime CreationDate
        {
            get { return _creationDate; }
            set
            {
                _creationDate = value;
                RaisePropertyChanged();
            }
        }

        private DateTime _lastModifiedDate;
        /// <summary>
        /// Last Modified Date
        /// </summary>
        public DateTime LastModifiedDate
        {
            get { return _lastModifiedDate; }
            set
            {
                _lastModifiedDate = value;
                RaisePropertyChanged();
            }
        }

        private TravelRequestStatus _status;
        /// <summary>
        ///Travel Request Status
        /// </summary>
        public TravelRequestStatus Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged();
            }
        }

        private int _employeeId;
        /// <summary>
        /// EmployeeId
        /// </summary>
        public int EmployeeId
        {
            get { return _employeeId; }
            set
            {
                _employeeId = value;
                RaisePropertyChanged();
            }
        }

        private Employee _employee;
        /// <summary>
        /// Employee
        /// </summary>
        public Employee Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                RaisePropertyChanged();
            }
        }

        private ICollection<TravelAttachment> _travelAttachments;
        /// <summary>
        /// Travel Attachments
        /// </summary>
        public ICollection<TravelAttachment> TravelAttachments {
            get { return _travelAttachments; }
            set
            {
                _travelAttachments = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Employee Photo.
        /// </summary>
        public byte[] EmployeePhoto
        {
            get
            {
                if (Employee==null || Employee.EmployeePictures == null)
                    return null;

                return Employee.EmployeePictures.Select(ep => ep.Content).FirstOrDefault();
            }
        }

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

    }
}
