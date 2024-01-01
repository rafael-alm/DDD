using productManagement.application.output.dto.product;
using productManagement.application.output.seedWork;

namespace productManagement.application.output.interfaces
{
    public interface IReadProductRepository
    {
        Task<ProductDTO> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
        Task<ProductDTO> GetByCodeAsync(int code, CancellationToken cancellationToken);
        Task<IPaging<ProductForPagingDTO>> ResearchAsync(ProductPaginationFilter filter, CancellationToken cancellationToken);
    }
}
