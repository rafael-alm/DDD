namespace productManagement.application.input.services.product.interfaces
{
    public interface IInactivateProductService
    {
        Task Execute(Guid id, CancellationToken cancellationToken);
    }
}
