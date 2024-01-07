using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using projectName.domain.shared.enumeration;
using projectName.domain.shared.seedWork.enumeration;

namespace projectName.infra.data.input.entityTypeConfiguration.converters
{
    internal class ConverterStatus : ValueConverter<StatusEntityEnum, int>
    {
        public ConverterStatus()
            : base(
                status => status.Id,
                code => Enumeration.GetById<StatusEntityEnum>(code))
        {}
    }
}
