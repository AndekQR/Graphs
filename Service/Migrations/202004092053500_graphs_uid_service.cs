namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class graphs_uid_service : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Edges", "Uid", c => c.String());
            AddColumn("dbo.Nodes", "Uid", c => c.String());
            AddColumn("dbo.GraphParts", "Uid", c => c.String());
            AddColumn("dbo.Graphs", "Uid", c => c.String());
            DropTable("dbo.Products");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Graphs", "Uid");
            DropColumn("dbo.GraphParts", "Uid");
            DropColumn("dbo.Nodes", "Uid");
            DropColumn("dbo.Edges", "Uid");
        }
    }
}
