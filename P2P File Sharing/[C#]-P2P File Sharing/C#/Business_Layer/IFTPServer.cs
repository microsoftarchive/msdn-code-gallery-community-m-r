using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Business_Layer
{
    public delegate void PostedDataHandler(string user , byte[] data);
    public delegate void UpdateHandler(string user);

    [Serializable()]
    public struct UploadData
    {
        public string Filename;
        public byte[] File;
    }

    public interface IFTPServer
    {
        void Upload(string user,List<UploadData> files);
        void Download(string user,string filename, out byte[] file);
        void GetFiles(out List<FileInfo> files);
        void Connect(string user);
        void Disconnect(string user);
        void PostData(string user, byte[] data);
        event PostedDataHandler PostedData;
        event UpdateHandler Update;

    }

    public abstract class PostedData : System.MarshalByRefObject 
    {
        public void Server_PostData(string user, byte[] data)
        {
            this.ImplementedPostData(user, data);
        }

        public void Server_Update(string user)
        {
            this.ImplementedUpdate(user);
        }

        public abstract void ImplementedPostData(string user, byte[] data);

        public abstract void ImplementedUpdate(string user);
    }
}
