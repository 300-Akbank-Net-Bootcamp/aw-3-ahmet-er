using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Cqrs;

public record CreateAccountTransactionCommand(AccountTransactionRequest Model) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record UpdateAccountTransactionCommand(int Id, AccountTransactionRequest Model) : IRequest<ApiResponse>;
public record DeleteAccountTransactionCommand(int Id) : IRequest<ApiResponse>;

public record GetAllAccountTranactionQuery() : IRequest<ApiResponse<List<AccountTransactionResponse>>>;
public record GetAccountTranactionByIdQuery(int Id) : IRequest<ApiResponse<AccountTransactionResponse>>;
public record GetAccountTranactionByParameterQuery(string ReferenceNumber, DateTime MinTransactionDate, DateTime MaxTransactionDate, decimal MinAmount, decimal MaxAmount, string TransferType) : IRequest<ApiResponse<List<AccountTransactionResponse>>>;