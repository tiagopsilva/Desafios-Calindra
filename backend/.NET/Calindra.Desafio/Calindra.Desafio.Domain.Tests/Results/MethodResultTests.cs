using Calindra.Desafio.Domain.Results;
using System.Linq;
using Xunit;

namespace Calindra.Desafio.Domain.Tests.Results
{
    public class MethodResultTests
    {
        [Fact]
        public void DadoMethodResultSemErrosDeveRetornarTrueEmSuccess()
        {
            // Arrange
            var methodResult = new MethodResult();

            // Act
            // Assert
            Assert.True(methodResult.Success);
            Assert.False(methodResult.Failure);
        }

        [Fact]
        public void DadoMethodResultComErrosDeveRetornarTrueEmFailure()
        {
            // Arrange
            var methodResult = new MethodResult();

            // Act
            methodResult.Add(string.Empty, "Mensagem de erro");

            // Assert
            Assert.True(methodResult.Failure);
            Assert.False(methodResult.Success);
        }

        [Fact]
        public void Dado2FalhasDaMesmaPropriedadeDeveMesclarMensagensNaMesmaFalha()
        {
            // Arrange
            const string mensagemDeErro1 = "Mensagem de erro 1";
            const string mensagemDeErro2 = "Mensagem de erro 2";
            var methodResult = new MethodResult();

            // Act
            methodResult.Add(string.Empty, mensagemDeErro1);
            methodResult.Add(string.Empty, mensagemDeErro1, mensagemDeErro2);

            // Arrange
            Assert.Equal(1, methodResult.Failures.Count);
            Assert.Equal(2, methodResult.Failures.First().Messages.Count);
        }

        [Fact]
        public void DadoMensagensIguaisParaMesmaPropriedadeDevePreencherMensagemDaPropriedadeSemRepeticaoDeMensagens()
        {
            // Arrange
            const string mensagemDeErro = "Mensagem de erro";
            var methodResult = new MethodResult();

            // Act
            methodResult.Add(string.Empty, mensagemDeErro, mensagemDeErro);

            // Arrange
            Assert.Equal(1, methodResult.Failures.First().Messages.Count);
        }
    }
}
