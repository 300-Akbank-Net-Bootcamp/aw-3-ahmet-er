using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;

namespace Vb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<CustomerResponse>>> Get()
        {
            var operation = new GetAllCustomerQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<CustomerResponse>> Get(int id)
        {
            var operation = new GetCustomerByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("get-by-parameter")]
        public async Task<ApiResponse<List<CustomerResponse>>> Get(
            [FromQuery] string FirstName,
            [FromQuery] string LastName,
            [FromQuery] string IdentityNumber)
        {
            var operation = new GetCustomerByParameterQuery(FirstName, LastName, IdentityNumber);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<CustomerResponse>> Post([FromBody] CustomerRequest customer)
        {
            var operation = new CreateCustomerCommand(customer);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] CustomerRequest customer)
        {
            var operation = new UpdateCustomerCommand(id, customer);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteCustomerCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
