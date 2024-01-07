namespace projectName.application.input.services.product.dto
{
    public readonly struct ReturnProductCreation
    {
        public ReturnProductCreation(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }
}
