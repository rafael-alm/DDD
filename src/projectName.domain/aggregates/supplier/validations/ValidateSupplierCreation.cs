using projectName.domain.aggregates.supplier.commands;
using projectName.domain.shared.seedWork.notification;

namespace projectName.domain.aggregates.supplier.validations
{
    internal sealed class ValidateSupplierCreation
    {
        internal static void Execute(CreateSupplierCommand data, INotification notificacao)
        {
            notificacao.AddIfFalse(SupplierRules.DescriptionIsRequired(data.Description), SupplierMessages.DescriptionIsRequired);
            notificacao.AddIfFalse(SupplierRules.DescriptionMustHaveAMaximumOf250Characters(data.Description), SupplierMessages.DescriptionMustHaveAMaximumOf250Characters);
            notificacao.AddIfFalse(SupplierRules.CNPJIsRequired(data.CNPJ.Number), SupplierMessages.CNPJIsRequired);
        }
    }
}