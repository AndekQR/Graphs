namespace Service.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Edges",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Weight = c.Double(nullable: false),
                        Uid = c.String(),
                        Destination_Id = c.Int(nullable: false),
                        GraphPart_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nodes", t => t.Destination_Id, cascadeDelete: true)
                .ForeignKey("dbo.GraphParts", t => t.GraphPart_Id)
                .Index(t => t.Destination_Id)
                .Index(t => t.GraphPart_Id);
            
            CreateTable(
                "dbo.Nodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Label = c.String(),
                        Uid = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GraphParts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.String(),
                        Graph_Id = c.Int(),
                        Node_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Graphs", t => t.Graph_Id)
                .ForeignKey("dbo.Nodes", t => t.Node_Id)
                .Index(t => t.Graph_Id)
                .Index(t => t.Node_Id);
            
            CreateTable(
                "dbo.Graphs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Uid = c.String(),
                        Directed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GraphParts", "Node_Id", "dbo.Nodes");
            DropForeignKey("dbo.GraphParts", "Graph_Id", "dbo.Graphs");
            DropForeignKey("dbo.Edges", "GraphPart_Id", "dbo.GraphParts");
            DropForeignKey("dbo.Edges", "Destination_Id", "dbo.Nodes");
            DropIndex("dbo.GraphParts", new[] { "Node_Id" });
            DropIndex("dbo.GraphParts", new[] { "Graph_Id" });
            DropIndex("dbo.Edges", new[] { "GraphPart_Id" });
            DropIndex("dbo.Edges", new[] { "Destination_Id" });
            DropTable("dbo.Graphs");
            DropTable("dbo.GraphParts");
            DropTable("dbo.Nodes");
            DropTable("dbo.Edges");
        }
    }
}
