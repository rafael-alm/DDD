using productManagement.domain.objectValues;

namespace productManagement.domain.aggregates.supplier.commands
{
    public struct ModifySupplierCommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public CNPJ CNPJ { get; set; }
    }
}
