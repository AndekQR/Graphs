namespace Service.Models {

    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyDbContext : DbContext {
        public virtual DbSet<Graph> Graphs { get; set; }
        public virtual DbSet<Node> Nodes { get; set; }
        public virtual DbSet<Edge> Edges { get; set; }
        public virtual DbSet<GraphPart> Pairs { get; set; }

        public MyDbContext()
            : base("name=MyDbContext") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
        }
    }
}