using System.ComponentModel.DataAnnotations;

namespace Iara.Services.DTOS
{
    public class CotacaoDto
    {
        public long Id { get; set; }
        [Required]
        public string CNPJComprador { get; set; }
        [Required]
        public string CNPJFornecedor { get; set; }
        [Required]
        public string NumeroCotacao { get; set; }
        [Required]
        public DateTime DataCotacao { get; set; }
        [Required]
        public DateTime DataEntregaCotacao { get; set; }
        [Required]
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string UF { get; set; }
        public string Observacao { get; set; }
        public ICollection<CotacaoItemDto> CotacaoItem { get; set; }

        public CotacaoDto()
        {

        }
    }


}
