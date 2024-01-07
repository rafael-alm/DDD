using projectName.domain.objectValues;

namespace projectName.infra.data.input.entityTypeConfiguration.models
{
    public sealed class SupplierModel
    {
        public Guid Id { get; private set; }
        public int Code { get; init; }
        public string Description { get; private set; }
        public CNPJ CNPJ { get; private set; }
    }
}
