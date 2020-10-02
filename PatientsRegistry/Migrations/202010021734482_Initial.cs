namespace PatientsRegistry.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.DoctorID)
                .ForeignKey("dbo.Users", t => t.PatientID)
                .Index(t => t.PatientID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        IsDoctor = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PatientID", "dbo.Users");
            DropForeignKey("dbo.Appointments", "DoctorID", "dbo.Users");
            DropIndex("dbo.Appointments", new[] { "DoctorID" });
            DropIndex("dbo.Appointments", new[] { "PatientID" });
            DropTable("dbo.Users");
            DropTable("dbo.Appointments");
        }
    }
}
