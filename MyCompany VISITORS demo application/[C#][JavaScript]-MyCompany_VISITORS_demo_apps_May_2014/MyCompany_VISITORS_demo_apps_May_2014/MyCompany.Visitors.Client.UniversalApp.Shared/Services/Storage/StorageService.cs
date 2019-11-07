namespace MyCompany.Visitors.Client.UniversalApp.Services.Storage
{
    using System;
    using System.Threading.Tasks;
    using Windows.Storage;
    using Windows.Storage.Streams;

    /// <summary>
    /// Picture Service Implementation.
    /// </summary>
    public class StorageService : IStorageService
    {
        /// <summary>
        /// Converts an StorageFile to a Byte Array.
        /// </summary>
        /// <param name="storageFile">StorageFile parameter</param>
        /// <returns>Byte Array</returns>
        public async Task<byte[]> FileToByte(StorageFile storageFile)
        {
            if(storageFile != null)
            {
                var stream = await storageFile.OpenReadAsync();

                using (var dataReader = new DataReader(stream))
                {
                    var bytes = new byte[stream.Size];
                    await dataReader.LoadAsync((uint)stream.Size);
                    dataReader.ReadBytes(bytes);

                    return bytes;
                } 
            }
            return null;
        }

        /// <summary>
        /// Converts a byte array to StorageFile.
        /// </summary>
        /// <param name="byteArray">Byte array</param>
        /// <param name="fileName">File name</param>
        /// <returns>An storaged file</returns>
        public async Task<StorageFile> ByteToFile(byte[] byteArray, string fileName)
        {
            StorageFolder folder = ApplicationData.Current.LocalFolder;
            StorageFile file = await folder.CreateFileAsync(string.Format(fileName), CreationCollisionOption.ReplaceExisting);

            using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (IOutputStream outputStream = fileStream.GetOutputStreamAt(0))
                {
                    using (DataWriter dataWriter = new DataWriter(outputStream))
                    {
                        dataWriter.WriteBytes(byteArray);
                        await dataWriter.StoreAsync();
                        dataWriter.DetachStream();
                    }

                    await outputStream.FlushAsync();
                }
            }
            return file;
        }
    }
}
