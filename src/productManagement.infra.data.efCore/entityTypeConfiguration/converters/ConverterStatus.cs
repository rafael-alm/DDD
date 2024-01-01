using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.enumeration;

namespace productManagement.infra.data.input.entityTypeConfiguration.converters
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
