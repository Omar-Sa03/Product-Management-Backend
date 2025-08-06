using Application.Features.CustomerFeatures.Validators;
using Application.Features.TestFeature.Commands;
using Application.Features.TestFeature.Queries;
using Application.Features.TestFeature.Validators;
using Application.Setting;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestControllerNew : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestControllerNew(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> Add(AddTestCommandNew cmd)
        {
            try
            {
                ResponseHttp AddCustomerResult;
                AddTestCommandNewValidator validator = new();

                AddCustomerResult = validator.Validate(new ValidationContext<AddTestCommandNew>(cmd));

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
        public async Task<ActionResult> Update([FromBody] UpdateTestCommandNew cmd)
        {
            try
            {
                ResponseHttp updateCustomerResult;
                UpdateTestCommandNewValidator validator = new();

                updateCustomerResult = validator.Validate(new ValidationContext<UpdateTestCommandNew>(cmd));

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
            var result = await _mediator.Send(new DeleteTestCommandNew(id));
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(Guid id)
        {
            GetTesByIdNewQuery qr = new(id);
            var result = await _mediator.Send(qr);

            return Ok(result);
        }
        [HttpGet("")]
        public async Task<ActionResult> Get(int? pageNumber, int? pageSize)
        {
            var result = await _mediator.Send(new GetAllTestNewQuery(pageNumber, pageSize));

            return Ok(result);
        }

    }
}
