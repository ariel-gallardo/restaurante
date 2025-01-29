using Microsoft.EntityFrameworkCore;
using Restaurante.Models;

namespace Restaurante.Migrations.Extensions
{
    public static class MapBaseExtensions
    {
        public static ModelBuilder MapBaseString<T>(this ModelBuilder modelBuilder) where T : StringEntity 
        {
            modelBuilder.Entity<T>().HasKey(x => x.Id);
            modelBuilder.Entity<T>().Property(x => x.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.DeletedAt).HasColumnName("deleted_at").IsRequired(false);
            modelBuilder.Entity<T>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<T>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<T>().HasIndex(x => x.DeletedAt);
            return modelBuilder;
        }

        public static ModelBuilder MapBaseBigInt<T>(this ModelBuilder modelBuilder) where T : BigIntEntity
        {
            modelBuilder.Entity<T>().HasKey(x => x.Id);
            modelBuilder.Entity<T>().Property(x => x.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<T>().Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<T>().Property(x => x.UpdatedAt).HasColumnName("updated_at").IsRequired(false);
            modelBuilder.Entity<T>().Property(x => x.DeletedAt).HasColumnName("deleted_at").IsRequired(false);
            modelBuilder.Entity<T>().HasIndex(x => x.CreatedAt);
            modelBuilder.Entity<T>().HasIndex(x => x.UpdatedAt);
            modelBuilder.Entity<T>().HasIndex(x => x.DeletedAt);
            return modelBuilder;
        }
    }
}
