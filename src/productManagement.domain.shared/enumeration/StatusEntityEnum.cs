using productManagement.domain.shared.seedWork.enumeration;

namespace productManagement.domain.shared.enumeration
{
    public class StatusEntityEnum : Enumeration
    {
        public StatusEntityEnum(int id, string name) : base(id, name) { }

        public static readonly StatusEntityEnum
            Inactive = new StatusEntityEnum(0, "Inativo"),
            Active = new StatusEntityEnum(1, "Ativo");
    }
}
