using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageFunctions.Classes
{
	class FileIO
	{
		public delegate void MediaSelectedHandler(string PathToMedia);
		public event MediaSelectedHandler SingleMediaSelected;
		public delegate void MediaSelectionCancelledHandler();
		public event MediaSelectionCancelledHandler MediaSelectionCancelled;

		private static string LastDirectory = null;	// Remembers which directory the user last went to.

		/// <summary>
		/// Instantiates the Class
		/// </summary>
		public FileIO()
		{

		}

		/// <summary>
		/// Open a Single Media File
		/// Raises MediaSelectedHandler Event when a file is chosen
		/// Raises MediaSelectionCancelledHandler Event if the User Cancels
		/// </summary>
		public void OpenFile()
		{
			OpenFileDialog ofd = new OpenFileDialog();

			// Configure the Dialog
			ofd.AutoUpgradeEnabled = true;	// Upgrades appearance on Vista
			ofd.CheckFileExists = true;
			ofd.CheckPathExists = true;
			ofd.DereferenceLinks = true;	// convert .lnk to real paths
			if (string.IsNullOrWhiteSpace(LastDirectory))
				ofd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); //TODO: Allow End user to set this in the application Preferences
			else
				ofd.InitialDirectory = LastDirectory;

			ofd.Multiselect = false; // One File only!
			ofd.ReadOnlyChecked = true;	//TODO: Allow End User to decide in Application Preferences
			ofd.RestoreDirectory = true;	// Next time we open this - go to the previous selected folder.
			ofd.ShowReadOnly = false; //TODO: Let EU Decide
			ofd.SupportMultiDottedExtensions = true; // Quite common these days
			ofd.Title = "Select a media file to load"; //TODO: Globalization
			ofd.ValidateNames = false;	// Doesn't have to be valid Win32 file name

			// Open the dialog
			DialogResult dr = ofd.ShowDialog();
			if (dr == DialogResult.OK)
			{
				FileInfo finfo = new FileInfo(ofd.FileName);
				if (SingleMediaSelected != null) SingleMediaSelected(ofd.FileName);
				LastDirectory = finfo.DirectoryName;
			}
			else
				if (MediaSelectionCancelled != null) MediaSelectionCancelled();

			ofd.Dispose(); // Clean up our resources now.
		}

	}
}
