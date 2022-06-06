using Store.Domain.Customers.Dtos;
using Store.Domain.Dtos;

namespace Store.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<GenericResultDto> AddNewCustomer(NewCustomerDto newCustomer);
    }
}