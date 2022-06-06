using Store.Domain.Dtos;

namespace Store.Domain.Customers.Dtos
{
    public class NewCustomerDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DocumentDto DocumentInformation { get; set; }
        public AddressDto Address { get; set; }
    }
}