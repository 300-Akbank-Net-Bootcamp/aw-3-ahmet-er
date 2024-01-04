using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;

namespace Vb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountTransactionController : ControllerBase
    {
        private readonly IMediator mediator;

        public AccountTransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> Get()
        {
            var operation = new GetAllAccountTranactionQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<AccountTransactionResponse>> Get(int id)
        {
            var operation = new GetAccountTranactionByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("get-by-parameter")]
        public async Task<ApiResponse<List<AccountTransactionResponse>>> Get(
            [FromQuery] string ReferenceNumber,
            [FromQuery] DateTime MinTransactionDate,
            [FromQuery] DateTime MaxTransactionDate,
            [FromQuery] decimal MinAmount,
            [FromQuery] decimal MaxAmount,
            [FromQuery] string TransferType)
        {
            var operation = new GetAccountTranactionByParameterQuery(ReferenceNumber, MinTransactionDate, MaxTransactionDate, MinAmount, MaxAmount, TransferType);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPost]
        public async Task<ApiResponse<AccountTransactionResponse>> Post([FromBody] AccountTransactionRequest accountTransaction)
        {
            var operation = new CreateAccountTransactionCommand(accountTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] AccountTransactionRequest accountTransaction)
        {
            var operation = new UpdateAccountTransactionCommand(id, accountTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteAccountTransactionCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}
