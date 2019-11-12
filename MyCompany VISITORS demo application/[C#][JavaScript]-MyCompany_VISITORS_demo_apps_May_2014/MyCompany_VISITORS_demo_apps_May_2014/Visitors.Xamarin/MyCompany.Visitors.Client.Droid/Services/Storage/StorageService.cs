
namespace MyCompany.Visitors.Client.WP.Services.Storage
{
    using System.IO.IsolatedStorage;
    using Newtonsoft.Json;
    using System.IO;

    /// <summary>
    /// Storage contract implementation
    /// </summary>
    public class StorageService : IStorageService
    {
        /// <summary>
        /// Load personal information from isolated storage.
        /// </summary>
        /// <returns></returns>
        public Visitor LoadPersonalInformation()
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (file.FileExists("user.json"))
                {
                    using (IsolatedStorageFileStream fs = file.OpenFile("user.json", System.IO.FileMode.Open))
                    {
                        StreamReader reader = new StreamReader(fs);
                        string json = reader.ReadToEnd();
                        Visitor pInformation = JsonConvert.DeserializeObject<Visitor>(json);
                        return pInformation;
                    }
                }

                return null;
            }
        }

        /// <summary>
        /// Save personal information to isolated storage.
        /// </summary>
        /// <param name="pInformation">User defined personal information</param>
        /// <returns></returns>
        public void SavePersonalInformation(Visitor pInformation)
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (file.FileExists("user.json"))
                    file.DeleteFile("user.json");

                using (IsolatedStorageFileStream fs = file.CreateFile("user.json"))
                {
                    StreamWriter writer = new StreamWriter(fs);
                    string json = JsonConvert.SerializeObject(pInformation);
                    writer.Write(json);
                    writer.Flush();
                }
            }
        }

        /// <summary>
        /// Known if the user had saved information.
        /// </summary>
        /// <returns></returns>
        public bool ExistInformation()
        {
            using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (file.FileExists("user.json"))
                    return true;

                return false;
            }
        }
    }
}
