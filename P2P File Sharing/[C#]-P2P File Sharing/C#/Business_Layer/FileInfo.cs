using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business_Layer
{
    [Serializable()]
    public class FileInfo
    {
        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="filename">It is filename value</param>
        /// <param name="size">It is file size value</param>
        public FileInfo(string filename, long size)
        {
            this.Filename = filename;
            this.Size = size;
        }

        /// <summary>
        /// It holds the filename value
        /// </summary>
        public string Filename = string.Empty;

        /// <summary>
        /// It holds the file size value
        /// </summary>
        public long Size = long.MinValue;
    }
}
