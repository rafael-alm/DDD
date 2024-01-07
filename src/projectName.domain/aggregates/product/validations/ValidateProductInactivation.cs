using projectName.domain.shared.enumeration;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product.validations
{
    internal sealed class ValidateProductInactivation
    {
        internal static void Execute(StatusEntityEnum currentStatus, INotification notificacao)
            => notificacao.AddIfFalse(currentStatus == StatusEntityEnum.Active, ProductMessages.ProductIsAlreadyInactive);
    }
}