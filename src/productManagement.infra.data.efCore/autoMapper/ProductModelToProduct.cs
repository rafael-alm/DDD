using AutoMapper;
using productManagement.domain.aggregates.product;
using productManagement.infra.data.input.entityTypeConfiguration.models;

namespace productManagement.infra.data.input.autoMapper
{
    public class ProductModelToProduct : Profile
    {
        public ProductModelToProduct()
            => CreateMap<ProductModel, Product>();
    }
}
