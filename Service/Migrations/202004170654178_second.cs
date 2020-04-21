namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Edges", "Destination_Id", "dbo.Nodes");
            DropIndex("dbo.Edges", new[] { "Destination_Id" });
            AlterColumn("dbo.Edges", "Destination_Id", c => c.Int());
            CreateIndex("dbo.Edges", "Destination_Id");
            AddForeignKey("dbo.Edges", "Destination_Id", "dbo.Nodes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Edges", "Destination_Id", "dbo.Nodes");
            DropIndex("dbo.Edges", new[] { "Destination_Id" });
            AlterColumn("dbo.Edges", "Destination_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Edges", "Destination_Id");
            AddForeignKey("dbo.Edges", "Destination_Id", "dbo.Nodes", "Id", cascadeDelete: true);
        }
    }
}
