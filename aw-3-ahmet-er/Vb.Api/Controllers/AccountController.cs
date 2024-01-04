using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;

namespace Vb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<AccountResponse>>> Get()
        {
            var operation = new GetAllAccountQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<AccountResponse>> Get(int id)
        {
            var operation = new GetAccountByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("get-by-parameter")]
        public async Task<ApiResponse<List<AccountResponse>>> Get(
            [FromQuery] string IBAN,
            [FromQuery] decimal MinBalance,
            [FromQuery] decimal MaxBalance,
            [FromQuery] string CurrentType,
            [FromQuery] string Name,
            [FromQuery] DateTime MinOpenDate,
            [FromQuery] DateTime MaxOpenDate)
        {
            var operation = new GetAccountByParameterQuery(IBAN, MinBalance, MaxBalance, CurrentType, Name, MinOpenDate, MaxOpenDate);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<AccountResponse>> Post([FromBody] AccountRequest account)
        {
            var operation = new CreateAccountCommand(account);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] AccountRequest account)
        {
            var operation = new UpdateAccountCommand(id, account);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAccountCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
