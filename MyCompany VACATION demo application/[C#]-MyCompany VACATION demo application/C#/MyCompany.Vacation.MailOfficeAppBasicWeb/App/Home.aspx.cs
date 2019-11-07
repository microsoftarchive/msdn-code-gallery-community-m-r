using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MyCompany.Vacation.MailOfficeAppBasicWeb.App
{
    public enum VacationRequestStatus
    {
        /// <summary>
        /// Unknown
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// Vacation is requested by employee
        /// </summary>
        Pending = 1,
        /// <summary>
        /// RRHH has validated the vacation, the workflow is completed!
        /// </summary>
        Approved = 2,
        /// <summary>
        /// Denied
        /// </summary>
        Denied = 3
    }

    public partial class Home : System.Web.UI.Page
    {
        private string remoteService = WebConfigurationManager.AppSettings["RemoteService"];
        private int vacationRequestId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string subject = Request.QueryString["subject"];
                if (!String.IsNullOrEmpty(subject))
                {
                    string id = subject.Substring(subject.IndexOf("[") + 1, subject.IndexOf("]") - subject.IndexOf("[") - 1);
                    if (Int32.TryParse(id, out vacationRequestId))
                    {
                        LoadRequestInfo();
                    }
                }
            }
        }


        protected async void Approve_Click(object sender, EventArgs e)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            vacationRequestId = (int)Session["VacationRequestId"];
            string url = String.Format(CultureInfo.InvariantCulture
                , "{0}api/vacationRequests/{1}/status/{2}?reason=''", remoteService, vacationRequestId, (int)VacationRequestStatus.Approved);
            var content = JsonConvert.SerializeObject(string.Empty);
            var response = await httpClient.PutAsync(url, new StringContent(content, Encoding.UTF8, "application/json"));

            MessageLbl.Text = "Approved!";
            MessageLbl.Visible = true;            
            ApproveBtn.Visible = false;
        }

        async void LoadRequestInfo()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string url = String.Format("{0}api/vacationrequests/{1}", remoteService, vacationRequestId);
            var response = await httpClient.GetStringAsync(url);

            JObject jo = JsonConvert.DeserializeObject(response) as JObject;
            if (jo != null)
            {
                this.FullNameLbl.Text = (string)jo["Employee"]["FirstName"] + " " + (string)jo["Employee"]["LastName"];
                this.StateLbl.Text = ((VacationRequestStatus)(int)jo["Status"]).ToString();
                this.FromLbl.Text = ((DateTime)jo["From"]).ToShortDateString();
                this.ToLbl.Text = ((DateTime)jo["To"]).ToShortDateString();
                this.NumDaysLbl.Text = ((int)jo["NumDays"]).ToString();
                this.CommentsLbl.Text = (string)jo["Comments"];

                bool isPending = (int)jo["Status"] == (int)VacationRequestStatus.Pending;
                if (isPending)
                {
                    Session["VacationRequestId"] = (int)jo["VacationRequestId"];
                }

                ApproveBtn.Visible = isPending;

            }
        }
    }
}