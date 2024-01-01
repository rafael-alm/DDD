using ProductManagement.Api.Filters;

namespace ProductManagement.Api.Configurations;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(this IServiceCollection services)
    {
        services
            .AddControllers(options
                => options.Filters.Add(typeof(ApiGlobalExceptionFilter))
            );

        services.AddDocumentation();
        return services;
    }

    private static IServiceCollection AddDocumentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }
}
