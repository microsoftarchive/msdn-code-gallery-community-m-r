using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using System.IO;
using System.Collections;
using System.Xml;

namespace ResumeParser
{
    public partial class Settings : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ArrayList list = (ArrayList)Session["Markers"];

                txtSummary.Text = list[0].ToString();
                txtSpecialties.Text = list[1].ToString();
                txtSkills.Text = list[2].ToString();
                txtExperience.Text = list[3].ToString();
                txtEducation.Text = list[4].ToString();
                txtInterest.Text = list[5].ToString();
                txtLanguage.Text = list[6].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            ArrayList list = new ArrayList();

            list.Add(txtSummary.Text);
            list.Add(txtSpecialties.Text);
            list.Add(txtSkills.Text);
            list.Add(txtExperience.Text);
            list.Add(txtEducation.Text);
            list.Add(txtInterest.Text);
            list.Add(txtLanguage.Text);

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter writer = XmlWriter.Create(Server.MapPath("Content/Project.xml"), settings);
            writer.WriteStartDocument();
            writer.WriteStartElement("Data");
            writer.WriteElementString("Summary", txtSummary.Text);
            writer.WriteElementString("Specialties", txtSpecialties.Text);
            writer.WriteElementString("Skills", txtSkills.Text);
            writer.WriteElementString("Experience", txtExperience.Text);
            writer.WriteElementString("Education", txtEducation.Text);
            writer.WriteElementString("Interests", txtInterest.Text);
            writer.WriteElementString("Language", txtLanguage.Text);
            writer.WriteEndElement();
            writer.WriteEndDocument();

            writer.Flush();
            writer.Close();

            Session["Markers"] = list;
        }

    }
}