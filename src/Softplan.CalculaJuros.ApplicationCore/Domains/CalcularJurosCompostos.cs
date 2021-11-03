using System;

namespace Softplan.CalculaJuros.ApplicationCore.Domains
{
    public class CalcularJurosCompostos
    {
        public decimal ValorInicial { get; }
        public int TempoMeses { get; }
        public decimal TaxaJuros { get; }
        public decimal ValorCalculado { get; }

        public CalcularJurosCompostos(decimal valorInicial, int meses, decimal taxa)
        {
            ValorInicial = valorInicial;
            TempoMeses = meses;
            TaxaJuros = taxa;
            ValorCalculado = valorInicial * (decimal)Math.Pow(1 + (double)taxa, meses);
        }

        public string GetTruncatedValue(int decimalPlaces)
        {
            var fator = Convert.ToDecimal(0.5 / Math.Pow(10, decimalPlaces));
            var result = Math.Round(ValorCalculado >= 0 ? ValorCalculado - fator : ValorCalculado + fator, decimalPlaces);
            return result.ToString();
        }
    }
}
