using projectName.application.output.dto.product;
using projectName.application.output.seedWork;

namespace projectName.application.output.interfaces
{
    public interface IReadProductRepository
    {
        Task<ProductDTO> GetByIdAsync(Guid productId, CancellationToken cancellationToken);
        Task<ProductDTO> GetByCodeAsync(int code, CancellationToken cancellationToken);
        Task<IPaging<ProductForPagingDTO>> ResearchAsync(ProductPaginationFilter filter, CancellationToken cancellationToken);
    }
}
