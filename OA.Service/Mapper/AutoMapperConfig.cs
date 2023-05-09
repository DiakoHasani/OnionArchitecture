using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace OA.Service.Mapper;

internal class AutoMapperConfig
{
    public void Config(IServiceCollection services)
    {
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);
    }
}