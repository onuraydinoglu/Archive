﻿using ArchiveApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ArchiveApp.Repository.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Category> Categories { get; set; }

    public DbSet<SubCategory> SubCategories { get; set; }
}
