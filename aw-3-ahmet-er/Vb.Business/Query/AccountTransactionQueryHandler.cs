using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Query;

public class AccountTransactionQueryHandler :
    IRequestHandler<GetAllAccountTranactionQuery, ApiResponse<List<AccountTransactionResponse>>>,
    IRequestHandler<GetAccountTranactionByIdQuery, ApiResponse<AccountTransactionResponse>>,
    IRequestHandler<GetAccountTranactionByParameterQuery, ApiResponse<List<AccountTransactionResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public AccountTransactionQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAllAccountTranactionQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>()
            .Include(x => x.Account)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<AccountTransaction>, List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
    }

    public async Task<ApiResponse<AccountTransactionResponse>> Handle(GetAccountTranactionByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<AccountTransaction>()
            .Include(x => x.Account)
            .FirstOrDefaultAsync(cancellationToken);

        if (entity is null)
            return new ApiResponse<AccountTransactionResponse>("Record not found");

        var mapped = mapper.Map<AccountTransaction, AccountTransactionResponse>(entity);
        return new ApiResponse<AccountTransactionResponse>(mapped);
    }

    public async Task<ApiResponse<List<AccountTransactionResponse>>> Handle(GetAccountTranactionByParameterQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<AccountTransaction>()
            .Include(x => x.Account)
            .Where(x => 
            x.ReferenceNumber == request.ReferenceNumber ||
            x.TransactionDate >= request.MinTransactionDate ||
            x.TransactionDate <= request.MaxTransactionDate ||
            x.Amount >= request.MinAmount ||
            x.Amount <= request.MaxAmount ||
            x.TransferType == request.TransferType
            ).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<AccountTransaction>, List<AccountTransactionResponse>>(list);
        return new ApiResponse<List<AccountTransactionResponse>>(mappedList);
    }
}
