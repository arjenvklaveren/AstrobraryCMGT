using System;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<SpaceBody> SpaceBodies { get; set; }
}
