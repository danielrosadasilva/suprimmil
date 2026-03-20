using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using suprimmil.Context.Maps;
using suprimmil.Models;

namespace suprimmil.Context;

public partial class AppDbContext : IdentityDbContext<User, IdentityRole<int>, int>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // 2. Chame o base PRIMEIRO para configurar as tabelas do Identity (AspNetUsers, etc)
        base.OnModelCreating(modelBuilder);

        // 3. Suas configurações customizadas
        modelBuilder.ApplyConfiguration(new UserMap());

        // Extensão do PostgreSQL
        modelBuilder.HasPostgresExtension("unaccent");

        // Opcional: Se o UserMap sobrescrever algo do Identity que você não quer, 
        // configure ajustes finos aqui após o ApplyConfiguration.
        // Exemplo: modelBuilder.Entity<User>().ToTable("Users"); 
    }
}
