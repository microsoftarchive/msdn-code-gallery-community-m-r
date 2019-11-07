using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace RESTbookProvider
{
    public class Library
    {
        
        //---------------------------------------------------------------------
        //              Uri 	    Method 	                                
        //              ---         -----------------------------------
        //              POST 	    http://localhost:30523/RESTbookService/     
        //              GET 	    http://localhost:30523/RESTbookService/     
        //{id}          GET 	    http://localhost:30523/RESTbookService/{ID} 
        //              PUT 	    http://localhost:30523/RESTbookService/{ID} 
        //              DELETE 	    http://localhost:30523/RESTbookService/{ID} 
        //--------------------------------------------------------------------

        public static string uri = "http://localhost:30523/RESTbookService";
        
        #region restOperations      


        public static DataTable RESTgetALL()
        {
            string Method = "GET";            
                                    
            HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
            req.KeepAlive = false;
            req.Method = Method.ToUpper();

            HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
            Encoding enc = System.Text.Encoding.GetEncoding(1254);
            StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
            string ResponseStr = loResponseStream.ReadToEnd();
            loResponseStream.Close();
            resp.Close();
            
            DataTable dt = xml2DataTable(ResponseStr);

            return dt;
        }


        public static string RESTgetByid(string id)
        {            
            string ResponseStr = "";            
            try
            {                
                string Method = "GET";                
                string uriStr = uri + "/" + id;
                
                HttpWebRequest req = WebRequest.Create(uriStr) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = Method.ToUpper();

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                Encoding enc = System.Text.Encoding.GetEncoding(1254);
                StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);

                ResponseStr = loResponseStream.ReadToEnd();

                loResponseStream.Close();
                resp.Close();
                                
            }
            catch (Exception ex)
            {
                
                ResponseStr = "ERROR : " + ex.Message.ToString();
            }

            return ResponseStr;
        }



        public static string RESTpost(string bookName, string pubYear)
        {
            string ResponseStr = "";
            
            try
            {
                string content;                
                string Method = "POST";                                
                HttpWebRequest req = WebRequest.Create(uri) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = Method.ToUpper();

                //--------------                
                //<Book>
                //  <BookName>The Grapes of Wrath</BookName>
                //  <BookNo>0</BookNo>
                //  <PublicationYear>1939</PublicationYear>
                //</Book>

                content = "<Book xmlns=\"http://schemas.datacontract.org/2004/07/RESTbook\">" +
                            "<BookName>" + bookName + "</BookName>" + 
                            "<BookNo>0</BookNo>" + 
                            "<PublicationYear>" + pubYear + "</PublicationYear>" + 
                         "</Book>";

                byte[] buffer = Encoding.ASCII.GetBytes(content);
                req.ContentLength = buffer.Length;
                req.ContentType = "text/xml";
                Stream PostData = req.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();                
                //--------------

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                Encoding enc = System.Text.Encoding.GetEncoding(1254);
                StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
                ResponseStr = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                resp.Close();                
            }
            catch (Exception ex)
            {                
                ResponseStr =  "ERROR : " + ex.Message.ToString();
            }
            
            return ResponseStr;
        }


        public static string RESTput(string id, string bookName, string pubYear)
        {
            string ResponseStr = "";

            string uriStr = uri + "/" + id;

            try
            {
                string content;
                string Method = "PUT";
                HttpWebRequest req = WebRequest.Create(uriStr) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = Method.ToUpper();

                //--------------                
                //<Book>
                //  <BookName>The Grapes of Wrath</BookName>
                //  <BookNo>2</BookNo>
                //  <PublicationYear>1939</PublicationYear>
                //</Book>

                content = "<Book xmlns=\"http://schemas.datacontract.org/2004/07/RESTbook\">" +
                            "<BookName>" + bookName + "</BookName>" +
                            "<BookNo>" + id + "</BookNo>" +
                            "<PublicationYear>" + pubYear + "</PublicationYear>" +
                         "</Book>";

                byte[] buffer = Encoding.ASCII.GetBytes(content);
                req.ContentLength = buffer.Length;
                req.ContentType = "text/xml";
                Stream PostData = req.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                //--------------

                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                Encoding enc = System.Text.Encoding.GetEncoding(1254);
                StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
                ResponseStr = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                resp.Close();
            }
            catch (Exception ex)
            {
                ResponseStr = "ERROR : " + ex.Message.ToString();
            }

            return ResponseStr;
        }


        public static string RESTdelete(string id)
        {
            string ResponseStr = "";

            string uriStr = uri + "/" + id;

            try
            {                
                string Method = "DELETE";
                HttpWebRequest req = WebRequest.Create(uriStr) as HttpWebRequest;
                req.KeepAlive = false;
                req.Method = Method.ToUpper();

            
                HttpWebResponse resp = req.GetResponse() as HttpWebResponse;
                Encoding enc = System.Text.Encoding.GetEncoding(1254);
                StreamReader loResponseStream = new StreamReader(resp.GetResponseStream(), enc);
                ResponseStr = loResponseStream.ReadToEnd();
                loResponseStream.Close();
                resp.Close();
            }
            catch (Exception ex)
            {
                ResponseStr = "ERROR : " + ex.Message.ToString();
            }

            return ResponseStr;
        }

        #endregion



        #region Functions
  
        public static DataTable xml2DataTable(string xmlStr)
        {
            DataTable dt = new DataTable();

            DataSet dataSet = new DataSet();
            dataSet.ReadXml(new StringReader(xmlStr));

            //return single table inside of dataset
            if (dataSet.Tables.Count > 0)
            {
                dt = dataSet.Tables[0];
            }

            return dt;
        }

        #endregion

    }
}
