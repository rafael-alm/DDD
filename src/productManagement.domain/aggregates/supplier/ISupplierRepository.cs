using productManagement.domain.objectValues;
using System.Text;

namespace productManagement.domain.aggregates.supplier
{
    public interface ISupplierRepository
    {
        Task<bool> CNPJHasAlreadyBeenNotifiedToAnotherSupplier(CNPJ cnpj, Guid? exceptSupplierWithId = default);
    }
}
