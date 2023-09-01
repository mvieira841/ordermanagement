namespace OrderManagement.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    public IProductRepository ProductRepository { get; }

    public IOrderRepository OrderRepository { get; }

    public ICustomerRepository CustomerRepository { get; }

    int Save();
}