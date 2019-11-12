// <generated />
namespace DialogAction
{
	using System;

	[System.ComponentModel.TypeDescriptionProvider(typeof(MainWindowViewModelAssociatedMetadataTypeTypeDescriptionProvider))]
	public partial class MainWindowViewModel
	{
		
		#region "OpenFileコマンド"
		private Microsoft.Practices.Prism.Commands.DelegateCommand _OpenFileCommand;
		
		partial void OpenFileExecute();
		
		partial void CanOpenFileExecute(ref bool result);
		
		partial void OpenFileError(Exception ex, ref bool handleError);
	
		public Microsoft.Practices.Prism.Commands.DelegateCommand OpenFileCommand
		{
			get
			{
				return this._OpenFileCommand = this._OpenFileCommand ??
					new Microsoft.Practices.Prism.Commands.DelegateCommand(
						() => 
						{
							try
							{
								this.OpenFileExecute();
							}
							catch (Exception ex)
							{
								bool handleError = false;
								this.OpenFileError(ex, ref handleError);
								if (!handleError)
								{
									throw;
								}
							}
						},
						() =>
						{
							bool ret = true;
							this.CanOpenFileExecute(ref ret);
							return ret;
						});
			}
		}
		#endregion

		#region "FileNameプロパティ"
		private string _FileName;

		partial void FileNameChanging(string newValue, ref bool canSetValue);
		partial void FileNameChanged();

		public string FileName
		{
			get
			{
				return this._FileName;
			}
			
			set
			{
				if (this._FileName == value)
				{
					return;
				}
				
				bool canSetValue = true;
				this.FileNameChanging(value, ref canSetValue);
				if (!canSetValue)
				{
					return;
				}
				
				this._FileName = value;
				this.RaisePropertyChanged("FileName");
				this.FileNameChanged();
			}
		}
		#endregion
		
		class MainWindowViewModelAssociatedMetadataTypeTypeDescriptionProvider : System.ComponentModel.DataAnnotations.AssociatedMetadataTypeTypeDescriptionProvider
		{
			public MainWindowViewModelAssociatedMetadataTypeTypeDescriptionProvider()
				: base(typeof(MainWindowViewModel))
			{
			}
		}
	}
		
}
