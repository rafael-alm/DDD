using Microsoft.Extensions.DependencyInjection;
using productManagement.application.input.seedWork.repository;
using productManagement.infra.data.input;
using productManagement.infra.data.input.autoMapper;
using productManagement.tests.integration.common;

namespace productManagement.tests.integration
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
