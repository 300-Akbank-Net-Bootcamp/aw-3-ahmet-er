using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Vb.Base.Response;
using Vb.Business.Cqrs;
using Vb.Data;
using Vb.Data.Entity;
using Vb.Schema;

namespace Vb.Business.Query;

public class AccountQueryHandler :
    IRequestHandler<GetAllAccountQuery, ApiResponse<List<AccountResponse>>>,
    IRequestHandler<GetAccountByIdQuery, ApiResponse<AccountResponse>>,
    IRequestHandler<GetAccountByParameterQuery, ApiResponse<List<AccountResponse>>>
{
    private readonly VbDbContext dbContext;
    private readonly IMapper mapper;

    public AccountQueryHandler(VbDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAllAccountQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>()
            .Include(x => x.Customer)
            .Include(x => x.AccountTransactions)
            .Include(x => x.EftTransactions)
            .ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }

    public async Task<ApiResponse<AccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await dbContext.Set<Account>()
            .Include(x => x.Customer)
            .Include(x => x.AccountTransactions)
            .Include(x => x.EftTransactions)
            .FirstOrDefaultAsync(x => x.AccountNumber == request.Id, cancellationToken);

        if (entity is null)
            return new ApiResponse<AccountResponse>("Record not found");

        var mapped = mapper.Map<Account, AccountResponse>(entity);
        return new ApiResponse<AccountResponse>(mapped);
    }

    public async Task<ApiResponse<List<AccountResponse>>> Handle(GetAccountByParameterQuery request, CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Account>()
            .Include(x => x.Customer)
            .Include(x => x.AccountTransactions)
            .Include(x => x.EftTransactions)
            .Where(x => 
            x.IBAN == request.IBAN ||
            x.Balance >= request.MinBalance ||
            x.Balance <= request.MaxBalance ||
            x.CurrencyType == request.CurrentType ||
            x.Name == request.Name ||
            x.OpenDate >= request.MinOpenDate ||
            x.OpenDate <= request.MaxOpenDate
            ).ToListAsync(cancellationToken);

        var mappedList = mapper.Map<List<Account>, List<AccountResponse>>(list);
        return new ApiResponse<List<AccountResponse>>(mappedList);
    }
}
