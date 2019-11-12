using System;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;

public class DataAccess
{
    private OleDbConnectionStringBuilder Builder =
        new OleDbConnectionStringBuilder
            {
                Provider = "Microsoft.ACE.OLEDB.12.0",
                DataSource = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database1.accdb")
            };

    public DataTable FamilyNames()
    {
        DataTable dt = new DataTable();
        using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ConnectionString })
        {
            using (OleDbCommand cmd = new OleDbCommand { Connection = cn })
            {
                cmd.CommandText =
                    @"
                    SELECT 
                        ID, 
                        FamilyName 
                    FROM FontNames;
                    ";
                cn.Open();
                dt.Load(cmd.ExecuteReader());
            }
        }
        return dt;
    }
    public DataTable AllFemaleNames()
    {
        DataTable dt = new DataTable();
        using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ConnectionString })
        {
            using (OleDbCommand cmd = new OleDbCommand { Connection = cn })
            {
                cmd.CommandText =
                    @"
                    SELECT Identifier, FirstName
                    FROM FirstNames
                    WHERE Gender = 'Female'
                    ORDER BY FirstName";
                cn.Open();
                dt.Load(cmd.ExecuteReader());
            }
        }
        return dt;
    }

    public AutoCompleteStringCollection AvailableFonts()
    {
        AutoCompleteStringCollection TheNameList = new AutoCompleteStringCollection();
        System.Drawing.Text.InstalledFontCollection ifc = new System.Drawing.Text.InstalledFontCollection();
        for (int i = 0; i < ifc.Families.Length; i++)
        {
           TheNameList.Add(ifc.Families[i].Name);
        }
        return TheNameList;
    }
    public AutoCompleteStringCollection LoadFemaleNames()
    {
        AutoCompleteStringCollection TheNameList = new AutoCompleteStringCollection();

        using (OleDbConnection cn = new OleDbConnection { ConnectionString = Builder.ConnectionString })
        {
            using (OleDbCommand cmd = new OleDbCommand { Connection = cn })
            {
                cmd.CommandText =
                @"
                    SELECT FirstName
                    FROM FirstNames
                    WHERE Gender = 'Female'
                    ORDER BY FirstName;
                    ";

                cn.Open();
                OleDbDataReader Reader = (OleDbDataReader)cmd.ExecuteReader();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        TheNameList.Add(Reader.GetString(0));
                    }
                    Reader.Close();
                }
            }
        }
        return TheNameList;
    }
}