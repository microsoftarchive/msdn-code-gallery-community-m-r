namespace MyCompany.Visitors.Client.UniversalApp.Services.Storage
{
    using System.Threading.Tasks;
    using Windows.Storage;

    /// <summary>
    /// Picture Service Contract.
    /// </summary>
    public interface IStorageService
    {
        /// <summary>
        /// Converts an StorageFile to a Byte Array.
        /// </summary>
        /// <param name="storageFile">StorageFile parameter</param>
        /// <returns>Byte Array</returns>
        Task<byte[]> FileToByte(StorageFile storageFile);

        /// <summary>
        /// Converts a byte array to StorageFile.
        /// </summary>
        /// <param name="byteArray">Byte array.</param>
        /// /// <param name="fileName">File name.</param>
        /// <returns>An storaged file.</returns>
        Task<StorageFile> ByteToFile(byte[] byteArray, string fileName);
    }
}
