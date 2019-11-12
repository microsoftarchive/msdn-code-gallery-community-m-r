using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FileSharing_FTP_Server
{

    public class FTPServer : System.MarshalByRefObject , Business_Layer.IFTPServer 
    {
        public static System.Windows.Forms.TextBox Logger = null;

        public void AddLog(string text)
        {
            if (Logger.InvokeRequired)
            {
                Action<string> invoker = new Action<string>(AddLog);
                Logger.Invoke(invoker, text);
            }
            else
            {
                Logger.Text +=Environment.NewLine + text;
            }
        }

        public void Connect(string user)
        {
            if (Logger != null)
            {
                if (Logger.InvokeRequired)
                {
                    Action<string> invoker = new Action<string>(Connect);
                    Logger.Invoke(invoker,user);
                }
                else
                {
                    Logger.Text += string.Format("{0}>{1} is connected at [{2}].", Environment.NewLine, user, DateTime.Now.ToShortTimeString());
                }
            }
        }
        public void Disconnect(string user)
        {
            if (Logger != null)
            {
                if (Logger.InvokeRequired)
                {
                    Action<string> invoker = new Action<string>(Disconnect);
                    Logger.Invoke(invoker,user);
                }
                else
                {
                    Logger.Text += string.Format("{0}>{1} is Disconnected at [{2}].", Environment.NewLine, user, DateTime.Now.ToShortTimeString());
                }
            }
        }

        public void PostData(string user, byte[] data)
        {
            if (PostedData != null)
                PostedData(user, data);
        }

        public event Business_Layer.PostedDataHandler PostedData;

        public void Upload(string user,List<Business_Layer.UploadData> files)
        {
            if (!System.IO.Directory.Exists("Share"))
                System.IO.Directory.CreateDirectory("Share");

            foreach (Business_Layer.UploadData file in files)
            {
                System.IO.File.WriteAllBytes("Share\\" + file.Filename, file.File);
                AddLog(string.Format("> File: {0} has been uploaded at {1}. by {2}",file.Filename,DateTime.Now.ToShortTimeString(),user));
            }

            if (Update != null)
                Update(user);

        }

        public void Download(string user,string filename, out byte[] file)
        {
            file = new byte[1];

            if (!System.IO.Directory.Exists("Share"))
                System.IO.Directory.CreateDirectory("Share");

            foreach (string the in System.IO.Directory.GetFiles("Share")) 
            {
                if(the.Contains(filename))
                if (System.IO.File.Exists(the))
                {
                    file = System.IO.File.ReadAllBytes(the);
                    AddLog(string.Format("> File: {0} has been downloaded at {1}. by {2}",(new System.IO.FileInfo(the)).Name,DateTime.Now.ToShortTimeString(),user));
                    break;
                }
            }

            if (file.Length == 1)
                file = null;
        }

        public void GetFiles(out List<Business_Layer.FileInfo> files)
        {

            if (!System.IO.Directory.Exists("Share"))
                System.IO.Directory.CreateDirectory("Share");
            
            List<Business_Layer.FileInfo > list = new List<Business_Layer.FileInfo>();

            foreach (string file in System.IO.Directory.GetFiles("Share"))
            {
                list.Add(new Business_Layer.FileInfo(file, ((new System.IO.FileInfo(file).Length) / 1024)));
            }

            files = list;
            
        }


        public event Business_Layer.UpdateHandler Update;

    }
}
