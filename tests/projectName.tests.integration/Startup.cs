using Microsoft.Extensions.DependencyInjection;
using projectName.application.input.seedWork.repository;
using projectName.infra.data.input;
using projectName.infra.data.input.autoMapper;
using projectName.tests.integration.common;

namespace projectName.tests.integration
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(ProductToProductModel));
            services.AddDbContext<TestContextProductManagement>();
            services.AddScoped<IDbContext, UnitOfWork>();
        }
    }
}
