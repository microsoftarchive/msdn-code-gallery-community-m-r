namespace PhluffyFotos.Data.WindowsAzure
{
    using System.Globalization;
    using Microsoft.WindowsAzure.StorageClient;

    public class TagRow : TableServiceEntity
    {
        public TagRow()
            : base()
        {
        }
        
        public TagRow(Tag tag) : 
            this(tag.Name.Substring(0, 1).ToLowerInvariant(), tag.Name)
        {
            this.Name = tag.Name;
        }

        private TagRow(string partitionkey, string rowkey) : base(partitionkey, rowkey)
        {
        }

        public string Name { get; set; }
    }
}
