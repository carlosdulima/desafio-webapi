using System;
using System.ComponentModel.DataAnnotations;

namespace Softplan.CalculaJuros.Api.Requests
{
    public class CalculaJurosRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public decimal ValorInicial { get; set; }

        [Required]
        [Range(0, short.MaxValue)]
        public int Meses { get; set; }
    }
}
