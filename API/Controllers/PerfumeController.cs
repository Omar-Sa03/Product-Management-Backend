using Application.Features.PerfumeFeatures.UpdateValidators;
using Application.Features.PerfumeFeature.Commands;
using Application.Features.PerfumeFeature.Queries;
using Application.Features.PerfumeFeatures.AddValidators;
using Application.Setting;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/Perfume")]
    [ApiController]
    public class PerfumeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PerfumeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddPerfumeCommand cmd)
        {
            try
            {
                ResponseHttp AddCustomerResult;
                AddPerfumeCommandValidator validator = new();

                AddCustomerResult = validator.Validate(new ValidationContext<AddPerfumeCommand>(cmd));

                if (AddCustomerResult.Status == StatusCodes.Status400BadRequest)
                {
                    return BadRequest(AddCustomerResult);
                }

                AddCustomerResult = await _mediator.Send(cmd);

                return Ok(AddCustomerResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("")]
        public async Task<ActionResult> Update([FromBody] UpdatePerfumeCommand cmd)
        {
            try
            {
                ResponseHttp updateCustomerResult;
                UpdatePerfumeCommandValidator validator = new();

                updateCustomerResult = validator.Validate(new ValidationContext<UpdatePerfumeCommand>(cmd));

                if (updateCustomerResult.Status == StatusCodes.Status400BadRequest)
                {
                    return BadRequest(updateCustomerResult);
                }

                updateCustomerResult = await _mediator.Send(cmd);

                return Ok(updateCustomerResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeletePerfumeCommand(id));
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            GetPerfumeByIdNewQuery qr = new(id);
            var result = await _mediator.Send(qr);

            return Ok(result);
        }
        [HttpGet("")]
        public async Task<ActionResult> Get(int? pageNumber, int? pageSize)
        {
            var result = await _mediator.Send(new GetAllPerfumeQuery(pageNumber, pageSize));

            return Ok(result);
        }

    }
}
