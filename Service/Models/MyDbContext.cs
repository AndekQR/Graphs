using System.Data.Entity;

namespace Service.Models {
    public class MyDbContext : DbContext {
        public MyDbContext()
            : base("name=MyDbContext") {
        }

        public virtual DbSet<Graph> Graphs { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<Edge> Edges { get; set; }
        public virtual DbSet<GraphPart> GraphParts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        }
    }
}