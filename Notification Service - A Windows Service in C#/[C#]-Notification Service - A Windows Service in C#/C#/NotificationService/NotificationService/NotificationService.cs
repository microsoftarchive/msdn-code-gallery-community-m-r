using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.IO;
using System.Timers;
using System.Data.SqlClient;
using System.Net.Mail;

namespace BarqSoft.LearningManagement.Services
{
    public partial class NotificationService : ServiceBase
    {
        private Timer timer = new Timer();
        private double servicePollInterval;
        private int id;
        private int status;
        private string message;

        public NotificationService()
        {
            InitializeComponent();
            servicePollInterval = 2000;
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
            //providing the time in miliseconds
            timer.Interval = servicePollInterval;
            timer.AutoReset = true;
            timer.Enabled = true;
            timer.Start();
        }

        protected override void OnContinue()
        {
            base.OnContinue();
            timer.Start();
        }

        protected override void OnPause()
        {
            base.OnPause();
            timer.Stop();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            timer.Stop();
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();
            timer.Stop();
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            FileStream fs = new FileStream(@"c:\TestWindowsService.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);

            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine("Verifying for any request to process..." + DateTime.Now.ToLongTimeString());

            DataTable dt = GetMessages();
            foreach (DataRow dr in dt.Rows)
            {
                id = (int)dr[0];
                status = (int)dr[3];
                message = dr[1].ToString();
                m_streamWriter.WriteLine("Sending Email/SMS for the request id = {0} and the message is: {1}", id, message);
                SendEMail(String.Format("Re:Request#{0}", id), message);
                UpdateMessageStatus(id, 0);
            }

            m_streamWriter.Flush();
            m_streamWriter.Close();
        }

        public DataTable GetMessages(params SqlParameter[] arrParam)
        {
            DataTable dt = new DataTable();

            // Open the connection 
            using (SqlConnection cnn = new SqlConnection(@"Data Source=MyServer\GHOUSE;Initial Catalog=MyDB;User ID=sa;Password=pass@word1;"))
            {
                cnn.Open();

                // Define the command 
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spNS_GetMessages";

                    // Handle the parameters 
                    if (arrParam != null)
                    {
                        foreach (SqlParameter param in arrParam)
                            cmd.Parameters.Add(param);
                    }

                    // Define the data adapter and fill the dataset 
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
                cnn.Close();
            }
            return dt;
        }

        public DataTable UpdateMessageStatus(int id, int status)
        {
            DataTable dt = new DataTable();

            // Open the connection 
            using (SqlConnection cnn = new SqlConnection(@"Data Source=MyServer\GHOUSE;Initial Catalog=MyDB;User ID=sa;Password=pass@word1;"))
            {
                cnn.Open();

                // Define the command 
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spNS_UpdateStatus";

                    SqlParameter param = new SqlParameter("@iMessageId", id);
                    cmd.Parameters.Add(param);
                    param = new SqlParameter("@iStatus", status);
                    cmd.Parameters.Add(param);

                    cmd.ExecuteNonQuery();
                }
                cnn.Close();
            }
            return dt;
        }

        public void SendEMail(string subject, string body)
        {
            try
            {
                SmtpClient SmtpServer = new SmtpClient();
                SmtpServer.Credentials = new System.Net.NetworkCredential("ghousebarq@gmail.com", "typepasswordhere");
                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                MailMessage mail = new MailMessage();
                //String[] addr = textBox1.Text.Split(',');
                mail.From = new MailAddress("ghousebarq@gmail.com");
                Byte i;
                //for (i = 0; i < addr.Length; i++)
                mail.To.Add("ghousebarq@gmail.com");
                mail.Subject = subject;
                mail.Body = body;
                //if (listBox1.Items.Count != 0)
                //{
                //for (i = 0; i < listBox1.Items.Count; i++)
                //mail.Attachments.Add(new Attachment(listBox1.Items[i].ToString()));
                //}

                mail.IsBodyHtml = true;
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                //mail.ReplyTo = new MailAddress("ghousebarq@gmail.com");
                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message, "EMail", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
