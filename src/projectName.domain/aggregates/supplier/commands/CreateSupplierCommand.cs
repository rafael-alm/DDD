using projectName.domain.objectValues;

namespace projectName.domain.aggregates.supplier.commands
{
    public struct CreateSupplierCommand
    {
        public string Description { get; set; }
        public CNPJ CNPJ { get; set; }
    }
}
