using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;

namespace Vb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMediator mediator;

        public AddressController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<AddressResponse>>> Get()
        {
            var operation = new GetAllAddressQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<AddressResponse>> Get(int id)
        {
            var operation = new GetAddressByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("get-by-parameter")]
        public async Task<ApiResponse<List<AddressResponse>>> Get(
            [FromQuery] string Country,
            [FromQuery] string City,
            [FromQuery] string County,
            [FromQuery] string PostalCode)
        {
            var operation = new GetAddressByParameterQuery(Country, City, County, PostalCode);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest address)
        {
            var operation = new CreateAddressCommand(address);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest address)
        {
            var operation = new UpdateAddressCommand(id, address);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAddressCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
