using Microsoft.AspNetCore.Mvc;
using projectName.application.input.services.product.dto;
using projectName.application.input.services.product.interfaces;
using projectName.domain.aggregates.product.commands;
using ProductManagement.Api.Models;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(typeof(ApiResponse<ReturnProductCreation>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromServices] ICreateProductService service, [FromBody] CreateProductCommand data, CancellationToken cancellationToken)
        {
            var output = await service.Execute(data, cancellationToken);

            return CreatedAtAction(
                nameof(Modify),
                new { output.Id },
                new ApiResponse<ReturnProductCreation>(output));
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Modify([FromServices] IModifyProductService modifyProductService, [FromBody] ModifyProductCommand dados, CancellationToken cancellationToken)
        {
            await modifyProductService.Execute(dados, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> DeleteById([FromRoute] Guid id, [FromServices] IInactivateProductService modifyProductService, CancellationToken cancellationToken)
        {
            await modifyProductService.Execute(id, cancellationToken);

            return NoContent();
        }
    }
}