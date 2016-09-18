namespace Jango.Lab.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeRecord : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ChargeRecords", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ChargeRecords", "CurrentAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ChargeRecords", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ChargeRecords", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.ChargeRecords", "CurrentAmount");
            DropColumn("dbo.ChargeRecords", "Amount");
        }
    }
}
