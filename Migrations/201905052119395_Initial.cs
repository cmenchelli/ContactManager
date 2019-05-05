namespace ContactManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressLine1 = c.String(nullable: false),
                        AddressLine2 = c.String(),
                        City = c.String(nullable: false),
                        StateCode = c.String(nullable: false),
                        Zip = c.String(),
                        AddressType = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        EmailAddress = c.String(),
                        Birthdate = c.DateTime(nullable: false),
                        NumberOfComputers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SummaryViewModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Contacts = c.Int(nullable: false),
                        Addresses = c.Int(nullable: false),
                        HomeAddresses = c.Int(nullable: false),
                        BusinessAddresses = c.Int(nullable: false),
                        OtherAddresses = c.Int(nullable: false),
                        Computers = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AddressContacts",
                c => new
                    {
                        Address_Id = c.Int(nullable: false),
                        Contact_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Address_Id, t.Contact_Id })
                .ForeignKey("dbo.Addresses", t => t.Address_Id, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.Contact_Id, cascadeDelete: true)
                .Index(t => t.Address_Id)
                .Index(t => t.Contact_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AddressContacts", "Contact_Id", "dbo.Contacts");
            DropForeignKey("dbo.AddressContacts", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.AddressContacts", new[] { "Contact_Id" });
            DropIndex("dbo.AddressContacts", new[] { "Address_Id" });
            DropTable("dbo.AddressContacts");
            DropTable("dbo.SummaryViewModels");
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
