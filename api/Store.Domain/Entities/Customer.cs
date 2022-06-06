using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation;
using Store.Domain.Common;

namespace Store.Domain.Entities
{
    public class Customer : Entity
    {
        public Customer()
        {
        }

        public Customer(string name, string email, string taxIdType, string document)
        {
            Name = name;
            Email = email;
            TaxIdType = taxIdType;
            Document = document;
        }

        public string Name { get; private set; }
        public string Email { get; private set; }

        [NotMapped]
        public string TaxIdType { get; private set; }
        public string Document { get; private set; }

        public bool IsValid()
        {
            var v = new InlineValidator<Customer>();
            v.RuleFor(x => x.Name).NotEmpty();
            v.RuleFor(x => x.Email).NotEmpty().EmailAddress();
            v.RuleFor(x => x.TaxIdType).NotEmpty().Must(y => Constants.BrazilianTaxIdType.Equals(y));
            v.RuleFor(x => x.Document).NotEmpty().Must(y => y.All(z => char.IsDigit(z))).Must(_ => HasValidBrazilianDocument());

            ValidationResult = v.Validate(this);
            return ValidationResult.IsValid;
        }

        private bool HasValidBrazilianDocument()
        {
            if (Document == null)
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

            foreach (var digitCharacter in Document)
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