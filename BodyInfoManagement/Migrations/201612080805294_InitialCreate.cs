namespace BodyInfoManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrators",
                c => new
                    {
                        AdministratorId = c.String(nullable: false, maxLength: 128),
                        AdministratorPassword = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AdministratorId);
            
            CreateTable(
                "dbo.HealthInfoes",
                c => new
                    {
                        HealthInfoId = c.Int(nullable: false, identity: true),
                        StudentId = c.String(maxLength: 10),
                        ExamDate = c.DateTime(nullable: false),
                        Height = c.Double(nullable: false),
                        Weigh = c.Double(nullable: false),
                        Jump = c.Double(nullable: false),
                        Breath = c.Double(nullable: false),
                        Seated = c.Double(nullable: false),
                        LongRun = c.Time(nullable: false, precision: 7),
                        ShortRun = c.Time(nullable: false, precision: 7),
                        Pull = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.HealthInfoId)
                .ForeignKey("dbo.Students", t => t.StudentId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 10),
                        StudentGender = c.Int(nullable: false),
                        SaltCode = c.String(),
                        StudentPassword = c.String(nullable: false, maxLength: 50),
                        StudentEmail = c.String(),
                        StudentAuth = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.StudentId);
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        TeacherId = c.String(nullable: false, maxLength: 50),
                        SaltCode = c.String(),
                        TeacherPassword = c.String(nullable: false, maxLength: 50),
                        TeacherEmail = c.String(),
                        StudentAuth = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HealthInfoes", "StudentId", "dbo.Students");
            DropIndex("dbo.HealthInfoes", new[] { "StudentId" });
            DropTable("dbo.Teachers");
            DropTable("dbo.Students");
            DropTable("dbo.HealthInfoes");
            DropTable("dbo.Administrators");
        }
    }
}
