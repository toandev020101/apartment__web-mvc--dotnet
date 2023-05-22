namespace web_chung_cu.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apartments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 45),
                        slug = c.String(nullable: false, maxLength: 45),
                        address = c.String(nullable: false),
                        totalFloor = c.Int(nullable: false),
                        totalRoom = c.Int(nullable: false),
                        status = c.Int(nullable: false),
                        userId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.userId, cascadeDelete: true)
                .Index(t => t.userId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        imageName = c.String(),
                        title = c.String(nullable: false),
                        slug = c.String(nullable: false),
                        price = c.Int(nullable: false),
                        pricePile = c.Int(nullable: false),
                        floorNumber = c.Int(nullable: false),
                        roomNumber = c.Int(nullable: false),
                        bedroomNumber = c.Int(nullable: false),
                        toletNumber = c.Int(nullable: false),
                        area = c.Int(nullable: false),
                        isInterior = c.Int(nullable: false),
                        description = c.String(),
                        status = c.Int(nullable: false),
                        apartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Apartments", t => t.apartmentId, cascadeDelete: true)
                .Index(t => t.apartmentId);
            
            CreateTable(
                "dbo.RoomImages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false),
                        path = c.String(nullable: false),
                        roomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Rooms", t => t.roomId, cascadeDelete: true)
                .Index(t => t.roomId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        avatar = c.String(),
                        firstName = c.String(nullable: false, maxLength: 45),
                        lastName = c.String(nullable: false, maxLength: 45),
                        phoneNumber = c.String(nullable: false, maxLength: 15),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apartments", "userId", "dbo.Users");
            DropForeignKey("dbo.RoomImages", "roomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "apartmentId", "dbo.Apartments");
            DropIndex("dbo.RoomImages", new[] { "roomId" });
            DropIndex("dbo.Rooms", new[] { "apartmentId" });
            DropIndex("dbo.Apartments", new[] { "userId" });
            DropTable("dbo.Users");
            DropTable("dbo.RoomImages");
            DropTable("dbo.Rooms");
            DropTable("dbo.Apartments");
        }
    }
}
