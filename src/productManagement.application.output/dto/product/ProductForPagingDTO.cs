using productManagement.domain.shared.enumeration;
using productManagement.domain.shared.seedWork.enumeration;

namespace productManagement.application.output.dto.product
{
    public struct ProductForPagingDTO
    {
        public string Id { get; init; }
        public int Code { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public string StatusDescription => Enumeration.GetById<StatusEntityEnum>(Status).Name;
        public DateOnly? ManufacturingDate { get; set; }
        public DateOnly? ExpirationDate { get; set; }
        public string SupplierDescription { get; set; }
    }
}
