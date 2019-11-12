using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyCompany.Visitors.Client.DocumentResponse;
using MyCompany.Visitors.Client.ViewModel.Base;
#if __IOS__
using MyCompany.Visitors.Client.DocumentResponse;
#endif

namespace MyCompany.Visitors.Client.Model
{
	/// <summary>
	///     Represents a visit to bind to the UI.
	/// </summary>
	public class VMVisit : VMBase
	{
		readonly Visit visit;

		public Visit Visit
		{
			get { return visit; }
		}

		/// <summary>
		///     Default constructor.
		/// </summary>
		/// <param name="visit"></param>
		/// <param name="groupId"></param>
		public VMVisit(Visit visit, int groupId)
		{
			GroupId = groupId;
			this.visit = visit;
		}

		/// <summary>
		///     Return the visit identification.
		/// </summary>
		public int VisitId
		{
			get { return visit.VisitId; }
		}

		/// <summary>
		///     Return the visitor identification.
		/// </summary>
		public int VisitorId
		{
			get { return visit.Visitor.VisitorId; }
		}

		/// <summary>
		///     Return if visitor has arrived.
		/// </summary>
		public bool HasArrived
		{
			get { return visit.Status == VisitStatus.Arrived; }
			set
			{
				visit.Status = VisitStatus.Arrived;
				RaisePropertyChanged(() => HasArrived);
			}
		}

		/// <summary>
		///     Group id to show this visit on.
		/// </summary>
		public int GroupId { get; set; }

		/// <summary>
		///     Full visitor name.
		/// </summary>
		public string VisitorName
		{
			get { return string.Format("{0} {1}", visit.Visitor.FirstName, visit.Visitor.LastName); }
		}

		/// <summary>
		///     Full employee name.
		/// </summary>
		public string EmployeeName
		{
			get { return string.Format("{0} {1}", visit.Employee.FirstName, visit.Employee.LastName); }
		}

		/// <summary>
		///     Visitor company name.
		/// </summary>
		public string CompanyName
		{
			get { return visit.Visitor.Company; }
		}

		/// <summary>
		///     VisitDate.
		/// </summary>
		public DateTime VisitDate
		{
			get { return visit.VisitDateTime; }
			set { visit.VisitDateTime = value; }
		}

		/// <summary>
		///     Plate.
		/// </summary>
		public string Plate
		{
			get { return visit.Plate; }
			set { visit.Plate = value; }
		}

		/// <summary>
		///     Has car.
		/// </summary>
		public bool HasCar
		{
			get { return visit.HasCar; }
			set { visit.HasCar = value; }
		}

		/// <summary>
		///     VisitTime.
		/// </summary>
		public string VisitTime
		{
			get { return visit.VisitDateTime.ToLocalTime().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern); }
		}

		/// <summary>
		///     Visit formatted date in current culture.
		/// </summary>
		public string VisitFormattedDate
		{
			get
			{
				return visit.VisitDateTime.ToLocalTime().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
			}
		}

		/// <summary>
		///     Visitr formatted hour in current culture.
		/// </summary>
		public string VisitFormattedTime
		{
			get
			{
				return visit.VisitDateTime.ToLocalTime().ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern);
			}
		}

		/// <summary>
		///     Visit comment.
		/// </summary>
		public string Comment
		{
			get { return visit.Comments; }
			set { visit.Comments = value; }
		}

		/// <summary>
		///     Visitor photo.
		/// </summary>
		public VisitorPicture VisitorPhoto
		{
			get
			{
				if ((visit.Visitor.VisitorPictures != null) && (visit.Visitor.VisitorPictures.Any()))
					return visit.Visitor.VisitorPictures.First();
				return null;
			}
		}

		/// <summary>
		///     Visitor photo original bytes.
		/// </summary>
		public byte[] VisitorPhotoBytes
		{
			get
			{
				if ((visit.Visitor.VisitorPictures != null) && (visit.Visitor.VisitorPictures.Any()))
				{
					return visit.Visitor.VisitorPictures.First().Content;
				}
				return null;
			}
		}

		/// <summary>
		///     Visitor small photo original bytes.
		/// </summary>
		public byte[] VisitorSmallPhotoBytes
		{
			get
			{
				if ((visit.Visitor.VisitorPictures != null) &&
				    (visit.Visitor.VisitorPictures.Any(p => p.PictureType == PictureType.Small))
				    && visit.Visitor.VisitorPictures.Any(v => v.PictureType == PictureType.Small))
				{
					return visit.Visitor.VisitorPictures.First(v => v.PictureType == PictureType.Small).Content;
				}
				return null;
			}
		}

		/// <summary>
		///     Visitor photo.
		/// </summary>
		public VisitorPicture VisitorBigPhoto
		{
			get
			{
				if ((visit.Visitor.VisitorPictures != null) &&
				    (visit.Visitor.VisitorPictures.Any(p => p.PictureType == PictureType.Big)))
				{
					return visit.Visitor.VisitorPictures.First(p => p.PictureType == PictureType.Big);
				}
				return null;
			}
		}

		/// <summary>
		///     Employee photo.
		/// </summary>
		public EmployeePicture EmployeePhoto
		{
			get
			{
				if ((visit.Employee.EmployeePictures != null) && (visit.Employee.EmployeePictures.Any()))
					return visit.Employee.EmployeePictures.First();
				return null;
			}
		}

		/// <summary>
		///     Gets the visitor
		/// </summary>
		public Visitor Visitor
		{
			get { return visit.Visitor; }
			set
			{
				visit.Visitor = value;
				RaisePropertyChanged(() => Visitor);
			}
		}

		public Employee Employee
		{
			get { return visit.Employee; }
			set
			{
				visit.Employee = value;
				RaisePropertyChanged(() => Employee);
			}
		}

		public void ChangePhotos(ICollection<VisitorPicture> visitorPictures)
		{
			visit.Visitor.VisitorPictures = visitorPictures;
			RaisePropertyChanged(() => VisitorPhoto);
		}
	}
}