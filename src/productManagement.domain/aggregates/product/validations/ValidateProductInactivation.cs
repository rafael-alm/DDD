using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.product.validations
{
    internal sealed class ValidateProductInactivation
    {
        internal static void Execute(StatusEntityEnum currentStatus, INotification notificacao)
            => notificacao.AddIfFalse(currentStatus == StatusEntityEnum.Active, ProductMessages.ProductIsAlreadyInactive);
    }
}