using projectName.domain.shared.enumeration;

namespace projectName.infra.data.input.entityTypeConfiguration.models
{
    public sealed class ProductModel
    {
        public Guid Id { get; private set; }
        public Guid? SupplierId { get; private set; }
        public int Code { get; private set; }
        public string Description { get; private set; }
        public StatusEntityEnum Status { get; private set; }
        public DateOnly? ManufacturingDate { get; private set; }
        public DateOnly? ExpirationDate { get; private set; }
    }
}
