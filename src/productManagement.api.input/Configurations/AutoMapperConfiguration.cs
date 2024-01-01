using productManagement.infra.data.input.autoMapper;

namespace productManagement.api.input.Configurations
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
            => services.AddAutoMapper(typeof(ProductToProductModel), 
                                      typeof(ProductModelToProduct),
                                      typeof(SupplierModelToSupplier), 
                                      typeof(SupplierToSupplierModel));
    }
}
