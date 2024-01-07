using projectName.domain.aggregates.product.commands;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.product.validations
{
    internal sealed class ValidateProductModification
    {
        internal static void Execute(ModifyProductCommand data, INotification notificacao)
        {
            var descriptionIsRequired = ProductRules.DescriptionIsRequired(data.Description);

            notificacao.AddIfFalse(ProductRules.DescriptionIsRequired(data.Description), ProductMessages.DescriptionIsRequired);

            if (descriptionIsRequired)
                notificacao.AddIfFalse(ProductRules.DescriptionMustHaveAMaximumOf250Characters(data.Description), ProductMessages.DescriptionMustHaveAMaximumOf250Characters);

            notificacao.AddIfFalse(ProductRules.ExpirationDateCannotBeLessThanTheManufacturingDate(data.ManufacturingDate, data.ExpirationDate), ProductMessages.ExpirationDateCannotBeLessThanTheManufacturingDate);
        }
    }
}