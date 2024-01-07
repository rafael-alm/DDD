using projectName.domain.aggregates.product.commands;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product.validations
{
    internal sealed class ValidateProductCreation
    {
        internal static void Execute(CreateProductCommand data, INotification notificacao)
        {
            notificacao.AddIfFalse(ProductRules.DescriptionIsRequired(data.Description), ProductMessages.DescriptionIsRequired);
            notificacao.AddIfFalse(ProductRules.DescriptionMustHaveAMaximumOf250Characters(data.Description), ProductMessages.DescriptionMustHaveAMaximumOf250Characters);
            notificacao.AddIfFalse(ProductRules.ExpirationDateCannotBeLessThanTheManufacturingDate(data.ManufacturingDate, data.ExpirationDate), ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate);
        }
    }
}