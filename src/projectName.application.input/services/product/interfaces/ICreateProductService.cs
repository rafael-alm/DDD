using projectName.application.input.services.product.dto;
using projectName.domain.aggregates.product.commands;

namespace projectName.application.input.services.product.interfaces
{
    public interface ICreateProductService
    {
        Task<ReturnProductCreation> Execute(CreateProductCommand data, CancellationToken cancellationToken);
    }
}
