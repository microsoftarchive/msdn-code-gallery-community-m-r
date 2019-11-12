using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.IO;
using System.Collections;
using Aspose.Words;
using System.Diagnostics;
using System.Data;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Xml.Linq;

namespace ResumeParser
{
    public partial class Default : System.Web.UI.Page
    {

        public static string name = "";
        public static string email = "";
        public static string phone = "";
        public static string skills = "";
        public static string summary = "";
        public static string experience = "";
        public static string education = "";
        public static string interests = "";
        public static string languages = "";
        public static int rowIndex = -1;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Markers"] == null)
                {
                    ArrayList list = new ArrayList();
                    if (File.Exists(Server.MapPath("Content/Project.xml")))
                    {

                        XDocument doc = XDocument.Load(Server.MapPath("Content/Project.xml"));

                        var data = doc.Descendants();

                        List<System.Xml.Linq.XElement> element = data.ToList();
                                              
                            list.Add(element[1].Value);
                            list.Add(element[2].Value);
                            list.Add(element[3].Value);
                            list.Add(element[4].Value);
                            list.Add(element[5].Value);
                            list.Add(element[6].Value);
                            list.Add(element[7].Value);
                        
                    }
                    else
                    {
                       
                        list.Add("summary");
                        list.Add("specialties");
                        list.Add("skills");
                        list.Add("experience");
                        list.Add("education");
                        list.Add("interests");
                        list.Add("languages");

                    }
                        Session["Markers"] = list;

                        System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Server.MapPath("Input/"));

                        foreach (FileInfo file in downloadedMessageInfo.GetFiles())
                        {
                            file.Delete();
                        }
                    

                }
            }
        }

        

        protected void File_Upload(object sender, AjaxFileUploadEventArgs e)
        {

           

            string filename = e.FileName;

            string strDestPath = Server.MapPath("Input/");

            AjaxFileUpload1.SaveAs(@strDestPath + filename);

        }


        protected void btnParse_Click(object sender, EventArgs e)
        {
            Parser parser = new Parser();
            parser.ParseData();

            GridView1.DataSource = (DataTable)Session["DataTable"];
            GridView1.DataBind();
        }


        protected void Button5_Click(object sender, EventArgs e)
        {

            DataTable dt = (DataTable)Session["DataTable"];

            dt.Rows[rowIndex]["Name"] = txtDName.Text;
            dt.Rows[rowIndex]["Email"] = txtDEmail.Text;
            dt.Rows[rowIndex]["Phone"] = txtDPhone.Text;
            dt.Rows[rowIndex]["Skills"] = txtDSkills.Text;
            dt.Rows[rowIndex]["Summary"] = txtDSummary.Text;
            dt.Rows[rowIndex]["Experience"] = txtDExperience.Text;
            dt.Rows[rowIndex]["Education"] = txtDEducation.Text;
            dt.Rows[rowIndex]["Interests"] = txtDInterests.Text;
            dt.Rows[rowIndex]["Languages"] = txtDLanguages.Text;

            Session["DataTable"] = dt;

            GridView1.DataSource = dt;
            GridView1.DataBind();

        }
        protected void imgbtn_Click(object sender, EventArgs e)
        {
            
            rowIndex = -1;
            Button btndetails = sender as Button;
            GridViewRow gvrow = (GridViewRow)btndetails.NamingContainer;
            
            DataTable dt = (DataTable)Session["DataTable"];

            txtDName.Text = dt.Rows[gvrow.RowIndex]["Name"].ToString();
            txtDEmail.Text = dt.Rows[gvrow.RowIndex]["Email"].ToString();
            txtDPhone.Text = dt.Rows[gvrow.RowIndex]["Phone"].ToString();
            txtDSkills.Text = dt.Rows[gvrow.RowIndex]["Skills"].ToString();
            txtDSummary.Text = dt.Rows[gvrow.RowIndex]["Summary"].ToString(); 
            txtDExperience.Text = dt.Rows[gvrow.RowIndex]["Experience"].ToString(); 
            txtDEducation.Text = dt.Rows[gvrow.RowIndex]["Education"].ToString(); 
            txtDInterests.Text = dt.Rows[gvrow.RowIndex]["Interests"].ToString(); 
            txtDLanguages.Text = dt.Rows[gvrow.RowIndex]["Languages"].ToString();

            rowIndex = gvrow.RowIndex;

            AjaxControlToolkit.ModalPopupExtender mpe = (AjaxControlToolkit.ModalPopupExtender)gvrow.FindControl("ModalPopupExtender4");
            mpe.Show();
        }
      
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ViewState["OrigData1"] = e.Row.Cells[3].Text;
                ViewState["OrigData2"] = e.Row.Cells[4].Text;
                ViewState["OrigData3"] = e.Row.Cells[5].Text;
                ViewState["OrigData4"] = e.Row.Cells[6].Text;
                if (e.Row.Cells[3].Text.Length >= 35) //Just change the value of 20 based on your requirements
                {
                    e.Row.Cells[3].Text = e.Row.Cells[3].Text.Substring(0, 35) + "...";
                    e.Row.Cells[3].ToolTip = ViewState["OrigData1"].ToString();
                }
                if (e.Row.Cells[4].Text.Length >= 35) //Just change the value of 20 based on your requirements
                {
                    e.Row.Cells[4].Text = e.Row.Cells[4].Text.Substring(0, 35) + "...";
                    e.Row.Cells[4].ToolTip = ViewState["OrigData2"].ToString();
                }

                if (e.Row.Cells[5].Text.Length >= 35) //Just change the value of 20 based on your requirements
                {
                    e.Row.Cells[5].Text = e.Row.Cells[5].Text.Substring(0, 35) + "...";
                    e.Row.Cells[5].ToolTip = ViewState["OrigData3"].ToString();
                }

                if (e.Row.Cells[6].Text.Length >= 35) //Just change the value of 20 based on your requirements
                {
                    e.Row.Cells[6].Text = e.Row.Cells[6].Text.Substring(0, 35) + "...";
                    e.Row.Cells[6].ToolTip = ViewState["OrigData4"].ToString();
                }

            }

        } 

      
    }
}