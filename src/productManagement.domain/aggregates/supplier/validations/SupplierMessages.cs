using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.supplier.validations
{
    internal class SupplierMessages : IMessageNotification
    {
        public SupplierMessages(string code, string text)
        {
            Code = code;
            Text = text;
        }

        public string Code { get; init; }
        public string Text { get; init; }

        public static readonly SupplierMessages
            CNPJHasAlreadyBeenInformed = new SupplierMessages("CNPJHasAlreadyBeenInformed", "CNPJ já foi informado para outro fornecedor."),
            DescriptionIsRequired = new SupplierMessages("DescriptionIsRequired", "Desrição é obrigatorio."),
            DescriptionMustHaveAMaximumOf250Characters = new SupplierMessages("DescriptionMustHaveAMaximumOf250Characters", "Descrição deve ter no máximo 250 caracteres"),
            CNPJIsRequired = new SupplierMessages("CNPJIsRequired", "CNPJ é obrigatorio."),
            CNPJMustBeValid = new SupplierMessages("CNPJMustBeValid", "CNPJ deve ser válido.");
    }
}
