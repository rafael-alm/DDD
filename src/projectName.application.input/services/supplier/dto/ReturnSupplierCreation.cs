namespace projectName.application.input.services.supplier.dto
{
    public readonly struct ReturnSupplierCreation
    {
        public ReturnSupplierCreation(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
