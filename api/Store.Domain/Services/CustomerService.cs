using Store.Domain.Customers.Dtos;
using Store.Domain.DataBase;
using Store.Domain.Dtos;
using Store.Domain.Entities;
using Store.Domain.Services.Interfaces;

namespace Store.Domain.Services
{
    public class CustomerService : ICustomerService
    {
        public readonly AppDbContext _database;

        public CustomerService(AppDbContext database)
        {
            _database = database;
        }


        public async Task<GenericResultDto> AddNewCustomer(NewCustomerDto newCustomer)
        {
            var brazilianCustomer = new Customer(
                name: newCustomer.Name,
                email: newCustomer.Email,
                taxIdType: newCustomer.DocumentInformation.TaxIdType, 
                document: newCustomer.DocumentInformation.Number
            );

            if(!brazilianCustomer.IsValid()){
                return new GenericResultDto(brazilianCustomer.ValidationResult);
            }

            await _database.Customers.AddAsync(brazilianCustomer);
            await _database.SaveChangesAsync();

            return new GenericResultDto(brazilianCustomer.ValidationResult, brazilianCustomer);
        }
    }
}