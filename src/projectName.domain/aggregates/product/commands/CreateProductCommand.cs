namespace projectName.domain.aggregates.product.commands
{
    public struct CreateProductCommand
    {
        public string Description { get; set; }
        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public Guid? SupplierId { get; set; }
    }
}
