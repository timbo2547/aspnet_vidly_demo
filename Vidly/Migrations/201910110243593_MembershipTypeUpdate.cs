namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MembershipTypeUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "MembershipTypeName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MembershipTypes", "MembershipTypeName");
        }
    }
}
