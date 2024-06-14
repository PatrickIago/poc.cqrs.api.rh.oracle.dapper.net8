using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using MediatR;
using poc.core.api.net8.Interface;
using Poc.Contract.Query.Region.Interfaces;
using Poc.Contract.Query.Region.Request;
using Poc.Contract.Query.Region.Validators;
using Poc.Contract.Query.Region.ViewModels;

namespace Poc.Query.Region;

public class GetRegionByIdQueryHandler : IRequestHandler<GetRegionByIdQuery, Result<RegionQueryModel>>
{
    private readonly IRegionsReadOnlyRepository _repo;
    private readonly IRedisCacheService<RegionQueryModel> _cacheService;
    private readonly GetRegionByIdQueryValidator _validator;
    public GetRegionByIdQueryHandler(IRegionsReadOnlyRepository repo,
                                     IRedisCacheService<RegionQueryModel> cacheService,
                                     GetRegionByIdQueryValidator validator)
    {
        _repo = repo;
        _cacheService = cacheService;
        _validator = validator;
    }

    public async Task<Result<RegionQueryModel>> Handle(GetRegionByIdQuery request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return Result.Invalid(validationResult.AsErrors());

        var cacheKey = $"{nameof(GetRegionByIdQuery)}_{request.RegionId}";

        var model = await _cacheService.GetOrCreateAsync(cacheKey, () => _repo.Get(request.RegionId), TimeSpan.FromHours(2));
        if (model == null)
            return Result.NotFound($"Nenhum registro encontrado pelo Id: {request.RegionId}");

        return Result.Success(model);

    }
}