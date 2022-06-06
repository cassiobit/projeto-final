using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace Store.Domain.Entities
{
    public class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }
    }
}