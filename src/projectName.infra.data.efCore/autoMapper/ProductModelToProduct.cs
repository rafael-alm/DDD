using AutoMapper;
using projectName.domain.aggregates.product;
using projectName.infra.data.input.entityTypeConfiguration.models;

namespace projectName.infra.data.input.autoMapper
{
    public class ProductModelToProduct : Profile
    {
        public ProductModelToProduct()
            => CreateMap<ProductModel, Product>();
    }
}
