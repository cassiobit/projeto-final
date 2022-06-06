using System.Text;
using Store.Domain.Common;

namespace Store.Domain.Dtos
{
    public class AddressDto
    {

        public string ZipCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            StringBuilder addressBuilder = new StringBuilder();

            addressBuilder.Append(Street);
            addressBuilder.Append(Constants.AddressStreetNumberSeparator);
            addressBuilder.Append(Number);
            addressBuilder.Append(Constants.AddressDefaultSeparator);
            addressBuilder.Append(City);
            addressBuilder.Append(Constants.AddressDefaultSeparator);
            addressBuilder.Append(State);
            addressBuilder.Append(Constants.AddressDefaultSeparator);
            addressBuilder.Append(Country);
            addressBuilder.Append(Constants.AddressDefaultSeparator);
            addressBuilder.Append(ZipCode);

            return addressBuilder.ToString();
        }
    }
}