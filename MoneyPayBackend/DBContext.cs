using Microsoft.EntityFrameworkCore;
using MoneyPayBackend.Model;

public class MoneyPayDBContext : DbContext
{
    public MoneyPayDBContext(DbContextOptions<MoneyPayDBContext> options) : base(options) { }

    public DbSet<UserModel> Users { get; set; }
    public DbSet<MoneyTypesModel> Categories { get; set; }
    public DbSet<MoneyTypeDetailModel> Transactions { get; set; }
    public DbSet<MoneyTypeDetailRemarkModel> CategoryItems { get; set; }
}
