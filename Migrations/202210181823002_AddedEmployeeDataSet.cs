namespace EPI_USE.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEmployeeDataSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeNumber = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Surname = c.String(nullable: false),
                        BirthDate = c.DateTime(nullable: false),
                        Salary = c.Double(nullable: false),
                        Position = c.String(nullable: false),
                        ReportingLineManager = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeNumber);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
