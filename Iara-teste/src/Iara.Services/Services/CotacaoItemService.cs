using AutoMapper;
using Iara.Infra.Repositories.Interfaces;
using Iara.Services.DTOS;
using Iara.Services.Services.Interfaces;

namespace Iara.Services.Services
{
    public class CotacaoItemService : ICotacaoItemService
    {
        private readonly IMapper _mapper;
        private readonly ICotacaoItemRepository _cotacaoItemRepository;

        public CotacaoItemService(IMapper mapper, ICotacaoItemRepository cotacaoRepository)
        {
            _mapper = mapper;
            _cotacaoItemRepository = cotacaoRepository;
        }

        public async Task<CotacaoItemDto> GetAsync(long id)
        {
            var cotacao = await _cotacaoItemRepository.GetAsnyc(id);
            return _mapper.Map<CotacaoItemDto>(cotacao);
        }

        public async Task RemoveAsync(long id)
        {
            await _cotacaoItemRepository.RemoveAsync(id);
        }
    }
}
