using System.Threading.Tasks;
using Store.Domain.Customers.Dtos;
using Store.Domain.DataBase;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Domain.Services.Interfaces;

namespace Store.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _database;
        private readonly ILoggingService _logger;

        public CustomerService(AppDbContext database, ILoggingService logger)
        {
            _database = database;
            _logger = logger;
        }


        public async Task<GenericResultDto> AddNewCustomer(NewCustomerDto newCustomer)
        {
            var brazilianCustomer = new Customer(
                name: newCustomer.Name,
                email: newCustomer.Email,
                taxIdType: newCustomer.DocumentInformation.TaxIdType,
                document: newCustomer.DocumentInformation.Number,
                address: newCustomer.Address.ToString()
            );

            if (!brazilianCustomer.IsValid())
            {
                return new GenericResultDto(brazilianCustomer.ValidationResult);
            }

            _logger.LogGCInfo();

            await _database.Customers.AddAsync(brazilianCustomer);
            await _database.SaveChangesAsync();

            return new GenericResultDto(brazilianCustomer.ValidationResult, brazilianCustomer);
        }
    }
}