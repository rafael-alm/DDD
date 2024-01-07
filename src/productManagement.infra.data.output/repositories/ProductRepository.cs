using productManagement.application.output.dto.product;
using productManagement.application.output.interfaces;
using productManagement.application.output.seedWork;
using productManagement.infra.data.output.seedWork;
using System.Data;

namespace productManagement.infra.data.output.repositories
{
    public sealed class ProductRepository : IReadProductRepository
    {
        private readonly IDbConnection connection;
        private readonly SqlFactory factory;

        public ProductRepository(SqlFactory factory)
        {
            connection = factory.SqlConnection();
            this.factory = factory;
        }

        async Task<ProductDTO> IReadProductRepository.GetByCodeAsync(int code, CancellationToken cancellationToken)
        {
            string query = @"SELECT [Id]
                                  , [SupplierId]
                                  , [Code]
                                  , [Description]
                                  , [Status]
                                  , [ManufacturingDate]
                                  , [ExpirationDate]
                               FROM [dbo].[Product]
                              WHERE [Code] = @Code";

            return await GetRecord<ProductDTO>.SingleOrDefaultAsync(
                factory,
                query,
                new { Code = code },
                x => new ProductDTO
                {
                    Id = x.Id,
                    SupplierId = x.SupplierId,
                    Code = x.Code,
                    Description = x.Description,
                    Status = x.Status,
                    ExpirationDate = x.ExpirationDate is null ? null : DateOnly.FromDateTime(x.ExpirationDate),
                    ManufacturingDate = x.ManufacturingDate is null ? null : DateOnly.FromDateTime(x.ManufacturingDate)
                },
                cancellationToken);
        }

        async Task<ProductDTO> IReadProductRepository.GetByIdAsync(Guid productId, CancellationToken cancellationToken)
        {
            string query = @"SELECT [Id]
                                  , [SupplierId]
                                  , [Code]
                                  , [Description]
                                  , [Status]
                                  , [ManufacturingDate]
                                  , [ExpirationDate]
                               FROM [dbo].[Product]
                              WHERE [Id] = @Id";

            return await GetRecord<ProductDTO>.SingleOrDefaultAsync(
                factory,
                query,
                new { Id = productId.ToString() },
                x => new ProductDTO
                {
                    Id = x.Id,
                    SupplierId = x.SupplierId,
                    Code = x.Code,
                    Description = x.Description,
                    Status = x.Status,
                    ExpirationDate = x.ExpirationDate is null ? null : DateOnly.FromDateTime(x.ExpirationDate),
                    ManufacturingDate = x.ManufacturingDate is null ? null : DateOnly.FromDateTime(x.ManufacturingDate)
                },
                cancellationToken);
        }

        async Task<IPaging<ProductForPagingDTO>> IReadProductRepository.ResearchAsync(ProductPaginationFilter filter, CancellationToken cancellationToken)
        {
            string query = @"SELECT [Product].[Id]
                                  , [Product].[Description]
                                  , [Supplier].[Description] AS SupplierDescription
                                  , [Product].[Code]
                                  , [Product].[Status]
                                  , [Product].[ManufacturingDate]
                                  , [Product].[ExpirationDate]
                               FROM [Product]
                               LEFT JOIN [Supplier] ON [Supplier].[Id] = [Product].[SupplierId]
                              WHERE (@SupplierDescription IS NULL OR [Supplier].[Description] LIKE '%@SupplierDescription%')
                                AND (@Code IS NULL OR [Product].[Code] = @Code)
                                AND (@Description IS NULL OR [Product].[Description] LIKE '%@Description%')
                                AND (@Status IS NULL OR [Status] = @Status)
                                AND (@ManufacturingDate IS NULL OR ([ManufacturingDate] >= @ManufacturingDate AND [ManufacturingDate] <= @ManufacturingDate))
                                AND (@ExpirationDate IS NULL OR ([ExpirationDate] >= @ManufacturingDate AND [ExpirationDate] <= @ExpirationDate))";

            return await Paging<ProductForPagingDTO>.ExecuteAsync(factory, query, "ORDER BY [Product].[Description]", filter, x => new ProductForPagingDTO
            {
                Id = x.Id,
                SupplierDescription = x.SupplierDescription,
                Code = x.Code,
                Description = x.Description,
                Status = x.Status,
                ExpirationDate = x.ExpirationDate is null ? null : DateOnly.FromDateTime(x.ExpirationDate),
                ManufacturingDate = x.ManufacturingDate is null ? null : DateOnly.FromDateTime(x.ManufacturingDate)
            }, cancellationToken);
        }
    }
}
