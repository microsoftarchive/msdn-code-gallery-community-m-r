using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MyCompany.Visitors.Client.ViewModel.Base;


namespace MyCompany.Visitors.Client
{
	public class VMVisitor : VMBase
	{
		Visitor visitor;

		public Visitor Visitor
		{
			get { return visitor; }
			set { visitor = value; }
		}

		public string FullName
		{
			get { return string.Format("{0} {1}", Visitor.FirstName, Visitor.LastName); }
		}

		/// <summary>
		///     Visitor photo.
		/// </summary>
		public VisitorPicture VisitorPhoto
		{
			get
			{
				if ((Visitor.VisitorPictures != null) && (Visitor.VisitorPictures.Any()))
					return Visitor.VisitorPictures.First();
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
				if ((Visitor.VisitorPictures != null) && (Visitor.VisitorPictures.Any()))
				{
					return Visitor.VisitorPictures.First().Content;
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
				if ((Visitor.VisitorPictures != null) &&
					(Visitor.VisitorPictures.Any(p => p.PictureType == PictureType.Small))
					&& Visitor.VisitorPictures.Any(v => v.PictureType == PictureType.Small))
				{
					return Visitor.VisitorPictures.First(v => v.PictureType == PictureType.Small).Content;
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
				if ((Visitor.VisitorPictures != null) &&
					(Visitor.VisitorPictures.Any(p => p.PictureType == PictureType.Big)))
				{
					return Visitor.VisitorPictures.First(p => p.PictureType == PictureType.Big);
				}
				return null;
			}
		}

		public void ChangePhotos(ICollection<VisitorPicture> visitorPictures)
		{
			Visitor.VisitorPictures = visitorPictures;
			RaisePropertyChanged(() => VisitorPhoto);
		}

		public void AddPicture(VisitorPicture picture)
		{
			if(visitor.VisitorPictures == null)
				visitor.VisitorPictures = new	Collection<VisitorPicture>();
			HasPhotoshanged = true;
			Visitor.VisitorPictures.Add(picture);
			RaisePropertyChanged(() => VisitorPhoto);
		}
		public bool HasPhotoshanged { get; set; }

		public void ClearPhotos()
		{
			visitor.VisitorPictures = new Collection<VisitorPicture>();
		}
		public VisitorPicture[] Pictures
		{
			get { return visitor.VisitorPictures.ToArray(); }
		}
	}
}