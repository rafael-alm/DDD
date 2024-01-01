using AutoMapper;
using productManagement.domain.aggregates.product;
using productManagement.infra.data.input.entityTypeConfiguration.models;

namespace productManagement.infra.data.input.autoMapper
{
    public class ProductToProductModel : Profile
    {
        public ProductToProductModel()
            => CreateMap<Product, ProductModel>();
    }
}
