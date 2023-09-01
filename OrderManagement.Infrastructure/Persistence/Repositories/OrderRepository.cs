using System.Data;

namespace OrderManagement.Infrastructure.Persistence.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    public OrderRepository(ApplicationDbContext context) : base(context) { }

    public override async Task<Order> GetById(int id)
    {
        return await _context.Orders.Include(o => o.Customer).Include(o => o.Product).SingleAsync(o => o.Id == id);
    }

    public override async Task Add(Order entity)
    {
        SqlParameter customerIdParameter = new SqlParameter(CreateOrderStoredProcedureConstants.CustomerId, entity.CustomerId);
        SqlParameter productIdParameter = new SqlParameter(CreateOrderStoredProcedureConstants.ProductId, entity.ProductId);
        SqlParameter quantityParameter = new SqlParameter(CreateOrderStoredProcedureConstants.Quantity, entity.Quantity);
        var idParameter = new SqlParameter { ParameterName = CreateOrderStoredProcedureConstants.Id, Direction = ParameterDirection.Output, DbType = DbType.Int32 };
        var totalCostParameter = new SqlParameter { ParameterName = CreateOrderStoredProcedureConstants.TotalCost, Direction = ParameterDirection.Output, DbType = DbType.Double };

        await _context.Database.ExecuteSqlRawAsync(CreateOrderStoredProcedureConstants.Command, customerIdParameter, productIdParameter, quantityParameter, idParameter, totalCostParameter);

        entity.Id = Convert.ToInt32(idParameter.Value);
        entity.TotalCost = Convert.ToDouble(totalCostParameter.Value);
    }
}