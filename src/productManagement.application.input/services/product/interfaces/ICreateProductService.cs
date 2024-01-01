using productManagement.application.input.services.product.dto;
using productManagement.domain.aggregates.product.commands;

namespace productManagement.application.input.services.product.interfaces
{
    public interface ICreateProductService
    {
        Task<ReturnProductCreation> Execute(CreateProductCommand data, CancellationToken cancellationToken);
    }
}
