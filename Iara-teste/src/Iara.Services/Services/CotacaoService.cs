using AutoMapper;
using Iara.Core.Utils;
using Iara.Domain.Entities;
using Iara.Infra.Repositories.Interfaces;
using Iara.Services.DTOS;
using Iara.Services.Services.Interfaces;
using System.Net;
using System.Text.Json;

namespace Iara.Services.Services
{
    public class CotacaoService : ICotacaoService
    {
        private readonly IMapper _mapper;
        private readonly ICotacaoRepository _cotacaoRepository;
        private readonly ICotacaoItemRepository _cotacaoItemRepository;

        public CotacaoService(IMapper mapper, ICotacaoRepository cotacaoRepository, ICotacaoItemRepository cotacaoItemRepository)
        {
            _mapper = mapper;
            _cotacaoRepository = cotacaoRepository;
            _cotacaoItemRepository = cotacaoItemRepository;
        }

        public async Task<CotacaoDto> CreateAsync(CotacaoDto cotacaoDto)
        {
            var cotacao = _mapper.Map<Cotacao>(cotacaoDto);

            if (!cotacao.IsValid) return null;

            if (String.IsNullOrEmpty(cotacao.Logradouro) || String.IsNullOrEmpty(cotacao.Bairro) || String.IsNullOrEmpty(cotacao.UF))
            {
                try
                {
                    HttpClient client = new HttpClient();
                    string url = $"https://viacep.com.br/ws/{cotacao.CEP.Replace("-", "").Trim()}/json/";
                    var res = await client.GetAsync(url);
                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        var address = JsonSerializer.Deserialize<Address>(res.Content.ReadAsStream());
                        cotacao.Logradouro = address.logradouro;
                        cotacao.UF = address.uf;
                        cotacao.Bairro = address.bairro;
                    }
                }
                catch
                {
                    throw;
                }
            }

            var cotacaoCreated = await _cotacaoRepository.CreateAsync(cotacao);
            return _mapper.Map<CotacaoDto>(cotacaoCreated);
        }

        public async Task<IList<CotacaoDto>> GetAllAsync()
        {
            var results = await _cotacaoRepository.GetAllAsync();
            return _mapper.Map<List<CotacaoDto>>(results);
        }

        public async Task<CotacaoDto> GetAsync(long id)
        {
            var cotacao = await _cotacaoRepository.GetAsnyc(id);
            return _mapper.Map<CotacaoDto>(cotacao);
        }

        public async Task RemoveAsync(long id)
        {
            await _cotacaoRepository.RemoveAsync(id);
        }

        public async Task<IList<CotacaoDto>> SearchByNameAsync(string name)
        {
            var results = await _cotacaoRepository.SearchAsync(x => x.CNPJComprador == name || x.CNPJFornecedor == name);
            return _mapper.Map<List<CotacaoDto>>(results);
        }

        public async Task<CotacaoDto> UpdateAsync(CotacaoDto cotacaoDto)
        {
            var cotacao = _mapper.Map<Cotacao>(cotacaoDto);
            if (!cotacao.IsValid) return null;
            var cotacaoUpdated = await _cotacaoRepository.UpdateAsync(cotacao);
            return _mapper.Map<CotacaoDto>(cotacaoUpdated);
        }
    }
}
