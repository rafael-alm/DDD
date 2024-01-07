namespace projectName.domain.aggregates.product.validations
{
    internal sealed class ProductRules
    {
        internal static bool DescriptionIsRequired(string? description)
              => !string.IsNullOrWhiteSpace(description);
        internal static bool DescriptionMustHaveAMaximumOf250Characters(string description)
              => string.IsNullOrEmpty(description) || description.Length <= 250;
        internal static bool ExpirationDateCannotBeLessThanTheManufacturingDate(DateOnly? manufacturingDate, DateOnly? expirationDate)
              => manufacturingDate is null || expirationDate > manufacturingDate;
    }
}
