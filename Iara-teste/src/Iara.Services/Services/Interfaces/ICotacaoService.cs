using Iara.Services.DTOS;

namespace Iara.Services.Services.Interfaces
{
    public interface ICotacaoService
    {
        Task<CotacaoDto> CreateAsync(CotacaoDto cotacaoDto);
        Task<CotacaoDto> UpdateAsync(CotacaoDto cotacaoDto);
        Task RemoveAsync(long id);
        Task<CotacaoDto> GetAsync(long id);
        Task<IList<CotacaoDto>> GetAllAsync();
        Task<IList<CotacaoDto>> SearchByNameAsync(string name);
    }
}
