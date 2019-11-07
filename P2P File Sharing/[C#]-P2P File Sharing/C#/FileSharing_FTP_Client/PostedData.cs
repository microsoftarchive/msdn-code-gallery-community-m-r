using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business_Layer;
using System.Net;
using System.Windows.Forms;

namespace FileSharing_FTP_Client
{
    public class PostedData : Business_Layer.PostedData 
    {
        public event EventHandler RefreshList;  

        public override void ImplementedPostData(string user, byte[] data)
        {

            if (!user.Equals(MachineInfo.GetJustIP()))
                return;

        }

        public override void ImplementedUpdate(string user)
        {
            if (user.Equals(MachineInfo.GetJustIP()))
                return;

            if (RefreshList != null)
                RefreshList(this, null);

            
        }
    }
}
