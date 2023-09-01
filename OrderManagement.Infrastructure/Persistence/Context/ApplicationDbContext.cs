
namespace OrderManagement.Infrastructure.Persistence.Context;

public class ApplicationDbContext : IdentityDbContext<User>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    public virtual DbSet<Product> Products => Set<Product>();
    public virtual DbSet<Order> Orders => Set<Order>();
    public virtual DbSet<Customer> Customers => Set<Customer>();
    public void Save()
    {
        base.SaveChanges();
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }
}