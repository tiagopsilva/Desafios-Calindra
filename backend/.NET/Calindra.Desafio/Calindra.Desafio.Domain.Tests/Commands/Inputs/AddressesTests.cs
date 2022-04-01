using Calindra.Desafio.Domain.Commands.Inputs;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Commands.Inputs
{
    public class AddressesTests
    {
        [Fact]
        public void DeveRetornarFalhasQuandoHaMenosDeDoisEnderecos()
        {
            // Arrange
            var addresses = new Addresses();
            addresses.AddressList.Add("Endereço qualquer");

            // Act
            var validationResults = addresses.Validate(new ValidationContext(addresses));

            // Assert
            Assert.NotNull(validationResults);
            Assert.NotEmpty(validationResults);
        }
    }
}
