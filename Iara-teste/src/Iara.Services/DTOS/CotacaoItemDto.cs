using System.ComponentModel.DataAnnotations;

namespace Iara.Services.DTOS
{
    public class CotacaoItemDto
    {
        public long Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string NumeroItem { get; set; }
        public double Preco { get; set; }
        [Required]
        public int Quantidade { get; set; }
        public string Marca { get; set; }
        public string Unidade { get; set; }

        public CotacaoItemDto()
        {

        }
    }
}