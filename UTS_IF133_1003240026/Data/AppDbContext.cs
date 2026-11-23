using Microsoft.EntityFrameworkCore;
using UTS_IF133_1003240026.Models;

namespace UTS_IF133_1003240026.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}