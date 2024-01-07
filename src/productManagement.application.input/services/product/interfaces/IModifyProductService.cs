using productManagement.domain.aggregates.product.commands;

namespace productManagement.application.input.services.product.interfaces
{
    public interface IModifyProductService
    {
        Task Execute(ModifyProductCommand data, CancellationToken cancellationToken);
    }
}
