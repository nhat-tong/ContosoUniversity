namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComplexeModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(),
                        InstructorId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastName = c.String(maxLength: 50),
                        FirstName = c.String(maxLength: 50),
                        HireDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorId = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorId)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        CourseId = c.Guid(nullable: false),
                        InstructorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CourseId, t.InstructorId })
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Instructor", t => t.InstructorId, cascadeDelete: true)
                .Index(t => t.CourseId)
                .Index(t => t.InstructorId);
            
            AddColumn("dbo.Course", "DepartmentId", c => c.Int(nullable: false));
            CreateIndex("dbo.Course", "DepartmentId");
            AddForeignKey("dbo.Course", "DepartmentId", "dbo.Department", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CourseInstructor", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.CourseInstructor", "CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "DepartmentId", "dbo.Department");
            DropForeignKey("dbo.Department", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.OfficeAssignment", "InstructorId", "dbo.Instructor");
            DropIndex("dbo.CourseInstructor", new[] { "InstructorId" });
            DropIndex("dbo.CourseInstructor", new[] { "CourseId" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorId" });
            DropIndex("dbo.Department", new[] { "InstructorId" });
            DropIndex("dbo.Course", new[] { "DepartmentId" });
            DropColumn("dbo.Course", "DepartmentId");
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.Instructor");
            DropTable("dbo.Department");
        }
    }
}
