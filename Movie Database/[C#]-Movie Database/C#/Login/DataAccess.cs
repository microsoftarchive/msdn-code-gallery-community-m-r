using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;

namespace Login
{
    class DataAccess
    {
        private static byte[] imagetoByte(Image img)
        {
            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public static Image ByteToImage(Byte[] byt)
        {
            MemoryStream ms = new MemoryStream(byt);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }


        static SqlConnection cn = new SqlConnection("Data Source=saif-server;Initial Catalog=sabiha;Integrated Security=True");
        public static int addEditImage(string name, int year, string actor, string actress, string category, string quality, string sound, string language, string myopinion, string director, Image image, string link)
        {
            Byte[] img = imagetoByte(image);
            SqlCommand cmd = new SqlCommand("addEditImage", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@actor", actor);
            cmd.Parameters.AddWithValue("@actress", actress);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@quality", quality);
            cmd.Parameters.AddWithValue("@sound", sound);
            cmd.Parameters.AddWithValue("@language", language);
            cmd.Parameters.AddWithValue("@myopinion", myopinion);
            cmd.Parameters.AddWithValue("@director", director);
            cmd.Parameters.AddWithValue("@image", img);
            cmd.Parameters.AddWithValue("@link", link);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            int x = cmd.ExecuteNonQuery();
            cn.Close();
            return x;
        }

        public static int updateTableMovie(string name, int year, string actor, string actress, string category, string quality, string sound, string language, string myopinion, string director, Image image, string link, string updateName, int updateYear)
        {
            Byte[] img = imagetoByte(image);
            SqlCommand cmd = new SqlCommand("updateTableMovie", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@year", year);
            cmd.Parameters.AddWithValue("@actor", actor);
            cmd.Parameters.AddWithValue("@actress", actress);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@quality", quality);
            cmd.Parameters.AddWithValue("@sound", sound);
            cmd.Parameters.AddWithValue("@language", language);
            cmd.Parameters.AddWithValue("@myopinion", myopinion);
            cmd.Parameters.AddWithValue("@director", director);
            cmd.Parameters.AddWithValue("@image", img);
            cmd.Parameters.AddWithValue("@updateName", updateName);
            cmd.Parameters.AddWithValue("@updateYear", updateYear);
            cmd.Parameters.AddWithValue("@link", link);
            if (cn.State == ConnectionState.Closed)
            {
                cn.Open();
            }
            int x = cmd.ExecuteNonQuery();
            cn.Close();
            return x;
        }

        public static DataTable getAllImages()
        {
            SqlDataAdapter DA = new SqlDataAdapter("getAllImages", cn);
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable DT = new DataTable();
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            DA.Fill(DT);
            cn.Close();
            return DT;
        }
        
        public static DataTable getImage(string name, int year)
        {
            SqlDataAdapter DA = new SqlDataAdapter("getImage", cn);
            DA.SelectCommand.Parameters.AddWithValue("@name", name);
            DA.SelectCommand.Parameters.AddWithValue("@year", year);
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable DT = new DataTable();
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            DA.Fill(DT);
            cn.Close();
            return DT;
        }
        public static DataTable getImage1(int year)
        {
            SqlDataAdapter DA = new SqlDataAdapter("getImage1", cn);
            DA.SelectCommand.Parameters.AddWithValue("@year", year);
            DA.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable DT = new DataTable();
            if (cn.State == ConnectionState.Closed)
                cn.Open();
            DA.Fill(DT);
            cn.Close();
            return DT;
        }
    }
}
