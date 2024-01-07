using productManagement.domain.objectValues;

namespace productManagement.domain.aggregates.supplier.commands
{
    public struct CreateSupplierCommand
    {
        public string Description { get; set; }
        public CNPJ CNPJ { get; set; }
    }
}
