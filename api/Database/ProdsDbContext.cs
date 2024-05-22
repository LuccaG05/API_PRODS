using System;
using System.Collections.Generic;
using api.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace api.Database;

public partial class ProdsDbContext : DbContext
{
    public DbSet<Produto> Produtos { get; set; }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public ProdsDbContext()
    {
    }

    public ProdsDbContext(DbContextOptions<ProdsDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    {
        string conn = "server=localhost;user id=root;database=prods";
        optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
