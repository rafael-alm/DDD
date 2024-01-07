using projectName.domain.objectValues;
using System.Text;

namespace projectName.domain.aggregates.supplier
{
    public interface ISupplierRepository
    {
        Task<bool> CNPJHasAlreadyBeenNotifiedToAnotherSupplier(CNPJ cnpj, Guid? exceptSupplierWithId = default);
    }
}
