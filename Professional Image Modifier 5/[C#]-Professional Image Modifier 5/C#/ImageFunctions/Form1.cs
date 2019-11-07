using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

#region Image Specific References
using ImageFunctions.Modifications.CornerDetection;
using AForge;
using ImageFunctions.Forms;
using System.Threading.Tasks;
using ImageFunctions.Controls;
using AForge.Imaging;
using System.Diagnostics;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using ImageFunctions.Classes;
using ImageFunctions.Classes.TrIDWrappers;

#endregion


namespace ImageFunctions
{
    public partial class Form1 : Form
    {


        #region Forms

        //TODO: To be re-implemented.
        //	private FrmProcessing m_Processing = null; // Not a dockable form

        private bool m_bSaveLayout = true;
        private DeserializeDockContent m_deserializeDockContent;
        private FrmConsole ConsoleWindow = new FrmConsole();
        private FrmFileLoader FileLoader = new FrmFileLoader();
        private FrmHistogram Histogram = new FrmHistogram();
        private FrmModificationProperties ModificationProperties = new FrmModificationProperties();
        private FrmModificationType ModificationTypes = new FrmModificationType();
        private FrmStatistics Statistics = new FrmStatistics();
        private FrmImageDisplay ImageDisplay = new FrmImageDisplay();
        private FrmCommandBox CommandBox = new FrmCommandBox();

        #endregion

        #region Enums


        private enum RGB : int
        {
            Red,
            Green,
            Blue,
        };

        private enum FileType
        {
            Unknown,
            Image,
        };
        #endregion

        #region Private Fields
        private bool IsImage;
        private Dictionary<int, string> dFileLoaderThumbnails = new Dictionary<int, string>();
        private int ThumbnailControlerCounter = 0;
        private string CurrentImage;
        #endregion

        //TODO: Prevent Users from running multiple <Hazardous> operations at the same time

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Loads the current Docking Layout if exists, or creates the default layout.
        /// Generates all the Events that we need to watch for.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            m_deserializeDockContent = new DeserializeDockContent(GetDockContentFromPersistString);
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
            
            if (File.Exists(configFile)) // Load DockingPanel Layout.
                try
                {
                    DockingPanel.LoadFromXml(configFile, m_deserializeDockContent);
                }
                catch (Exception ex)
                {
                    if (ConsoleWindow != null) ConsoleWindow.Log("Error: " + ex.Message);
                }

            else // Create default layout.
            {
                ConsoleWindow.Show(DockingPanel, DockState.DockBottom);
                FileLoader.Show(DockingPanel, DockState.DockLeft);
                Histogram.Show(DockingPanel, DockState.DockLeft);
                Statistics.Show(DockingPanel, DockState.DockLeft);
                ModificationTypes.Show(DockingPanel, DockState.DockRight);
                ModificationProperties.Show(DockingPanel, DockState.DockRight);
                ImageDisplay.Show(DockingPanel, DockState.DockRight);
                CommandBox.Show(DockingPanel, DockState.DockBottom);
            }



            ImageDisplay.FormClosing += ImageDisplay_FormClosing;
            ImageDisplay.MediaFailedToLoad += ImageDisplay_MediaFailedToLoad;
            ImageDisplay.MediaLoaded += ImageDisplay_MediaLoaded;
            ImageDisplay.MediaPixelColour += ImageDisplay_MediaPixelColour;
            ImageDisplay.MediaPixelCoordinates += ImageDisplay_MediaPixelCoordinates;
            ImageDisplay.ImageDisplayLog += ImageDisplay_ImageDisplayLog;
            Histogram.histogramLog += Histogram_histogramStatus;
            Histogram.histogramCompleted += Histogram_histogramCompleted;
            Statistics.StatisticsLog += Statistics_StatisticsLog;
            FileLoader.ThumbnailSelected += FileLoader_ThumbnailSelected;
            ModificationTypes.SusanSelected += ModificationTypes_SusanSelected;
            ModificationTypes.HarrisSelected += ModificationTypes_HarrisSelected;
            ModificationTypes.MoravecSelected += ModificationTypes_MoravecSelected;
            ModificationTypes.FASTSelected += ModificationTypes_FASTSelected;
            ModificationProperties.ModificationPropertiesLog += ModificationProperties_ModificationPropertiesLog;
            ModificationProperties.UpdateImage += ModificationProperties_UpdateImage;
            ModificationProperties.CurrentImage = CurrentImage;
        }

        /// <summary>
        /// Parses the DockingPanel Layout and returns the appropriate Instantiated form as requested by the Persist String.
        /// </summary>
        /// <param name="persistString"></param>
        /// <returns></returns>
        private IDockContent GetDockContentFromPersistString(string persistString)
        {
            switch (persistString)
            {
                case "Console":
                    consoleMenuItem.Checked = true;
                    return ConsoleWindow;

                case "File Loader":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading File Loader window");
                    fileLoaderMenuItem.Checked = true;
                    return FileLoader;

                case "Histogram":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading Histogram window");
                    histogramMenuItem.Checked = true;
                    return Histogram;

                case "Image Display":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading Image Display window");
                    imageDisplayMenuItem.Checked = true;
                    return ImageDisplay;

                case "Modification Properties":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading Modification Properties window");
                    modificationPropertiesMenuItem.Checked = true;
                    return ModificationProperties;

                case "Modification Types":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading Modification Types window");
                    modificationTypesMenuItem.Checked = true;
                    return ModificationTypes;

                case "Exif Statistics":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading Exif Statistics window");
                    statisticsMenuItem.Checked = true;
                    return Statistics;
                case "Command Box":
                    if (ConsoleWindow != null) ConsoleWindow.Log("Loading Command Window");
                    commandInputToolStripMenuItem.Checked = true;
                    return CommandBox;
                default:
                    if (ConsoleWindow != null) ConsoleWindow.Log("Unknown persistString: " + persistString);
                    consoleMenuItem.Checked = true;
                    return ConsoleWindow;

            }
        }

        /// <summary>
        /// Saves the current Docking Layout.
        /// TODO: Save unsaved files and other warnings.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

            if (m_bSaveLayout)
                DockingPanel.SaveAsXml(configFile);
            else if (File.Exists(configFile))
                File.Delete(configFile);
        }

        #region Menu
        #region Windows Menu
        /// <summary>
        /// If the console window is not displayed show it, Create and show if necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void consoleMenuItem_Click(object sender, EventArgs e)
        {
            if (ConsoleWindow != null && consoleMenuItem.Checked == false)
            {
                consoleMenuItem.Checked = true;
                ConsoleWindow.Show(DockingPanel, DockState.DockBottom);
            }
            else if (ConsoleWindow != null && consoleMenuItem.Checked == true)
            {
                consoleMenuItem.Checked = false;
                ConsoleWindow.Close();
            }
            else
            {
                ConsoleWindow = new FrmConsole();
                consoleMenuItem.Checked = true;
                ConsoleWindow.Show(DockingPanel, DockState.DockBottom);
            }
        }

        /// <summary>
        /// If the Statistics window is not displayed show it, Create and show if necessary
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void statisticsMenuItem_Click(object sender, EventArgs e)
        {
            if (Statistics != null && statisticsMenuItem.Checked == false)
            {
                statisticsMenuItem.Checked = true;
                Statistics.Show(DockingPanel, DockState.DockBottom);
            }
            else if (Statistics != null && statisticsMenuItem.Checked == true)
            {
                statisticsMenuItem.Checked = false;
                Statistics.Close();
            }
            else
            {
                Statistics = new FrmStatistics();
                statisticsMenuItem.Checked = true;
                Statistics.Show(DockingPanel, DockState.DockBottom);
            }
        }

        private void commandInputToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CommandBox != null && commandInputToolStripMenuItem.Checked == false)
            {
                commandInputToolStripMenuItem.Checked = true;
                CommandBox.Show(DockingPanel, DockState.DockBottom);
            }
            else if (CommandBox != null && commandInputToolStripMenuItem.Checked == true)
            {
                commandInputToolStripMenuItem.Checked = false;
                CommandBox.Close();
            }
            else
            {
                CommandBox = new FrmCommandBox();
                commandInputToolStripMenuItem.Checked = true;
                CommandBox.Show(DockingPanel, DockState.DockBottom);
            }
        }

        private void histogramMenuItem_Click(object sender, EventArgs e)
        {
            if (Histogram != null && histogramMenuItem.Checked == false)
            {
                histogramMenuItem.Checked = true;
                Histogram.Show(DockingPanel, DockState.DockBottom);
            }
            else if (Histogram != null && histogramMenuItem.Checked == true)
            {
                histogramMenuItem.Checked = false;
                Histogram.Close();
            }
            else
            {
                Histogram = new FrmHistogram();
                histogramMenuItem.Checked = true;
                Histogram.Show(DockingPanel, DockState.DockBottom);
            }
        }

        private void imageDisplayMenuItem_Click(object sender, EventArgs e)
        {
            if (ImageDisplay != null && imageDisplayMenuItem.Checked == false)
            {
                imageDisplayMenuItem.Checked = true;
                ImageDisplay.Show(DockingPanel, DockState.DockBottom);
            }
            else if (ImageDisplay != null && imageDisplayMenuItem.Checked == true)
            {
                imageDisplayMenuItem.Checked = false;
                ImageDisplay.Close();
            }
            else
            {
                ImageDisplay = new FrmImageDisplay();
                imageDisplayMenuItem.Checked = true;
                ImageDisplay.Show(DockingPanel, DockState.DockBottom);
            }
        }

        private void modificationPropertiesMenuItem_Click(object sender, EventArgs e)
        {
            if (ModificationProperties != null && modificationPropertiesMenuItem.Checked == false)
            {
                modificationPropertiesMenuItem.Checked = true;
                ModificationProperties.Show(DockingPanel, DockState.DockBottom);
            }
            else if (ModificationProperties != null && modificationPropertiesMenuItem.Checked == true)
            {
                modificationPropertiesMenuItem.Checked = false;
                ModificationProperties.Close();
            }
            else
            {
                ModificationProperties = new FrmModificationProperties();
                modificationPropertiesMenuItem.Checked = true;
                ModificationProperties.Show(DockingPanel, DockState.DockBottom);
            }
        }

        private void modificationTypesMenuItem_Click(object sender, EventArgs e)
        {
            if (ModificationTypes != null && modificationTypesMenuItem.Checked == false)
            {
                modificationTypesMenuItem.Checked = true;
                ModificationTypes.Show(DockingPanel, DockState.DockBottom);
            }
            else if (ModificationTypes != null && modificationTypesMenuItem.Checked == true)
            {
                modificationTypesMenuItem.Checked = false;
                ModificationTypes.Close();
            }
            else
            {
                ModificationTypes = new FrmModificationType();
                modificationTypesMenuItem.Checked = true;
                ModificationTypes.Show(DockingPanel, DockState.DockBottom);
            }
        }
        #endregion

        #region Help Menu
        /// <summary>
        /// Shows the About Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAboutBox about = new FrmAboutBox();
            about.ShowDialog();
        }
        #endregion
        #endregion

        #region Command Strip
        /// <summary>
        /// Open a Single file for modification
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openfileToolStripButton_Click(object sender, EventArgs e)
        {
            FileIO SingleFileLoad = new FileIO(); ;
            SingleFileLoad.SingleMediaSelected += SingleFileLoad_SingleMediaSelected;
            SingleFileLoad.MediaSelectionCancelled += SingleFileLoad_MediaSelectionCancelled;
            ConsoleWindow.Log("Select Media ");
            SingleFileLoad.OpenFile();
        }

        /// <summary>
        /// Shows all the System Installed Codecs.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSystemCodecInformation_Click_1(object sender, EventArgs e)
        {
            foreach (ImageCodecInfo ici in ImageCodecInfo.GetImageDecoders())
            {
                ConsoleWindow.Log("*************************************************");
                ConsoleWindow.Log("Name: " + ici.CodecName);
                ConsoleWindow.Log("Dll Name: " + ici.DllName);
                ConsoleWindow.Log("Filename Extension(s): " + ici.FilenameExtension);
                //TODO: Work out how to get Flag information.
                //foreach (ImageCodecFlags icf in ici.Flags)
                //{
                //	Log("Codec Flags: " + icf.ToString());
                //}
                ConsoleWindow.Log("Format Description: " + ici.FormatDescription);
                ConsoleWindow.Log("Mime Type: " + ici.MimeType);
                ConsoleWindow.Log("Signature Masks: " + Utilities.ConvertToString(ici.SignatureMasks));
                ConsoleWindow.Log("Signature Patterns: " + Utilities.ConvertToString(ici.SignaturePatterns));
                ConsoleWindow.Log("Codec Version: " + ici.Version);
                ConsoleWindow.Log("*************************************************");
            }
        }
        #endregion


        /// <summary>
        /// The User decided not to open a file.
        /// </summary>
        void SingleFileLoad_MediaSelectionCancelled()
        {
            ConsoleWindow.Log("User Canceled Media Selection ");
        }

        /// <summary>
        /// Send the PathToMedia value to ImageDisplay
        /// </summary>
        /// <param name="PathToMedia"></param>
        void SingleFileLoad_SingleMediaSelected(string PathToMedia)
        {
            if (ImageDisplay != null) // If the image display form exists. Should make sure it is showing as well!
            {
                ConsoleWindow.Log("Loading & Processing Media " + PathToMedia);
                IdentifyFileType fileType = new IdentifyFileType(PathToMedia);
                fileType.IdentifyFileTypeComplete += fileType_IdentifyFileTypeComplete;
                fileType.IdentifyFileTypeError += fileType_IdentifyFileTypeError;
                fileType.IdentifyFileTypeLog += fileType_IdentifyFileTypeLog;
                fileType.GetFileType();
                if (IsImage)
                {
                    if (!dFileLoaderThumbnails.ContainsValue(PathToMedia))
                    {
                        if (FileLoader != null)
                        {
                            ThumbnailControl thumbnail = new ThumbnailControl(PathToMedia);
                            FileLoader.Add(thumbnail);
                        }
                        dFileLoaderThumbnails.Add(ThumbnailControlerCounter, PathToMedia);
                        ThumbnailControlerCounter++;
                    }
                    if (CurrentImage != PathToMedia)
                    {
                        ImageDisplay.ShowSingleMedia(PathToMedia);
                        Histogram.DoHistogram(PathToMedia);
                        Statistics.DoStatistics(PathToMedia);
                        CurrentImage = PathToMedia;
                    }
                }
            }
        }

        #region FileType Identification Events

        void fileType_IdentifyFileTypeComplete(IdentifyFileType.FileType ft)
        {
            switch (ft)
            {
                case IdentifyFileType.FileType.Unknown:
                    IsImage = false;
                    break;
                case IdentifyFileType.FileType.Image:
                    IsImage = true;
                    break;
                default:
                    IsImage = false;
                    break;
            }
        }

        void fileType_IdentifyFileTypeLog(string Message)
        {
            ConsoleWindow.Log(Message);
        }

        void fileType_IdentifyFileTypeError(string Message)
        {
            ConsoleWindow.Log("Error Identifying File: " + Message);
        }
        #endregion

        #region Histogram Events

        /// <summary>
        /// Log what the Histogram Builder is reporting to us.
        /// </summary>
        /// <param name="Message">
        /// String: Message to be sent to ConsoleWindow.Log();
        /// </param>
        void Histogram_histogramStatus(string Message)
        {
            ConsoleWindow.Log(Message);
        }

        /// <summary>
        /// Log the Completion of the Histogram being built.
        /// </summary>
        /// <param name="Message">
        /// String: Information about the build process
        /// </param>
        void Histogram_histogramCompleted(string Message)
        {
            ConsoleWindow.Log(Message);
        }



        #endregion

        #region ImageDisplay Events

        /// <summary>
        /// Information from the ImageDisplay form to be shown to the End User
        /// </summary>
        /// <param name="Message"></param>
        void ImageDisplay_ImageDisplayLog(string Message)
        {
            ConsoleWindow.Log(Message);
        }

        /// <summary>
        /// The XY Coordinates of the Mouse on the Image
        /// </summary>
        /// <param name="mouseXY"></param>
        void ImageDisplay_MediaPixelCoordinates(System.Drawing.Point mouseXY)
        {
            if (histogramMenuItem.Checked)
            {
                Histogram.SetMouseCoordinates(mouseXY);
            }
        }

        /// <summary>
        /// The colour of the Pixel under the Mouse within the image
        /// </summary>
        /// <param name="colour"></param>
        void ImageDisplay_MediaPixelColour(Color colour)
        {
            if (histogramMenuItem.Checked)
            {
                Histogram.SetPixelColour(colour);
            }
        }

        /// <summary>
        /// Notification that the ImageDisplay form has been closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImageDisplay_FormClosing(object sender, FormClosingEventArgs e)
        {
            ImageDisplay.FormClosing -= ImageDisplay_FormClosing;
            ImageDisplay.MediaFailedToLoad -= ImageDisplay_MediaFailedToLoad;
            ImageDisplay.MediaLoaded -= ImageDisplay_MediaLoaded;
        }

        /// <summary>
        /// Notification that the ImageDisplay form has loaded an Image
        /// </summary>
        /// <param name="MediaPath"></param>
        void ImageDisplay_MediaLoaded(string MediaPath)
        {
            ConsoleWindow.Log("Successfully loaded: " + MediaPath);
            ModificationTypes.EnableModifications();
            ModificationProperties.CurrentImage = CurrentImage;
        }

        /// <summary>
        /// Notification that the ImageDisplay form has failed to load an image
        /// </summary>
        /// <param name="ErrorMessage"></param>
        void ImageDisplay_MediaFailedToLoad(string ErrorMessage)
        {
            ConsoleWindow.Log("Failed to load Media " + ErrorMessage);
            ModificationTypes.DisableModifications();
            ModificationProperties.CurrentImage = CurrentImage;
        }
        #endregion

        #region Statistics Events

        /// <summary>
        /// Log messages from the Statistics Application
        /// </summary>
        /// <param name="Message">
        /// String: Message sent from Statistics
        /// </param>
        void Statistics_StatisticsLog(string Message)
        {
            ConsoleWindow.Log(Message);
        }

        #endregion

        #region FileLoader Events

        /// <summary>
        /// Notification that the USer has asked for one of the Thumbnails displayed to be shown in the ImageDisplay form
        /// </summary>
        /// <param name="PathToMedia"></param>
        void FileLoader_ThumbnailSelected(string PathToMedia)
        {
            if (CurrentImage != PathToMedia)
            {
                ImageDisplay.ShowSingleMedia(PathToMedia);
                Histogram.DoHistogram(PathToMedia);
                Statistics.DoStatistics(PathToMedia);
                CurrentImage = PathToMedia;
            }

        }
        #endregion

        #region Modification Type Events

        /// <summary>
        /// Notification that the ImageDisplay should be updated
        /// </summary>
        /// <param name="modifiedImage"></param>
        void ModificationProperties_UpdateImage(System.Drawing.Image modifiedImage)
        {
            ImageDisplay.UpdateImage(modifiedImage);
        }

        /// <summary>
        /// Notification the FAST Corner detection has been selected
        /// </summary>
        void ModificationTypes_FASTSelected()
        {
            ModificationProperties.DetectorType = "FAST";
            ModificationProperties.CurrentImage = CurrentImage;
            ModificationProperties.DoFast();
        }

        /// <summary>
        /// Notification the Moravec Corner detection has been selected
        /// </summary>
        void ModificationTypes_MoravecSelected()
        {
            ModificationProperties.DetectorType = "Moravec";
            ModificationProperties.CurrentImage = CurrentImage;
            ModificationProperties.DoMoravec();
        }

        /// <summary>
        /// Notification the Harris Corner detection has been selected
        /// </summary>
        void ModificationTypes_HarrisSelected()
        {
            ModificationProperties.DetectorType = "Harris";
            ModificationProperties.CurrentImage = CurrentImage;
            ModificationProperties.DoHarris();
        }

       /// <summary>
        /// Notification the SUSAN Corner detection has been selected
       /// </summary>
        void ModificationTypes_SusanSelected()
        {
            ModificationProperties.DetectorType = "SUSAN";
            ModificationProperties.CurrentImage = CurrentImage;
            ModificationProperties.DoSusan();
        }

        #endregion

        #region Modification Properties Events

        /// <summary>
        /// Information to be shown to the EU sent for the ModificationProperties form
        /// </summary>
        /// <param name="Message"></param>
        void ModificationProperties_ModificationPropertiesLog(string Message)
        {
            ConsoleWindow.Log(Message);
        }

        #endregion

       


    }
}
