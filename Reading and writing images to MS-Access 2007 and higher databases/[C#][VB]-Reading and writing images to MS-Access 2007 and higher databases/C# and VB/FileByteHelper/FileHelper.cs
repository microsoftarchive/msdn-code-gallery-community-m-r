using System;
using System.IO;

namespace FileByteHelper
{
    public class FileHelper
    {
        private string _fileName = "";

        private FileHelper()
        {
        }

        public byte[] ImageBytes(string pFileName)
        {
            var fileStream = new FileStream(pFileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] byteArray = new byte[Convert.ToInt32(fileStream.Length - 1) + 1];
            fileStream.Read(byteArray, 0, Convert.ToInt32(fileStream.Length));
            return byteArray;
        }
    }
}