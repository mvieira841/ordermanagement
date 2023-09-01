namespace OrderManagement.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IProductRepository ProductRepository { get; }

    public IOrderRepository OrderRepository { get; }

    public ICustomerRepository CustomerRepository { get; }

    public UnitOfWork(ApplicationDbContext context, IProductRepository productRepository, IOrderRepository orderRepository, ICustomerRepository customerRepository)
    {
        _context = context;
        ProductRepository = productRepository;
        OrderRepository = orderRepository;
        CustomerRepository = customerRepository;
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
