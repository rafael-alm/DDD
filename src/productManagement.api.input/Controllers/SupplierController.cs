using Microsoft.AspNetCore.Mvc;
using productManagement.application.input.services.supplier.dto;
using productManagement.application.input.services.supplier.interfaces;
using productManagement.domain.aggregates.supplier.commands;
using ProductManagement.Api.Models;

namespace ProductManagement.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupplierController : ControllerBase
    {
        [HttpPost()]
        [ProducesResponseType(typeof(ApiResponse<ReturnSupplierCreation>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Create([FromServices] ICreateSupplierService service, [FromBody] CreateSupplierCommand data, CancellationToken cancellationToken)
        {
            var output = await service.Execute(data, cancellationToken);

            return CreatedAtAction(
                nameof(Modify),
                new { output.Id },
                new ApiResponse<ReturnSupplierCreation>(output));
        }

        [HttpPut()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Modify([FromServices] IModifySupplierService modifySupplierService, [FromBody] ModifySupplierCommand dados, CancellationToken cancellationToken)
        {
            await modifySupplierService.Execute(dados, cancellationToken);

            return Ok();
        }
    }
}