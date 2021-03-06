using Store.Domain.Customers.Dtos;
using Store.Domain.Dtos;
using System.Threading.Tasks;

namespace Store.Domain.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<GenericResultDto> AddNewCustomer(NewCustomerDto newCustomer);
    }
}