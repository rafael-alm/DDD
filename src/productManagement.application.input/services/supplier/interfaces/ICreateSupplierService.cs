using productManagement.application.input.services.product.dto;
using productManagement.application.input.services.supplier.dto;
using productManagement.domain.aggregates.supplier.commands;

namespace productManagement.application.input.services.supplier.interfaces
{
    public interface ICreateSupplierService
    {
        Task<ReturnSupplierCreation> Execute(CreateSupplierCommand data, CancellationToken cancellationToken);
    }
}
