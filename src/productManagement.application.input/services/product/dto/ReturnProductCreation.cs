namespace productManagement.application.input.services.product.dto
{
    public readonly struct ReturnProductCreation
    {
        public ReturnProductCreation(Guid id, int code)
        {
            Id = id;
            Code = code;
        }

        public Guid Id { get; }
        public int Code { get; }
    }
}
