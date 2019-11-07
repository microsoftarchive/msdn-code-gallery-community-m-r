' ReSharper disable once CheckNamespace
Module ConnectionStringSetting
    Public Builder As New OleDb.OleDbConnectionStringBuilder With
        {
        .DataSource = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "Database1.accdb"),
        .Provider = "Microsoft.ACE.OLEDB.12.0"
        }
End Module
