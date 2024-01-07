using projectName.domain.aggregates.supplier.commands;

namespace projectName.application.input.services.supplier.interfaces
{
    public interface IModifySupplierService
    {
        Task Execute(ModifySupplierCommand data, CancellationToken cancellationToken);
    }
}
