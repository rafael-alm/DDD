using projectName.domain.aggregates.supplier.commands;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.supplier.validations
{
    internal sealed class ValidateSupplierModification
    {
        internal static void Execute(ModifySupplierCommand data, INotification notificacao)
        {
            var descriptionIsRequired = SupplierRules.DescriptionIsRequired(data.Description);

            notificacao.AddIfFalse(descriptionIsRequired, SupplierMessages.DescriptionIsRequired);

            if (descriptionIsRequired)
                notificacao.AddIfFalse(SupplierRules.DescriptionMustHaveAMaximumOf250Characters(data.Description), SupplierMessages.DescriptionMustHaveAMaximumOf250Characters);

            var cnpjIsRequired = SupplierRules.CNPJIsRequired(data.CNPJ.Number);
            notificacao.AddIfFalse(cnpjIsRequired, SupplierMessages.CNPJIsRequired);

            if (cnpjIsRequired)
                notificacao.AddIfFalse(SupplierRules.CNPJMustBeValid(data.CNPJ.Number), SupplierMessages.CNPJMustBeValid);
        }
    }
}