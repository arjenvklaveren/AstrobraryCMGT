using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<SpaceBody> SpaceBodies { get; set; }
    public DbSet<RingSystem> RingSystems { get; set; }
    public DbSet<Astronomer> Astronomers { get; set; }
}
