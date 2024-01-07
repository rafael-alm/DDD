namespace projectName.domain.aggregates.product.commands
{
    public struct ModifyProductCommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public Guid? SupplierId { get; set; }

    }
}
