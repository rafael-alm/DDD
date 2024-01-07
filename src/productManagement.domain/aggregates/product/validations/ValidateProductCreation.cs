using productManagement.domain.aggregates.product.commands;
using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.product.validations
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