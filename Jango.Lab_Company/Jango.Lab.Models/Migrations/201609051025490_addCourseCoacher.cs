namespace Jango.Lab.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCourseCoacher : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CourseCoachers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CourseID = c.Long(nullable: false),
                        CoacherID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CourseCoachers");
        }
    }
}
