using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<SpaceBody> SpaceBodies { get; set; }
    public DbSet<RingSystem> RingSystems { get; set; }
    public DbSet<Astronomer> Astronomers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RingSystem>()
            .HasOne(c => c.SpaceBody)
            .WithOne(p => p.RingSystem)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SpaceBody>()
            .HasOne(c => c.Discoverer)
            .WithMany()
            .HasForeignKey(sb => sb.DiscovererId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
