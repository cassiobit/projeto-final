using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using Store.Domain.Common;
using System.Linq;

namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer()
        {
        }

        public Customer(string name, string email, string taxIdType, string document, string address)
        {
            Name = name;
            Email = email;
            TaxIdType = taxIdType;
            Document = document;
            Address = address;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }

        [NotMapped]
        public string TaxIdType { get; private set; }
        public string Document { get; private set; }
        public string Address { get; private set; }

        public bool IsValid()
        {
            var v = new InlineValidator<Customer>();
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            v.RuleFor(x => x.TaxIdType).NotEmpty().Must(y => Constants.BrazilianTaxIdType.Equals(y));
            v.RuleFor(x => x.Document).NotEmpty().Must(y => y.All(z => char.IsDigit(z))).Must(_ => HasValidBrazilianDocument());
            v.RuleFor(x => x.Address).NotEmpty();

            ValidationResult = v.Validate(this);
            return ValidationResult.IsValid;
        }

        private bool HasValidBrazilianDocument()
        {
            if (Document.Length != 11)
            {
                return false;
            }

            if (Document.Equals("00000000000") ||
                Document.Equals("11111111111") ||
                Document.Equals("22222222222") ||
                Document.Equals("33333333333") ||
                Document.Equals("44444444444") ||
                Document.Equals("55555555555") ||
                Document.Equals("66666666666") ||
                Document.Equals("77777777777") ||
                Document.Equals("88888888888") ||
                Document.Equals("99999999999"))
            {
                return false;
            }

            int digit1Total = 0;
            int digit2Total = 0;

            int[] cpfDigits = new int[11];

            for (int i = 0; i < Document.Length; i++)
            {
                cpfDigits[i] = int.Parse(Document[i].ToString());
            }

            for (int i = 0; i < cpfDigits.Length - 2; i++)
            {
                digit1Total += cpfDigits[i] * (10 - i);
                digit2Total += cpfDigits[i] * (11 - i);
            }

            int digit1 = digit1Total % 11;
            digit1 = digit1 < 2
                ? 0
                : 11 - digit1;

            if (cpfDigits[9] != digit1)
            {
                return false;
            }

            digit2Total += digit1 * 2;

            int digit2 = digit2Total % 11;
            digit2 = digit2 < 2
                ? 0
                : 11 - digit2;

            if (cpfDigits[10] != digit2)
            {
                return false;
            }

            return true;
        }
    }
}