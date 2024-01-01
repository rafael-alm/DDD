using productManagement.domain.objectValues;

namespace productManagement.domain.aggregates.supplier.validations
{
    internal sealed class SupplierRules
    {
        internal static bool DescriptionIsRequired(string? description)
              => !string.IsNullOrWhiteSpace(description);
        internal static bool DescriptionMustHaveAMaximumOf250Characters(string description)
              => string.IsNullOrEmpty(description) || description.Length <= 250;
        internal static bool CNPJIsRequired(string? cnpj)
              => !string.IsNullOrWhiteSpace(cnpj);
        internal static bool CNPJMustBeValid(string? cnpj)
              => CNPJ.IsValid(cnpj);
    }
}
