using System;

namespace RCE.Infrastructure
{
    using System.IO;
    using System.IO.IsolatedStorage;

    using SmoothStreamingManifestGenerator;

    public static class IsolatedStoragePersistence
    {
        public static void SaveData(Stream data, string fileName)
        {
            data.Position = 0;
            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Create, isf))
                {
                    using (BinaryWriter sw = new BinaryWriter(isfs))
                    {
                        byte[] bytes = new byte[data.Length];
                        data.Read(bytes, 0, (int)data.Length);
                        sw.Write(bytes);
                    }
                }
            }
        }

        public static Stream LoadData(string fileName)
        {
            Stream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);

            using (IsolatedStorageFile isf = IsolatedStorageFile.GetUserStoreForApplication())
            {
                using (IsolatedStorageFileStream isfs = new IsolatedStorageFileStream(fileName, FileMode.Open, isf))
                {
                    isfs.Position = 0;
                    byte[] bytes = new byte[isfs.Length];
                    isfs.Read(bytes, 0, (int)isfs.Length);
                    writer.Write(bytes);
                }
            }

            return stream;
        }

        public static bool CompareStreams(Stream stream1, Stream stream2)
        {

            using (BinaryReader reader1 = new BinaryReader(stream1))
            {
                using (BinaryReader reader2 = new BinaryReader(stream2))
                {
                    var bytes1 = reader1.ReadBytes((int)stream1.Length);
                    var bytes2 = reader2.ReadBytes((int)stream2.Length);

                    for (int i = 0; i < bytes1.Length; i++)
                    {
                        if (bytes1[i] != bytes2[i])
                        {
                            return false;
                        }
                    }

                }
            }
            //using (StreamReader reader1 = new StreamReader(stream1))
            //{
            //    using (StreamReader reader2 = new StreamReader(stream2))
            //    {
            //        string s1 = reader1.ReadToEnd();
            //        string s2 = reader2.ReadToEnd();
            //        for (int i = 0; i < s1.Length; i++)
            //        {
            //            char char1 = s1[i];
            //            char char2 = s2[i];
            //            if (char1 != char2)
            //            {
            //                return false;
            //            }
            //        }
            //    }
            //}


            return true;
        }

        public static void Download()
        {
            DownloaderManager dm = new DownloaderManager();
            string url =
                "http://rcetest.southworks.net/RCE.V2/encode/CSM/ab97e0f7-8921-4158-b653-15d0eacd3a02-20110728223449.csm";

            dm.DownloadManifestCompleted += dm_DownloadManifestCompleted;

            dm.DownloadManifestAsync(new Uri(url), true, null);
        }

        public static void dm_DownloadManifestCompleted(object sender, DownloaderMangerEventArgs e)
        {
            Stream result = e.Stream;
            Stream manifest = LoadData("GeneratedStream");
            manifest.Position = 0;
            bool r = CompareStreams(result, manifest);
        }
    }
}
