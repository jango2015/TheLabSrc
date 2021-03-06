namespace Jango.Lab.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitializeDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChargeCards",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CardNO = c.String(maxLength: 50, storeType: "nvarchar"),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GiftIntegral = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsValid = c.Boolean(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Remark = c.String(maxLength: 500, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ChargeRecords",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CardID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CardOrderStatus = c.Int(nullable: false),
                        PaySatus = c.Int(nullable: false),
                        SubmitAt = c.DateTime(nullable: false, precision: 0),
                        PaiedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Coachers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        ShopID = c.Long(nullable: false),
                        Avatar = c.String(maxLength: 100, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        CreatedUser = c.String(unicode: false),
                        Name = c.String(maxLength: 50, storeType: "nvarchar"),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseCategories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100, storeType: "nvarchar"),
                        Remark = c.String(maxLength: 500, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        Creator = c.String(maxLength: 50, storeType: "nvarchar"),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedUser = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CourseCoachers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CourseID = c.Long(nullable: false),
                        CoacherID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100, storeType: "nvarchar"),
                        Desc = c.String(maxLength: 1000, storeType: "nvarchar"),
                        m_EnumCourseType = c.Int(nullable: false),
                        IntegralUse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BalanceUse = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CourseType = c.Int(nullable: false),
                        m_CourseCategoryId = c.Long(nullable: false),
                        CourseBeginTime = c.DateTime(nullable: false, precision: 0),
                        CourseEndTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseReserveRecords",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        CourseID = c.Long(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        IsQRCodeUsded = c.Boolean(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedUser = c.String(unicode: false),
                        QRCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        ReserveTime = c.DateTime(nullable: false, precision: 0),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseSignInRecords",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CourseID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        CourseTellerID = c.Long(nullable: false),
                        SignInTime = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseTellers",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        CourseID = c.Long(nullable: false),
                        UserID = c.Long(nullable: false),
                        QRCode = c.String(maxLength: 50, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        IsValid = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Logs",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Action = c.String(maxLength: 50, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        Message = c.String(maxLength: 4000, storeType: "nvarchar"),
                        Operator = c.String(maxLength: 100, storeType: "nvarchar"),
                        UserID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OpenID = c.String(maxLength: 50, storeType: "nvarchar"),
                        Content = c.String(maxLength: 500, storeType: "nvarchar"),
                        MsgType = c.Int(nullable: false),
                        SendTime = c.DateTime(nullable: false, precision: 0),
                        Status = c.Int(nullable: false),
                        UserID = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.OrderItems",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OrderID = c.Long(nullable: false),
                        ProductID = c.Long(nullable: false),
                        ProductType = c.Int(nullable: false),
                        ProductName = c.String(unicode: false),
                        ProductPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductRemark = c.String(maxLength: 500, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        OrderNo = c.String(maxLength: 50, storeType: "nvarchar"),
                        TradeId = c.String(unicode: false),
                        DispatchStatus = c.Int(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedUser = c.String(maxLength: 100, storeType: "nvarchar"),
                        OrderPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        OrderStatus = c.Int(nullable: false),
                        PaidAt = c.DateTime(nullable: false, precision: 0),
                        PayStatus = c.Int(nullable: false),
                        PayTerm = c.Int(nullable: false),
                        SubmitAt = c.DateTime(nullable: false, precision: 0),
                        UserID = c.Long(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        CreatedUser = c.String(maxLength: 100, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserAccounts",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        AccountType = c.Int(nullable: false),
                        Amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserConsigneeInfoes",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        UserID = c.Long(nullable: false),
                        ConsigneeUserName = c.String(maxLength: 100, storeType: "nvarchar"),
                        ConsigneeUserMobile = c.String(maxLength: 50, storeType: "nvarchar"),
                        ConsigneeUserAddress = c.String(maxLength: 200, storeType: "nvarchar"),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        IsValid = c.Boolean(nullable: false),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Long(nullable: false, identity: true),
                        Birthday = c.DateTime(nullable: false, precision: 0),
                        CreatedAt = c.DateTime(nullable: false, precision: 0),
                        Email = c.String(maxLength: 100, storeType: "nvarchar"),
                        Level = c.Int(nullable: false),
                        Mobile = c.String(maxLength: 50, storeType: "nvarchar"),
                        ModifiedAt = c.DateTime(nullable: false, precision: 0),
                        Name = c.String(maxLength: 100, storeType: "nvarchar"),
                        OpenID = c.String(maxLength: 50, storeType: "nvarchar"),
                        Code = c.String(maxLength: 50, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.UserConsigneeInfoes");
            DropTable("dbo.UserAccounts");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderItems");
            DropTable("dbo.Messages");
            DropTable("dbo.Logs");
            DropTable("dbo.CourseTellers");
            DropTable("dbo.CourseSignInRecords");
            DropTable("dbo.CourseReserveRecords");
            DropTable("dbo.CourseInfoes");
            DropTable("dbo.CourseCoachers");
            DropTable("dbo.CourseCategories");
            DropTable("dbo.Coachers");
            DropTable("dbo.ChargeRecords");
            DropTable("dbo.ChargeCards");
        }
    }
}
