namespace OrderManagement.Application.Features.Customers.Query.GetCustomers;

public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, Response<PaginatedList<CustomerDetails>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetCustomersHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Response<PaginatedList<CustomerDetails>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
    {
        var response = new Response<PaginatedList<CustomerDetails>>();

        var customers = await _unitOfWork.CustomerRepository.GetQueryable(sort: request.Pagination.Sort);

        if(!string.IsNullOrEmpty(request.FirstName))
        {
            customers = customers.Where(c => EF.Functions.Like(c.FirstName, $"%{request.FirstName}%"));
        }

        if (!string.IsNullOrEmpty(request.LastName))
        {
            customers = customers.Where(c => EF.Functions.Like(c.LastName, $"%{request.LastName}%"));
        }

        if (!string.IsNullOrEmpty(request.Contact))
        {
            customers = customers.Where(c => EF.Functions.Like(c.Contact, $"%{request.Contact}%"));
        }

        if (!string.IsNullOrEmpty(request.Email))
        {
            customers = customers.Where(c => EF.Functions.Like(c.Email, $"%{request.Email}%"));
        }

        var customerDetails = customers.Select(x => new CustomerDetails
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Contact = x.Contact,
            Email = x.Email,
        });

        response.Data = new PaginatedList<CustomerDetails>(customerDetails, request.Pagination.PageNumber, request.Pagination.PageSize);

        return response;
    }
}
  