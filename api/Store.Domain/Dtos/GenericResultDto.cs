using FluentValidation.Results;
using System;

namespace Store.Domain.Dtos
{
    public class GenericResultDto
    {

        public ValidationResult ValidationResult { get; private set; }
        public Object Result { get; private set; }

        public GenericResultDto(ValidationResult validationResult, object result)
        {
            ValidationResult = validationResult;
            Result = result;
        }

        public GenericResultDto(ValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}