namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Title = c.String(nullable: false),
                        Credits = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CourseId = c.Guid(nullable: false),
                        StudentId = c.Guid(nullable: false),
                        Grade = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LastName = c.String(nullable: false),
                        FirstName = c.String(nullable: false),
                        EnrollmentDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollment", "StudentId", "dbo.Student");
            DropForeignKey("dbo.Enrollment", "CourseId", "dbo.Course");
            DropIndex("dbo.Enrollment", new[] { "StudentId" });
            DropIndex("dbo.Enrollment", new[] { "CourseId" });
            DropTable("dbo.Student");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Course");
        }
    }
}
