using AutoMapper;
using projectName.domain.aggregates.product;
using projectName.infra.data.input.entityTypeConfiguration.models;

namespace projectName.infra.data.input.autoMapper
{
    public class SupplierToSupplierModel : Profile
    {
        public SupplierToSupplierModel()
            => CreateMap<Supplier, SupplierModel>();
    }
}
