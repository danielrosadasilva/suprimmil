using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using suprimmil.Models;

namespace suprimmil.Context.Maps;

public class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(e => e.Id).HasName("users_pkey");
        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
        builder.Property(e => e.Password).HasColumnName("password").IsRequired().HasMaxLength(255);
        builder.Property(e => e.IsActive).HasColumnName("is_active").HasDefaultValue(false);
        builder.Property(e => e.IsAdmin).HasColumnName("is_admin").HasDefaultValue(false);
        builder.Property(e => e.LoginAttempts).HasColumnName("login_attempts").HasDefaultValue(0);
        builder.Property(e => e.IsLocked).HasColumnName("is_locked").HasDefaultValue(true);
    }
}