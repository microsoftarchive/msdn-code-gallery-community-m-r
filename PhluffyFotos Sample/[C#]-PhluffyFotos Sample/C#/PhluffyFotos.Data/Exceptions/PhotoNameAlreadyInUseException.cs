namespace PhluffyFotos.Data.Exceptions
{
    using System;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "Not Required")]
    public class PhotoNameAlreadyInUseException : ApplicationException
    {
        public PhotoNameAlreadyInUseException(string albumName, string photoName) :
            base("A picture with the same name already exists on this album")
        {
            this.AlbumName = albumName;
            this.PhotoName = photoName;
        }

        public string AlbumName { get; set; }

        public string PhotoName { get; set; }
    }
}
