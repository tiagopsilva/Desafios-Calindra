using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Calindra.Desafio.Domain.Commands.Inputs
{
    public class Addresses : ICommand, IValidatableObject
    {
        public Addresses()
        {
            AddressList = new List<string>();
        }

        public List<string> AddressList { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddressList.Count < 2)
                yield return new ValidationResult("2 addresses or more must be provided", new[] { nameof(AddressList) });
        }
    }
}
