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

        [NotMapped]
        private static InlineValidator<Customer> _validator = new InlineValidator<Customer>();
        public bool IsValid()
        {
            _validator.RuleFor(x => x.Name).NotEmpty();
            _validator.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            _validator.RuleFor(x => x.TaxIdType).NotEmpty().Must(y => Constants.BrazilianTaxIdType.Equals(y));
            _validator.RuleFor(x => x.Document).NotEmpty()
                .Must(y => y.All(z => char.IsDigit(z)))
                .Must(z => HasValidBrazilianDocument(z));
            _validator.RuleFor(x => x.Address).NotEmpty();

            ValidationResult = _validator.Validate(this);
            return ValidationResult.IsValid;
        }

        private static bool HasValidBrazilianDocument(string cpf)
        {
            if (cpf == null)
            {
                return false;
            }

            var documentIndex = 0;
            var digit1Total = 0;
            var digit2Total = 0;

            bool identicDigits = true;

            var lastDigitProcessed = -1;

            var validatorDigit1 = 0;
            var validatorDigit2 = 0;

            foreach (var digitCharacter in cpf)
            {
                var digit = digitCharacter - Constants.DocumentReferenceCharacter;
                if (documentIndex != 0 && lastDigitProcessed != digit)
                {
                    identicDigits = false;
                }

                lastDigitProcessed = digit;
                if (documentIndex < 9)
                {
                    digit1Total += digit * (10 - documentIndex);
                    digit2Total += digit * (11 - documentIndex);
                }
                else if (documentIndex == 9)
                {
                    validatorDigit1 = digit;
                }
                else if (documentIndex == 10)
                {
                    validatorDigit2 = digit;
                }

                documentIndex++;

            }

            if (documentIndex > 11)
            {
                return false;
            }

            if (identicDigits)
            {
                return false;
            }

            var digit1 = digit1Total % 11;
            digit1 = digit1 < 2
                ? 0
                : 11 - digit1;

            if (validatorDigit1 != digit1)
            {
                return false;
            }

            digit2Total += digit1 * 2;
            var digit2 = digit2Total % 11;
            digit2 = digit2 < 2
                ? 0
                : 11 - digit2;

            return validatorDigit2 == digit2;
        }
    }
}