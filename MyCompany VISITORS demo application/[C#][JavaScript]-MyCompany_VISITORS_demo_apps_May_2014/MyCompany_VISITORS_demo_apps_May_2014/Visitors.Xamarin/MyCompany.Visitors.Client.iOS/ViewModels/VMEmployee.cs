using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyCompany.Visitors.Client.ViewModel.Base;


namespace MyCompany.Visitors.Client
{
	public class VMEmployee : VMBase
	{
		Employee visitor;

		public Employee Employee
		{
			get { return visitor; }
			set { visitor = value; }
		}

		public string FullName
		{
			get { return string.Format("{0} {1}", Employee.FirstName, Employee.LastName); }
		}

		/// <summary>
		///     Employee photo.
		/// </summary>
		public EmployeePicture EmployeePhoto
		{
			get
			{
				if ((Employee.EmployeePictures != null) && (Employee.EmployeePictures.Any()))
					return Employee.EmployeePictures.First();
				return null;
			}
		}

		/// <summary>
		///     Employee photo original bytes.
		/// </summary>
		public byte[] EmployeePhotoBytes
		{
			get
			{
				if ((Employee.EmployeePictures != null) && (Employee.EmployeePictures.Any()))
				{
					return Employee.EmployeePictures.First().Content;
				}
				return null;
			}
		}

		/// <summary>
		///     Employee small photo original bytes.
		/// </summary>
		public byte[] EmployeeSmallPhotoBytes
		{
			get
			{
				if ((Employee.EmployeePictures != null) &&
					(Employee.EmployeePictures.Any(p => p.PictureType == PictureType.Small))
					&& Employee.EmployeePictures.Any(v => v.PictureType == PictureType.Small))
				{
					return Employee.EmployeePictures.First(v => v.PictureType == PictureType.Small).Content;
				}
				return null;
			}
		}

		/// <summary>
		///     Employee photo.
		/// </summary>
		public EmployeePicture EmployeeBigPhoto
		{
			get
			{
				if ((Employee.EmployeePictures != null) &&
					(Employee.EmployeePictures.Any(p => p.PictureType == PictureType.Big)))
				{
					return Employee.EmployeePictures.First(p => p.PictureType == PictureType.Big);
				}
				return null;
			}
		}

		public void ChangePhotos(ICollection<EmployeePicture> visitorPictures)
		{
			Employee.EmployeePictures = visitorPictures;
			RaisePropertyChanged(() => EmployeePhoto);
		}
	}
}