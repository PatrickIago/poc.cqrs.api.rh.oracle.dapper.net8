using AutoMapper;
using Dapper;
using Poc.Contract.Query.Employee.Interfaces;
using Poc.Contract.Query.Employee.ViewModels;
using Poc.Domain.Entities.Employee;
using Poc.Oracle.Context;
using Poc.Oracle.SQL;
using System.Data;
namespace Poc.Oracle.QueryStore;
public class EmployeeReadOnlyRepository : IEmployeeReadOnlyRepository
{
    private readonly OracleDbContext _dbContext;
    private readonly IMapper _mapper;

    public EmployeeReadOnlyRepository(OracleDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<EmployeeQueryModel> Get(decimal id)
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryFirstOrDefaultAsync<EmployeeEntity>
            (EmployeeSqlConsts.SQL_GET_BY_ID, new { PR_EMPLOYEE_ID = id });

        if (result == null)
        {
            return null;
        }

        var mapper = _mapper.Map<EmployeeQueryModel>(result);
        return mapper;
    }

    public async Task<List<EmployeeQueryModel>> Get()
    {
        using IDbConnection dbConnection = _dbContext.CreateConnection();
        dbConnection.Open();

        var result = await dbConnection.QueryAsync<EmployeeEntity>(EmployeeSqlConsts.SQL_GET);
        var mapper = _mapper.Map<List<EmployeeQueryModel>>(result);
        return mapper.AsList();
    }
}
