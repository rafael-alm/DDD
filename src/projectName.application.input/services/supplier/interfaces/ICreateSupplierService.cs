using projectName.application.input.services.product.dto;
using projectName.application.input.services.supplier.dto;
using projectName.domain.aggregates.supplier.commands;

namespace projectName.application.input.services.supplier.interfaces
{
    public interface ICreateSupplierService
    {
        Task<ReturnSupplierCreation> Execute(CreateSupplierCommand data, CancellationToken cancellationToken);
    }
}
