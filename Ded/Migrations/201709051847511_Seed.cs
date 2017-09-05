namespace DeductionsAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Seed : DbMigration
    {
        public override void Up()
        {

            CreateTable(
                "dbo.Deductions",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    EmployeeDeduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DependentDeduction = c.Decimal(nullable: false, precision: 18, scale: 2),
                    Discount = c.Decimal(nullable: false, precision: 18, scale: 2),
                    NumberOfPayPeriods = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO dbo.Deductions (EmployeeDeduction, DependentDeduction, Discount, NumberOfPayPeriods) VALUES (1000.00, 500.00, 0.1, 26)");

            //CreateTable(
            //    "dbo.Dependents",
            //    c => new
            //    {
            //        DependentId = c.Int(nullable: false, identity: true),
            //        FirstName = c.String(nullable: false, maxLength: 50),
            //        EmployeeId = c.Int(nullable: false),
            //        Relationship_Id = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.DependentId)
            //    .ForeignKey("dbo.Employees", t => t.EmployeeId, cascadeDelete: true)
            //    .Index(t => t.EmployeeId);

            //CreateTable(
            //    "dbo.Employees",
            //    c => new
            //    {
            //        EmployeeId = c.Int(nullable: false, identity: true),
            //        FirstName = c.String(nullable: false, maxLength: 50),
            //        LastName = c.String(nullable: false, maxLength: 50),
            //        Salary = c.Decimal(nullable: false, precision: 18, scale: 2),
            //    })
            //    .PrimaryKey(t => t.EmployeeId);

            CreateTable(
                "dbo.Relationships",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false, maxLength: 10),
                })
                .PrimaryKey(t => t.Id);
            Sql("INSERT INTO dbo.Relationships (Description) Values ('Spouse')");
            Sql("INSERT INTO dbo.Relationships (Description) Values ('Child')");
            Sql("INSERT INTO dbo.Relationships (Description) Values ('Other')");

        }

        public override void Down()
        {
            DropForeignKey("dbo.Dependents", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Dependents", new[] { "EmployeeId" });
            DropTable("dbo.Relationships");
            DropTable("dbo.Employees");
            DropTable("dbo.Dependents");
            DropTable("dbo.Deductions");
        }
    }
}
