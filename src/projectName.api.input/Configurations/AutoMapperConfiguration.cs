using projectName.infra.data.input.autoMapper;

namespace projectName.api.input.Configurations
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
