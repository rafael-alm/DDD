using productManagement.domain.aggregates.supplier.commands;

namespace productManagement.application.input.services.supplier.interfaces
{
    public interface IModifySupplierService
    {
        Task Execute(ModifySupplierCommand data, CancellationToken cancellationToken);
    }
}
