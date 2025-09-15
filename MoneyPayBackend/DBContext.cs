using Microsoft.EntityFrameworkCore;
using MoneyPayBackend.Model;

public class MoneyPayDBContext : DbContext
{
    public MoneyPayDBContext(DbContextOptions<MoneyPayDBContext> options) : base(options) { }

    public DbSet<UserModel> User { get; set; }
    public DbSet<TypeModel> MoneyTypes { get; set; }
}