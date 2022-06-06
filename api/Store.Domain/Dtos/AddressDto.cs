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

        private static StringBuilder _addressBuilder = new StringBuilder();

        public override string ToString(){
            _addressBuilder.Clear();
            
            _addressBuilder.Append(Street);
            _addressBuilder.Append(Constants.AddressStreetNumberSeparator);
            _addressBuilder.Append(Number);
            _addressBuilder.Append(Constants.AddressDefaultSeparator);
            _addressBuilder.Append(City);
            _addressBuilder.Append(Constants.AddressDefaultSeparator);
            _addressBuilder.Append(State);
            _addressBuilder.Append(Constants.AddressDefaultSeparator);
            _addressBuilder.Append(Country);
            _addressBuilder.Append(Constants.AddressDefaultSeparator);
            _addressBuilder.Append(ZipCode);

            return _addressBuilder.ToString();
        }
    }
}