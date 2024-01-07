using AutoMapper;
using projectName.domain.aggregates.product;
using projectName.infra.data.input.entityTypeConfiguration.models;

namespace projectName.infra.data.input.autoMapper
{
    public class SupplierModelToSupplier : Profile
    {
        public SupplierModelToSupplier()
            => CreateMap<SupplierModel, Supplier>();
    }
}
