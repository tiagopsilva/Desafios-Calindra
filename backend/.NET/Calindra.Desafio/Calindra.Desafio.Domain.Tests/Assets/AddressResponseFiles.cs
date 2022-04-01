using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Calindra.Desafio.Domain.Tests.Assets
{
    public static class AddressResponseFiles
    {
        public static IReadOnlyDictionary<string, string> AddressesFiles = new ReadOnlyDictionary<string, string>(
            new Dictionary<string, string>
            {
                { "Caixa economica federal, retiro, volta redonda rj", "Resposta_CaixaEconomicaFederal_Retiro_VoltaRedonda_RJ.json" },
                { "Rua 1, Santa Rita de Cássia, barra mansa rj", "Resposta_Rua1_SantaRitaDeCassia_BarraMansa_RJ.json" },
                { "Santa Cruz, Volta Redonda, RJ", "Resposta_SantaCruz_VoltaRedonda_RJ.json" },
                { "Faetec, Santo Agostinho, Volta Redonda, RJ", "Resposta_Faetec_SantoAgostinho_VoltaRedonda_RJ.json" }
            });
    }
}
