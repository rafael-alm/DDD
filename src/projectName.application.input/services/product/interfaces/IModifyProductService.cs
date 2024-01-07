using projectName.domain.aggregates.product.commands;

namespace projectName.application.input.services.product.interfaces
{
    public interface IModifyProductService
    {
        Task Execute(ModifyProductCommand data, CancellationToken cancellationToken);
    }
}
