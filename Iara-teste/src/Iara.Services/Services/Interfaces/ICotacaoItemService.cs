using Iara.Services.DTOS;

namespace Iara.Services.Services.Interfaces
{
    public interface ICotacaoItemService
    {
        Task RemoveAsync(long id);
        Task<CotacaoItemDto> GetAsync(long id);
    }
}
