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
            string fullAddress = Street;
            fullAddress = fullAddress + Constants.AddressStreetNumberSeparator;
            fullAddress = fullAddress + Number;
            fullAddress = fullAddress + Constants.AddressDefaultSeparator;
            fullAddress = fullAddress + City;
            fullAddress = fullAddress + Constants.AddressDefaultSeparator;
            fullAddress = fullAddress + State;
            fullAddress = fullAddress + Constants.AddressDefaultSeparator;
            fullAddress = fullAddress + Country;
            fullAddress = fullAddress + Constants.AddressDefaultSeparator;
            fullAddress = fullAddress + ZipCode;

            return fullAddress;
        }
    }
}