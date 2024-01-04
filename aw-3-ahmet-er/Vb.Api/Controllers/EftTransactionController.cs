using MediatR;
using Microsoft.AspNetCore.Mvc;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Schema;

namespace Vb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EftTransactionController : ControllerBase
    {
        private readonly IMediator mediator;

        public EftTransactionController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<EftTransactionResponse>>> Get()
        {
            var operation = new GetAllEtfTransactionQuery();
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ApiResponse<EftTransactionResponse>> Get(int id)
        {
            var operation = new GetEftTransactionByIdQuery(id);
            var result = await mediator.Send(operation);
            return result;
        }       
    
        [HttpGet("get-by-parameter")]
        public async Task<ApiResponse<List<EftTransactionResponse>>> Get(
            [FromQuery] int AccountId,
            [FromQuery] string ReferenceNumber,
            [FromQuery] DateTime MinTransactionDate,
            [FromQuery] DateTime MaxTransactionDate,
            [FromQuery] decimal MinAmount,
            [FromQuery] decimal MaxAmount,
            [FromQuery] string SenderAccount,
            [FromQuery] string SenderIban,
            [FromQuery] string SenderName)
        {
            var operation = new GetEtfTransactionByParameterQuery(AccountId, ReferenceNumber, MinTransactionDate, MaxTransactionDate, MinAmount, MaxAmount, SenderAccount, SenderIban, SenderName);
            var result = await mediator.Send(operation);
            return result;
        }
        [HttpPost]
        public async Task<ApiResponse<EftTransactionResponse>> Post([FromBody] EftTransactionRequest eftTransaction)
        {
            var operation = new CreateEftTransactionCommand(eftTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<ApiResponse> Put(int id, [FromBody] EftTransactionRequest eftTransaction)
        {
            var operation = new UpdateEftTransactionCommand(id, eftTransaction);
            var result = await mediator.Send(operation);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<ApiResponse> Delete(int id)
        {
            var operation = new DeleteEftTransactionCommand(id);
            var result = await mediator.Send(operation);
            return result;
        }
    }
}