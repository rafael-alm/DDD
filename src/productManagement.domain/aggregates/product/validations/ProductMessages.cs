using productManagement.domain.shared.seedWork.notification;

namespace productManagement.domain.aggregates.product.validations
{
    public sealed class ProductMessages : IMessageNotification
    {
        private ProductMessages(string code, string text)
        {
            Code = code;
            Text = text;
        }

        public string Code { get; init; }
        public string Text { get; init; }

        public static readonly ProductMessages
            ProductIsAlreadyInactive = new ProductMessages("ProductIsAlreadyInactive", "Produto já está inativo."),
            DescriptionIsRequired = new ProductMessages("DescriptionIsRequired", "Desrição é obrigatorio."),
            DescriptionMustHaveAMaximumOf250Characters = new ProductMessages("DescriptionMustHaveAMaximumOf250Characters", "Descrição deve ter no máximo 250 caracteres"),
            ExpirationDateCannotBeLessThanTheManufacturingDate = new ProductMessages("ExpirationDateCannotBeLessThanTheManufacturingDate", "Data de validade não pode ser menor igual a data de fabricação");
    }
}
