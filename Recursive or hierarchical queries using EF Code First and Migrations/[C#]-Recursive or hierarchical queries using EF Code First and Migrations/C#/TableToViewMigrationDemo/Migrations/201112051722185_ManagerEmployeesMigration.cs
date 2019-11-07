namespace TableToViewMigrationDemo.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ManagerEmployeesMigration : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "ManagerEmployees",
            //    c => new
            //        {
            //            ManagerEmployeeID = c.Int(nullable: false),
            //            EmployeeID = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => new { t.ManagerEmployeeID, t.EmployeeID });

            Sql(@"CREATE VIEW [dbo].[ManagerEmployees]
AS
    WITH    cte ( ManagerEmployeeID, EmployeeID )
              AS ( SELECT   EmployeeID ,
                            EmployeeID
                   FROM     dbo.Employees
                   UNION ALL
                   SELECT   e.EmployeeID ,
                            cte.EmployeeID
                   FROM     cte
                            INNER JOIN dbo.Employees AS e ON e.ReportsToEmployeeID = cte.ManagerEmployeeID
                 )
    SELECT  ISNULL(EmployeeID, 0) AS ManagerEmployeeID ,
            ISNULL(ManagerEmployeeID, 0) AS EmployeeID
    FROM    cte");
        }

        public override void Down()
        {
            //DropTable("ManagerEmployees");
            Sql("DROP VIEW [dbo].[ManagerEmployees]");
        }
    }
}
