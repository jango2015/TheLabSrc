namespace Jango.Lab.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIgnoreField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CourseInfoes", "IsReserved", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CourseInfoes", "IsReserved");
        }
    }
}
