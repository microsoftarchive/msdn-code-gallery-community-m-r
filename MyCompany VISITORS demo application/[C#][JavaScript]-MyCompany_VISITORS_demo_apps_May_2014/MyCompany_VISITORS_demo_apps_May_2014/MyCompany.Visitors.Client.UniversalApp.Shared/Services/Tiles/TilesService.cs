namespace MyCompany.Visitors.Client.UniversalApp.Services.Tiles
{
    using MyCompany.Visitors.Client.UniversalApp.Model;
    using MyCompany.Visitors.Client.UniversalApp.Services.Storage;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Windows.Data.Xml.Dom;
    using Windows.UI.Notifications;
    using System.Linq;
    using Windows.UI.StartScreen;

    /// <summary>
    /// Tiles service implementation.
    /// </summary>
    public class TilesService : ITilesService
    {
        private const string NOIMAGE_PATH = "/Assets/no_photo_big.png";
        private const string IMAGE_FORMAT = ".jpg";
        private const string IMAGE_FILENAME = "secondaryTile_";
        private const string IMAGE_FOLDER = "ms-appdata:///local/";

        private TileUpdater tileUpdater;
        private readonly IStorageService storageService;

        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="storageService">Storage service</param>
        public TilesService(IStorageService storageService)
        {
            this.storageService = storageService;
        }

        /// <summary>
        /// Update the main tile with next visits.
        /// </summary>
        /// <param name="visits">Visits collection.</param>
        public async void UpdateMainTile(IList<Visit> visits)
        {
            if (visits.Any())
            {
                tileUpdater = TileUpdateManager.CreateTileUpdaterForApplication();
                tileUpdater.Clear();
                tileUpdater.EnableNotificationQueue(true);
                var i = 0;

                foreach (var visit in visits)
                {
                    var visitItem = new VisitItem(visit, 0);
                    
                    await CreateTile(visitItem,string.Format("mainTile_{0}.jpg", i.ToString()));
                    i++;
                }
            }
        }

        /// <summary>
        /// Update the pin tile of a visit.
        /// </summary>
        /// <param name="visitItem">Visit item</param>
        /// <param name="tile">Tile</param>
        public async void UpdatePinTile(VisitItem visitItem, SecondaryTile tile)
        {
            try
            {
                tileUpdater = TileUpdateManager.CreateTileUpdaterForSecondaryTile(tile.TileId);
                tileUpdater.Clear();
                tileUpdater.EnableNotificationQueue(true);

                await CreateTile(visitItem, IMAGE_FILENAME + tile.TileId + IMAGE_FORMAT);
                CreateImageTile(visitItem, IMAGE_FILENAME + tile.TileId + IMAGE_FORMAT);
            }
            catch(Exception)
            {
            }
        }

        private void CreateImageTile(VisitItem visitItem, string fileName)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Image);
            string path;

            if (visitItem.VisitorPhotoBytes != null)
            {
                path = IMAGE_FOLDER + fileName;
            }
            else
            {
                path = NOIMAGE_PATH;
            }

            var picture = (XmlElement)tileXml.GetElementsByTagName("image")[0];
            picture.SetAttribute("src", path);

            var tileNotification = new TileNotification(tileXml);

            tileUpdater.Update(tileNotification);
        }

        private async Task CreateTile(VisitItem visitItem, string fileName)
        {
            XmlDocument tileXml = await CreateWideTemplate(visitItem, fileName);

            XmlDocument secondTileXml = CreateSquareTemplate(visitItem);

            IXmlNode node = tileXml.ImportNode(secondTileXml.GetElementsByTagName("binding").Item(0), true);

            tileXml.GetElementsByTagName("visual").Item(0).AppendChild(node);

            var tileNotification = new TileNotification(tileXml);
            this.tileUpdater.Update(tileNotification);
        }

        private async Task<XmlDocument> CreateWideTemplate(VisitItem visitItem, string fileName)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText02);
            
            // Visitor Name
            var nameText = (XmlElement)tileXml.GetElementsByTagName("text")[0];
            nameText.AppendChild(tileXml.CreateTextNode(visitItem.VisitorName));

            // Visitor Company
            var companyText = (XmlElement)tileXml.GetElementsByTagName("text")[1];
            companyText.AppendChild(tileXml.CreateTextNode(visitItem.CompanyName));

            // Visitor Picture
            string path;

            if ((visitItem.VisitorSmallPhotoBytes != null))
            {
                var pictureStoraged = await this.storageService.ByteToFile(visitItem.VisitorSmallPhotoBytes, fileName);
                path = IMAGE_FOLDER + pictureStoraged.Name;
            }
            else
            {
                path = NOIMAGE_PATH;
            }

            var picture = (XmlElement)tileXml.GetElementsByTagName("image")[0];
            picture.SetAttribute("src", path);

            // Visit Time (local)
            var dateText = (XmlElement)tileXml.GetElementsByTagName("text")[2];
            var formattedDate = visitItem.VisitFormattedDate;
            dateText.AppendChild(tileXml.CreateTextNode(formattedDate));

            // Visit Hour (local)
            var hourText = (XmlElement)tileXml.GetElementsByTagName("text")[3];
            var formattedTime = visitItem.VisitFormattedTime;
            hourText.AppendChild(tileXml.CreateTextNode(formattedTime));

            return tileXml;
        }

        private XmlDocument CreateSquareTemplate(VisitItem visitItem)
        {
            XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquare150x150Text03);

            // Visitor name
            var nameText2 = (XmlElement)tileXml.GetElementsByTagName("text")[0];
            var name = visitItem.VisitorName;
            nameText2.AppendChild(tileXml.CreateTextNode(name));

            // Visitor's company
            var companyText2 = (XmlElement)tileXml.GetElementsByTagName("text")[1];
            var company = visitItem.CompanyName;
            companyText2.AppendChild(tileXml.CreateTextNode(company));

            // Date
            var dateText2 = (XmlElement)tileXml.GetElementsByTagName("text")[2];
            var date = visitItem.VisitFormattedDate;
            dateText2.AppendChild(tileXml.CreateTextNode(date));

            // Hour
            var hourText2 = (XmlElement)tileXml.GetElementsByTagName("text")[3];
            var hour = visitItem.VisitFormattedTime;
            hourText2.AppendChild(tileXml.CreateTextNode(hour));

            return tileXml;
        }
    }
}
