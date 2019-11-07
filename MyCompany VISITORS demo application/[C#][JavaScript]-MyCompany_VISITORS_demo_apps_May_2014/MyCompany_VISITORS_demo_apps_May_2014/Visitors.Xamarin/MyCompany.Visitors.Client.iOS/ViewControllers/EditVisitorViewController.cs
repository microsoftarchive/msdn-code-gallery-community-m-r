using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MonoTouch.CoreGraphics;
using MonoTouch.Dialog;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MyCompany.Visitors.Client.iOS;
using MyCompany.Visitors.Client;
using MyCompany.Visitors.Client.ViewModels;
using Xamarin.Media;


namespace MyCompany.Visitors.Client
{
	class EditVisitorViewController : UIViewController
	{

		public Action<Visitor> Selected { get; set; }
		readonly EditVisitorView view;
		UIBarButtonItem saveButton;
		public EditVisitorViewController()
		{
			View = view = new EditVisitorView(this);
			this.NavigationItem.RightBarButtonItem = saveButton = new UIBarButtonItem(UIBarButtonSystemItem.Save, (sender, args) => Save());
		}

		bool isSaving;
		async Task Save()
		{
			if (isSaving)
				return;
			isSaving = true;
			var spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
			spinner.StartAnimating();
			this.NavigationItem.RightBarButtonItem = new UIBarButtonItem(spinner);
			try
			{
				if (!validate())
					return;
				view.UpdateVisitor();

				var visitor = new Visitor
				{
					VisitorId = Visitor.Visitor.VisitorId,
					Company = Visitor.Visitor.Company,
					CreatedDateTime = Visitor.Visitor.CreatedDateTime,
					Email = Visitor.Visitor.Email,
					FirstName = Visitor.Visitor.FirstName,
					LastModifiedDateTime = DateTime.Now,
					LastName = Visitor.Visitor.LastName,
					LastVisit = Visitor.Visitor.LastVisit,
					PersonalId = Visitor.Visitor.PersonalId,
					Position = Visitor.Visitor.Position,
				};
				if (Visitor.Visitor.VisitorId == 0)
				{
					var result = await AppDelegate.CompanyClient.VisitorService.Add(visitor);
					if (result > 0)
					{
						Visitor.Visitor.VisitorId = result;
						VMMainPage.MainPage.Visitors.Add(Visitor);
					}
				}
				else
				{
					await AppDelegate.CompanyClient.VisitorService.Update(visitor);
				}

				if (Visitor.HasPhotoshanged)
				{

					var newVisitorPictures = Visitor.Pictures;

					foreach (var visitorPicture in newVisitorPictures)
					{
						visitorPicture.VisitorId = Visitor.Visitor.VisitorId;
					}

					await AppDelegate.CompanyClient.VisitorPictureService.AddOrUpdatePictures(newVisitorPictures);
					Visitor.HasPhotoshanged = false;
				}
				if(Selected !=null)
					Selected(Visitor.Visitor);
				this.NavigationController.PopViewControllerAnimated(true);

			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
				new UIAlertView("Error", "There was an error saving the visitor", null, "Ok").Show();
			}
			finally
			{
				isSaving = false;
				this.NavigationItem.RightBarButtonItem = saveButton;
			}
		}


		public bool validate()
		{
			return true;
		}
		public VMVisitor Visitor
		{
			get { return view.Visitor; }
			set
			{
				view.Visitor = value;
				Title = value.Visitor.VisitorId == 0 ? "Add a visitor" : "Edit visitor";
			}
		}

		class EditVisitorView : UIView
		{
			UIButton image;
			const float imageWidth = 150;
			const float tableWidth = 485f;
			EntryElement firstName;
			EntryElement lastName;
			EntryElement idNumber;
			EntryElement email;
			EntryElement company;
			EntryElement title;
			DialogViewController dvc;
			VMVisitor visitor;

			public EditVisitorView(EditVisitorViewController parent)
			{
				BackgroundColor = UIColor.FromRGB(239, 239, 244);

				image = new UIButton
				{
					Frame = new RectangleF(0, 0, 150, 150),
					TintColor = UIColor.White,
					Layer =
					{
						CornerRadius = 75,
						MasksToBounds = true,
					}
				};
				image.SetTitle("Change photo", UIControlState.Normal);
				image.ImageView.ContentMode = UIViewContentMode.ScaleAspectFill;;
				image.SetImage(Theme.UserImageDefaultLight.Value,UIControlState.Normal);
				image.TouchUpInside += async (sender, args) =>
				{
					try
					{
						var picker = new MediaPicker();
						var controller = picker.GetTakePhotoUI(new StoreCameraMediaOptions
						{
							Name = "test.jpg",
							Directory = "MediaPickerSample"
						});

						parent.PresentViewController(controller, true, null);

						var result = await controller.GetResultAsync();
						var i = UIImage.FromFile(result.Path).ResizeImage(380,380);
						NSError error;
						i.AsJPEG().Save(result.Path, NSDataWritingOptions.FileProtectionNone, out error);
						
						Console.WriteLine("Result came back");
						Console.WriteLine(result);
						Console.WriteLine(result.Path);
						
						var picture = new VisitorPicture
						{
							Content = File.ReadAllBytes(result.Path),
							PictureType = PictureType.Small,
						};

						var picture2 = new VisitorPicture
						{
							Content = File.ReadAllBytes(result.Path),
							PictureType = PictureType.Big,
						};

						image.SetImage(UIImage.FromFile(result.Path) ?? Theme.UserImageDefaultLight.Value, UIControlState.Normal);
						Visitor.ClearPhotos();
						Visitor.AddPicture(picture);
						Visitor.AddPicture(picture2);
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex);
					}
					finally
					{
						parent.DismissViewController(true,null);
					}
					
				};

				AddSubview(image);

				dvc = new DialogViewController(new RootElement("Visitor")
				{
					new Section("Visitor")
					{
						(firstName = new EntryElement("First name", "John","")),
						(lastName = new EntryElement("Last name","Apleseed","")),
						(idNumber = new EntryElement("Id number","123456","")),
						(email = new EntryElement("Email", "visitor@mycompanydemo.com","")),
						(company = new EntryElement("Organization/ Company","MyCompany","")),
						(title = new EntryElement("Proffesional Title","CEO",""))
					}
				})
				{
					TableView =
					{
						SectionHeaderHeight = 0,
						BackgroundColor = UIColor.White,
						Layer =
						{
							CornerRadius = 5,
							MasksToBounds = true
						}
					}
				};
				
				this.AddSubview(dvc.View);
				parent.AddChildViewController(dvc);
			}

			public VMVisitor Visitor
			{
				get { return visitor; }
				set
				{
					visitor = value; 
					SetupVisitor();
				}
			}

			void SetupVisitor()
			{
				firstName.Value = Visitor.Visitor.FirstName;
				lastName.Value = Visitor.Visitor.LastName;
				idNumber.Value = Visitor.Visitor.PersonalId;
				email.Value = Visitor.Visitor.Email;
				company.Value = Visitor.Visitor.Company;
				title.Value = Visitor.Visitor.Position; 
				image.SetImage(Visitor.VisitorPhoto.ToImage() ?? Theme.UserImageDefaultLight.Value, UIControlState.Normal);
			}

			public void UpdateVisitor()
			{
				Visitor.Visitor.FirstName = firstName.Value;
				Visitor.Visitor.LastName = lastName.Value;
				Visitor.Visitor.PersonalId = idNumber.Value;
				Visitor.Visitor.Email = email.Value;
				Visitor.Visitor.Company = company.Value;
				Visitor.Visitor.Position = title.Value;
			}

			public override void LayoutSubviews()
			{
				const float topH = 64;
				base.LayoutSubviews();

				var bounds = Bounds;
				var midx = bounds.GetMidX();

				var frame = image.Frame;
				frame.X = midx - frame.Width/2;
				frame.Y = topH;
				image.Frame = frame;

				bounds.Y = frame.Bottom + 40;
				bounds.Height -= bounds.Y + 20;
				bounds.Width = tableWidth;
				bounds.X = midx - bounds.Width/2;

				dvc.View.Frame = bounds;
			}
		}
	}
}