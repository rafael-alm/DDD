using Microsoft.AspNetCore.Mvc;
using productManagement.application.output.dto.product;
using productManagement.application.output.interfaces;
using productManagement.application.output.seedWork;
using ProductManagement.Api.Output.Models;

namespace ProductManagement.Api.Output.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ApiResponse<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, [FromServices] IReadProductRepository repository, CancellationToken cancellationToken)
        {
            var output = await repository.GetByIdAsync(id, cancellationToken);
            return Ok(new ApiResponse<ProductDTO>(output));
        }

        [HttpGet("{code:int}")]
        [ProducesResponseType(typeof(ApiResponse<ProductDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] int code, [FromServices] IReadProductRepository repository, CancellationToken cancellationToken)
        {
            var output = await repository.GetByCodeAsync(code, cancellationToken);
            return Ok(new ApiResponse<ProductDTO>(output));
        }

        [HttpGet()]
        [ProducesResponseType(typeof(ApiResponse<IPaging<ProductForPagingDTO>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Research([FromServices] IReadProductRepository repository, CancellationToken cancellationToken,
                                                  [FromQuery] int page = 1, [FromQuery] int recordsPerPage = 10, [FromQuery] string? supplierDescription = default, 
                                                  [FromQuery] int? code = default, [FromQuery] string? description = default, [FromQuery] int? status = default, 
                                                  [FromQuery] DateTime? manufacturingDate = default, [FromQuery] DateTime? expirationDate = default)
        {
            var filter = new ProductPaginationFilter
            {
                Code = code,
                Description = description,
                ExpirationDate = expirationDate,
                ManufacturingDate = manufacturingDate,
                Page = page,
                RecordsPerPage = recordsPerPage,
                Status = status,
                SupplierDescription = supplierDescription
            };

            var output = await repository.ResearchAsync(filter, cancellationToken);
            return Ok(new ApiResponse<IPaging<ProductForPagingDTO>>(output));
        }
    }
}