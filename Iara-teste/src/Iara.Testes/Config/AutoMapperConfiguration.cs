using AutoMapper;
using Iara.Domain.Entities;
using Iara.Services.DTOS;

namespace Iara.Testes.Config
{
    public static class AutoMapperConfiguration
    {
        public static IMapper GetConfiguration()
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cotacao, CotacaoDto>()
                    .ReverseMap();

                cfg.CreateMap<CotacaoItem, CotacaoItemDto>()
                    .ReverseMap();
            });

            return autoMapperConfig.CreateMapper();
        }
    }
}
