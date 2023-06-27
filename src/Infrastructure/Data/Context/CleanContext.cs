using Domain.Entity;
using Infrastructure.Data.Configuration;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Data.Context;

public class CleanContext : DbContext
{
    public CleanContext(DbContextOptions<CleanContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioConfiguration).Assembly);
        // Outras configurações...
    }
}
