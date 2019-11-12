namespace TableToViewMigrationDemo.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        ReportsToEmployeeID = c.Int(),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("Employees", t => t.ReportsToEmployeeID)
                .Index(t => t.ReportsToEmployeeID);
            
        }
        
        public override void Down()
        {
            DropIndex("Employees", new[] { "ReportsToEmployeeID" });
            DropForeignKey("Employees", "ReportsToEmployeeID", "Employees", "EmployeeID");
            DropTable("Employees");
        }
    }
}
