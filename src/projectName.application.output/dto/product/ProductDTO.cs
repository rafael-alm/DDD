namespace projectName.application.output.dto.product
{
    public struct ProductDTO
    {
        public string Id { get; set; }
        public string? SupplierId { get; set; }
        public int Code { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
    }
}
