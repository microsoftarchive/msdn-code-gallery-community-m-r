//// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
//// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
//// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//// PARTICULAR PURPOSE.
////
//// Copyright (c) Microsoft Corporation. All rights reserved

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;
using Windows.Web.Http.Filters;
using Windows.Data.Json;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using PlayReadyUAP.Data;

// The data model defined by this file serves as a representative example of a strongly-typed
// model.  The property names chosen coincide with data bindings in the standard item templates.
//
// Applications may use this model as a starting point and build on it, or discard it entirely and
// replace it with something appropriate to their needs. If using this model, you might improve app 
// responsiveness by initiating the data loading task in the code behind for App.xaml when the app 
// is first launched.

namespace PlayReadyUAP.Data
{
    public class ContentProperty
    {
        public ContentProperty(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }
        public string Value { get; private set; }
    }

    /// <summary>
    /// Generic item data model.
    /// </summary>
    public class SampleDataItem
    {
        public SampleDataItem(String Comment,
                              String uniqueId,
                              String title,
                              String category,
                              String imagePath,
                              String description,
                              String content,
                              String encryptionAlgorithm,
                              String laurl,
                              String kid,
                              String uplinkKey,
                              String contentKey,
                              String DomainUrl,
                              String serviceId,
                              String domainId,
                              String customData,
                              String firstPlayExpiration,
                              String meteringCertFile,
                              String secureStopCertFile,
                              String bitRateMin,        // adaptive only
                              String bitRateMax,        // adaptive only
                              String resolutionMin,     // adaptive only
                              String resolutionMax,     // adaptive only
                              String keyRotation,
                              String avSep,
                              String deliveryMethod,    // Dash, Progressive, Smooth
                              String audioCodec,
                              String videoCodec,
                              String isAudioEncrypted,
                              String slicesPerFrame)
        {
            this.Comment = Comment;
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Category = category;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Content = content;
            this.EncryptionAlgorithm = encryptionAlgorithm;
            this.Laurl = laurl;
            this.Kid = kid;
            this.UplinkKey = uplinkKey;
            this.ContentKey = contentKey;
            this.DomainUrl = DomainUrl;
            this.ServiceId = serviceId;
            this.DomainId = domainId;
            this.CustomData = customData;
            this.FirstPlayExpiration = firstPlayExpiration;
            this.MeteringCertFile = meteringCertFile;
            this.SecureStopCertFile = secureStopCertFile;
            this.CombinedInfo = this.Comment + " " + this.UniqueId;

            this.BitRateMin = bitRateMin;
            this.BitRateMax = bitRateMax;
            this.ResolutionMin = resolutionMin;
            this.ResolutionMax = resolutionMax;
            this.KeyRotation = keyRotation;
            this.AVSep = avSep;
            this.DeliveryMethod = deliveryMethod;
            this.AudioCodec = audioCodec;
            this.VideoCodec = videoCodec;
            this.IsAudioEncrypted = isAudioEncrypted;
            this.SlicesPerFrame = slicesPerFrame;

            this.Properties = new List<ContentProperty>();
            this.Properties.Add(new ContentProperty("Unique Id: ", UniqueId));
            this.Properties.Add(new ContentProperty("Title: ", Title));
            this.Properties.Add(new ContentProperty("Category: ", Category));
            this.Properties.Add(new ContentProperty("Description: ", Description));
            if (EncryptionAlgorithm != "null")
            {
                this.Properties.Add(new ContentProperty("Encryption Algorithm: ", EncryptionAlgorithm));
            }
            if (Laurl != "null")
            {
                this.Properties.Add(new ContentProperty("License Acquisition Url: ", Laurl));
            }
            if (Kid != "null")
            {
                this.Properties.Add(new ContentProperty("Key Id: ", Kid));
            }
            if (UplinkKey != "null")
            {
                this.Properties.Add(new ContentProperty("Uplink Key: ", UplinkKey));
            }
            if (ContentKey != "null")
            {
                this.Properties.Add(new ContentProperty("ContentKey: ", ContentKey));
            }
            if (DomainUrl != "null")
            {
                this.Properties.Add(new ContentProperty("Domain Url: ", DomainUrl));
            }
            if (ServiceId != "null")
            {
                this.Properties.Add(new ContentProperty("Service Id: ", ServiceId));
            }
            if (CustomData != "null")
            {
                this.Properties.Add(new ContentProperty("Custom Data: ", CustomData));
            }
            if (BitRateMin != "null")
            {
                this.Properties.Add(new ContentProperty("Min. Bit Rate: ", BitRateMin));
            }
            if (BitRateMax != "null")
            {
                this.Properties.Add(new ContentProperty("Max. Bit Rate: ", BitRateMax));
            }
            if (ResolutionMin != "null")
            {
                this.Properties.Add(new ContentProperty("Min Resolution: ", ResolutionMin));
            }
            if (ResolutionMax != "null")
            {
                this.Properties.Add(new ContentProperty("Max Resolution: ", ResolutionMax));
            }
            if (KeyRotation != "null")
            {
                this.Properties.Add(new ContentProperty("Key Rotation: ", KeyRotation));
            }
            if (AVSep != "null")
            {
                this.Properties.Add(new ContentProperty("AV Separation: ", AVSep));
            }
            if (DeliveryMethod != "null")
            {
                this.Properties.Add(new ContentProperty("Delivery Method: ", DeliveryMethod));
            }
            if (AudioCodec != "null")
            {
                this.Properties.Add(new ContentProperty("Audio Codec: ", AudioCodec));
            }
            if (VideoCodec != "null")
            {
                this.Properties.Add(new ContentProperty("Video Codec: ", VideoCodec));
            }
            if (IsAudioEncrypted != "null")
            {
                this.Properties.Add(new ContentProperty("Audio Encrypted: ", IsAudioEncrypted));
            }
            if (SlicesPerFrame != "null")
            {
                this.Properties.Add(new ContentProperty("Slices Per Frame: ", SlicesPerFrame));
            }
            if (Comment != "null")
            {
                this.Properties.Add(new ContentProperty("Comment: ", Comment));
            }
        }

        public string Comment { get; private set; }
        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string Content { get; private set; }
        public string EncryptionAlgorithm { get; private set; }
        public string Laurl { get; private set; }
        // Include the license rights/restrictions for the directtaps server
        public string LaurlWithRights { get; set; }
        public string Kid { get; private set; }
        public string UplinkKey { get; private set; }
        public string ContentKey { get; private set; }
        public string DomainUrl { get; private set; }
        public string ServiceId { get; private set; }
        public string DomainId { get; private set; }
        public string CustomData { get; private set; }
        public string FirstPlayExpiration { get; private set; }
        public string MeteringCertFile { get; private set; }
        public string SecureStopCertFile { get; private set; }
        public string BitRateMin { get; private set; }
        public string BitRateMax { get; private set; }
        public string ResolutionMin { get; private set; }
        public string ResolutionMax { get; private set; }
        public string KeyRotation { get; private set; }
        public string AVSep { get; private set; }
        public string DeliveryMethod { get; private set; }
        public string AudioCodec { get; private set; }
        public string VideoCodec { get; private set; }
        public string IsAudioEncrypted { get; private set; }
        public string SlicesPerFrame { get; private set; }
        public override string ToString()
        {
            return this.Title;
        }
        public string CombinedInfo { get; private set; }

        public List<ContentProperty> Properties { get; private set; }

        // Specify the type of license to acquire next in order to play this item.
        public bool IsRootLicense { get; set; }

        // Specify whether the license to play this item is a LDL (limited duration license)
        public bool IsLDL { get; set; }

        // Specify the index of the player that is used to play this item
        public uint PlayerIndex { get; set; } 

        // Use as the content of a tooltip
        public string Details
        {
            get
            {
                string details = "Details:\n\n";
                foreach(ContentProperty item in Properties)
                {
                    details += item.Name + item.Value + "\n";
                }

                return details;
            }
        }
    }

    /// <summary>
    /// Generic group data model.
    /// </summary>
    public class SampleDataGroup
    {
        public SampleDataGroup(String uniqueId, 
                               String title, 
                               String category, 
                               String imagePath, 
                               String description)
        {
            this.UniqueId = uniqueId;
            this.Title = title;
            this.Category = category;
            this.Description = description;
            this.ImagePath = imagePath;
            this.Items = new ObservableCollection<SampleDataItem>();
        }

        public string UniqueId { get; private set; }
        public string Title { get; private set; }
        public string Category { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public ObservableCollection<SampleDataItem> Items { get; private set; }

        public override string ToString()
        {
            return this.Title;
        }
    }

    /// <summary>
    /// Creates a collection of groups and items with content read from a static json file.
    /// 
    /// SampleDataSource initializes with data read from a static json file included in the 
    /// project.  This provides sample data at both design-time and run-time.
    /// </summary>
    public sealed class SampleDataSource
    {
        public string SampleDataPath { get; private set; }

        private static SampleDataSource _sampleDataSource = new SampleDataSource();

        private ObservableCollection<SampleDataGroup> _groups = new ObservableCollection<SampleDataGroup>();
        private ObservableCollection<SampleDataItem> _dataItems = new ObservableCollection<SampleDataItem>();
        public ObservableCollection<SampleDataGroup> Groups
        {
            get { return this._groups; }
        }

        public ObservableCollection<SampleDataItem> DataItems
        {
            get { return this._dataItems; }
        }

        public static async Task<IEnumerable<SampleDataGroup>> GetGroupsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.Groups;
        }

        public static async Task<SampleDataGroup> GetGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataGroup> GetOneOrTwoFromGroupAsync(string uniqueId)
        {
            await _sampleDataSource.GetOneOrTwoSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.Where((group) => group.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<SampleDataItem> GetItemAsync(string uniqueId)
        {
            await _sampleDataSource.GetSampleDataAsync();
            // Simple linear search is acceptable for small data sets
            var matches = _sampleDataSource.Groups.SelectMany(group => group.Items).Where((item) => item.UniqueId.Equals(uniqueId));
            if (matches.Count() == 1) return matches.First();
            return null;
        }

        public static async Task<IEnumerable<SampleDataItem>> GetItemsAsync()
        {
            await _sampleDataSource.GetSampleDataAsync();

            return _sampleDataSource.DataItems;
        }

        private async Task GetSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            string jsonText = string.Empty;

            // Read data from embedded json data file.
            if (string.IsNullOrEmpty(jsonText))
            {
                Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

                StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
                jsonText = await FileIO.ReadTextAsync(file);
                SampleDataPath = "./DataModel/SampleData.json";
            }
                
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();
        
            foreach (JsonValue groupValue in jsonArray)
            {
                JsonObject groupObject = groupValue.GetObject();
                SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                            groupObject["Title"].GetString(),
                                                            groupObject["Category"].GetString(),
                                                            groupObject["ImagePath"].GetString(),
                                                            groupObject["Description"].GetString());

                foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                {
                    JsonObject itemObject = itemValue.GetObject();
                    var item = new SampleDataItem(itemObject["Comment"].GetString(),
                                                       itemObject["UniqueId"].GetString(),
                                                       itemObject["Title"].GetString(),
                                                       itemObject["Category"].GetString(),
                                                       itemObject["ImagePath"].GetString(),
                                                       itemObject["Description"].GetString(),
                                                       itemObject["Content"].GetString(),
                                                       itemObject["EncryptionAlgorithm"].GetString(),
                                                       itemObject["Laurl"].GetString(),
                                                       itemObject["Kid"].GetString(),
                                                       itemObject["UplinkKey"].GetString(),
                                                       itemObject["ContentKey"].GetString(),
                                                       itemObject["DomainUrl"].GetString(),
                                                       itemObject["ServiceId"].GetString(),
                                                       itemObject["DomainId"].GetString(),
                                                       itemObject["CustomData"].GetString(),
                                                       itemObject["FirstPlayExpiration"].GetString(),
                                                       itemObject["MeteringCertFile"].GetString(),
                                                       itemObject["SecureStopCertFile"].GetString(),
                                                       itemObject["BitRateMin"].GetString(),
                                                       itemObject["BitRateMax"].GetString(),
                                                       itemObject["ResolutionMin"].GetString(),
                                                       itemObject["ResolutionMax"].GetString(),
                                                       itemObject["KeyRotation"].GetString(),
                                                       itemObject["AVSep"].GetString(),
                                                       itemObject["DeliveryMethod"].GetString(),
                                                       itemObject["AudioCodec"].GetString(),
                                                       itemObject["VideoCodec"].GetString(),
                                                       itemObject["IsAudioEncrypted"].GetString(),
                                                       itemObject["SlicesPerFrame"].GetString());
                    group.Items.Add(item);
                    this._dataItems.Add(item);
                }
                this.Groups.Add(group);
            }
        }

        private async Task GetOneOrTwoSampleDataAsync()
        {
            if (this._groups.Count != 0)
                return;

            Uri dataUri = new Uri("ms-appx:///DataModel/SampleData.json");

            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(dataUri);
            string jsonText = await FileIO.ReadTextAsync(file);
            JsonObject jsonObject = JsonObject.Parse(jsonText);
            JsonArray jsonArray = jsonObject["Groups"].GetArray();

            foreach (JsonValue groupValue in jsonArray)
            {

                JsonObject groupObject = groupValue.GetObject();
                SampleDataGroup group = new SampleDataGroup(groupObject["UniqueId"].GetString(),
                                                            groupObject["Title"].GetString(),
                                                            groupObject["Category"].GetString(),
                                                            groupObject["ImagePath"].GetString(),
                                                            groupObject["Description"].GetString());
                int itemCount = 0;

                foreach (JsonValue itemValue in groupObject["Items"].GetArray())
                {
                    itemCount++;
                    if (itemCount > 2)
                    {
                        break;
                    }

                    JsonObject itemObject = itemValue.GetObject();
                    group.Items.Add(new SampleDataItem(itemObject["Comment"].GetString(),
                                                        itemObject["UniqueId"].GetString(),
                                                        itemObject["Title"].GetString(),
                                                        itemObject["Category"].GetString(),
                                                        itemObject["ImagePath"].GetString(),
                                                        itemObject["Description"].GetString(),
                                                        itemObject["Content"].GetString(),
                                                        itemObject["EncryptionAlgorithm"].GetString(),
                                                        itemObject["Laurl"].GetString(),
                                                        itemObject["Kid"].GetString(),
                                                        itemObject["UplinkKey"].GetString(),
                                                        itemObject["ContentKey"].GetString(),
                                                        itemObject["DomainUrl"].GetString(),
                                                        itemObject["ServiceId"].GetString(),
                                                        itemObject["DomainId"].GetString(),
                                                        itemObject["CustomData"].GetString(),
                                                        itemObject["FirstPlayExpiration"].GetString(),
                                                        itemObject["MeteringCertFile"].GetString(),
                                                        itemObject["SecureStopCertFile"].GetString(),
                                                        itemObject["BitRateMin"].GetString(),
                                                        itemObject["BitRateMax"].GetString(),
                                                        itemObject["ResolutionMin"].GetString(),
                                                        itemObject["ResolutionMax"].GetString(),
                                                        itemObject["KeyRotation"].GetString(),
                                                        itemObject["AVSep"].GetString(),
                                                        itemObject["DeliveryMethod"].GetString(),
                                                        itemObject["AudioCodec"].GetString(),
                                                        itemObject["VideoCodec"].GetString(),
                                                        itemObject["IsAudioEncrypted"].GetString(),
                                                        itemObject["SlicesPerFrame"].GetString()));

                }
                this.Groups.Add(group);
            }
        }
    }
}