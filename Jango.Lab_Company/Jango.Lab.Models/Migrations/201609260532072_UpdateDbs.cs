namespace Jango.Lab.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDbs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChargeRecords", "OpenId", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ChargeRecords", "OpenId");
        }
    }
}
