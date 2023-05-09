using Microsoft.Extensions.DependencyInjection;

namespace OA.Service.Mapper;
public static class AutoMapperExtension
{
    public static void ConfigAutoMapper(this IServiceCollection services)
    {
        new AutoMapperConfig().Config(services);
    }
}