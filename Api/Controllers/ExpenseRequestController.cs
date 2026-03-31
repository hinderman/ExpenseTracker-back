using Api.Common.Errors;
using Api.Contracts.ExpenseRequest;
using Application.Common.Models;
using Application.ExpenseRequest.Commands;
using Application.ExpenseRequest.Dtos;
using Application.ExpenseRequest.Queries;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class ExpenseRequestController(ISender prmISender) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(Pagination<SummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] Guid? prmStatusId = null, [FromQuery] Guid? prmCategoryId = null,
            [FromQuery] DateTime? prmStartDate = null, [FromQuery] DateTime? prmEndDate = null, [FromQuery] int prmPageNumber = 1, [FromQuery] int prmPageSize = 10, CancellationToken prmCancellationToken = default)
        {
            ErrorOr<Pagination<SummaryDto>> objResult = await prmISender.Send(new GetAll(prmStatusId, prmCategoryId, prmStartDate, prmEndDate, prmPageNumber, prmPageSize), prmCancellationToken);

            return objResult.Match(success => Ok(success), error => ErrorExtensions.Problem(error));
        }

        [HttpGet("{prmId:guid}")]
        [ProducesResponseType(typeof(DetailDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] Guid prmId, CancellationToken prmCancellationToken = default)
        {
            ErrorOr<DetailDto> objResult = await prmISender.Send(new GetById(prmId), prmCancellationToken);

            return objResult.Match(success => Ok(success), error => ErrorExtensions.Problem(error));
        }

        [HttpGet("summary")]
        [ProducesResponseType(typeof(DetailDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Summary(CancellationToken prmCancellationToken = default)
        {
            //ErrorOr<Summary> objResult = await prmISender.Send(new Summary(), prmCancellationToken);

            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromBody] CreateRequestDto prmCreateRequest, CancellationToken prmCancellationToken = default)
        {
            ErrorOr<Unit> objResult = await prmISender.Send(new Create(prmCreateRequest.RequestedById, prmCreateRequest.CategoryId, prmCreateRequest.StatusId,
                prmCreateRequest.CurrencyId, prmCreateRequest.Amount, prmCreateRequest.ExpenseDate, prmCreateRequest.Description), prmCancellationToken);

            return objResult.Match(success => Created(), error => ErrorExtensions.Problem(error));
        }

        [HttpPut("{prmId:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Update([FromRoute] Guid prmId, [FromBody] UpdateRequestDto prmUpdateRequest, CancellationToken prmCancellationToken = default)
        {
            ErrorOr<Unit> objResult = await prmISender.Send(new Update(prmId, prmUpdateRequest.CategoryId, prmUpdateRequest.StatusId, prmUpdateRequest.CurrencyId, prmUpdateRequest.Amount,
                prmUpdateRequest.ExpenseDate, prmUpdateRequest.Description), prmCancellationToken);

            return objResult.Match(success => NoContent(), error => ErrorExtensions.Problem(error));
        }

        [HttpPatch("{prmId:guid}/approve")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Approve([FromRoute] Guid prmId, CancellationToken prmCancellationToken = default)
        {
            //ErrorOr<Unit> objResult = await prmISender.Send(new Update(prmId, prmUpdateRequest.CategoryId, prmUpdateRequest.StatusId, prmUpdateRequest.CurrencyId, prmUpdateRequest.Amount,
            //    prmUpdateRequest.ExpenseDate, prmUpdateRequest.Description), prmCancellationToken);

            return NoContent();
        }

        [HttpPatch("{prmId:guid}/reject")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Reject([FromRoute] Guid prmId, CancellationToken prmCancellationToken = default)
        {
            //ErrorOr<Unit> objResult = await prmISender.Send(new Update(prmId, prmUpdateRequest.CategoryId, prmUpdateRequest.StatusId, prmUpdateRequest.CurrencyId, prmUpdateRequest.Amount,
            //    prmUpdateRequest.ExpenseDate, prmUpdateRequest.Description), prmCancellationToken);

            return NoContent();
        }

        [HttpDelete("{prmId:guid}")]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Delete([FromRoute] Guid prmId, CancellationToken prmCancellationToken = default)
        {
            ErrorOr<Unit> objResult = await prmISender.Send(new Delete(prmId), prmCancellationToken);

            return objResult.Match(success => NoContent(), error => ErrorExtensions.Problem(error));
        }
    }
}
