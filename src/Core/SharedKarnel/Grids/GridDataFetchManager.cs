using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKarnel.Exceptions;

namespace SharedKarnel.Grids;

public class GridDataFetchManager(IMapper mapper)
{
    public async Task<ListResponse<TResponse>> GetListAsync<TEntity, TResponse>(
        IQueryable<TEntity> entityQuery,
        GridDataFetchRequest request,
        CancellationToken ctn = default)
        where TResponse : class
        where TEntity : class
    {
        var result = new ListResponse<TResponse>();

        var projectedQuery = entityQuery.ProjectTo<TResponse>(mapper.ConfigurationProvider, null, GetValidFields(request, typeof(TResponse)));

        var queryBuilder = await new GridQueryBuilder<TResponse, GridDataFetchRequest>(projectedQuery, request).ExecuteAsync();

        result.TotalRowCount = queryBuilder.TotalCount;
        result.Rows = await queryBuilder.Query.ToListAsync(ctn);

        return result;
    }

    private static string[] GetValidFields(GridDataFetchRequest request, Type responseType)
    {
        var propertyInfos = responseType.GetProperties();
        var fields = request.GetPascalcaseFields();

        if (!fields.Any())
        {
            return propertyInfos.Select(x => x.Name).ToArray();
        }

        var invalidFields = fields.Except(propertyInfos.Select(x => x.Name)).ToList();

        if (invalidFields.Any())
        {
            throw new InvalidRequestException($"[{string.Join(", ", invalidFields)}] are invalid fields");
        }

        return fields.ToArray();
    }
}
