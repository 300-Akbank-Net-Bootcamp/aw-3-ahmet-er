using MediatR;
using Vb.Base.Response;
using Vb.Schema;

namespace Vb.Business.Cqrs;

public record CreateEftTransactionCommand(EftTransactionRequest Model) : IRequest<ApiResponse<EftTransactionResponse>>;
public record UpdateEftTransactionCommand(int Id, EftTransactionRequest Model) : IRequest<ApiResponse>;
public record DeleteEftTransactionCommand(int Id) : IRequest<ApiResponse>;

public record GetAllEtfTransactionQuery() : IRequest<ApiResponse<List<EftTransactionResponse>>>;
public record GetEftTransactionByIdQuery(int Id) : IRequest<ApiResponse<EftTransactionResponse>>;
public record GetEtfTransactionByParameterQuery(int AccountId, string ReferenceNumber, DateTime MinTransactionDate, DateTime MaxTransactionDate, decimal MinAmount, decimal MaxAmount, string SenderAccount, string SenderIban, string SenderName) : IRequest<ApiResponse<List<EftTransactionResponse>>>;
