namespace Jango.Lab.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class courseTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseInfoes", "CourseBeginTime", c => c.DateTime(nullable: false, precision: 0));
            AddColumn("dbo.CourseInfoes", "CourseEndTime", c => c.DateTime(nullable: false, precision: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseInfoes", "CourseEndTime");
            DropColumn("dbo.CourseInfoes", "CourseBeginTime");
        }
    }
}
