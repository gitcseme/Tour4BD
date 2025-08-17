using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;

namespace SharedKernel.Grids;

public static class GridDataFetcher
{
    public static async Task<ListResponse<TResponse>> GetListAsync<TEntity, TResponse>(
        IQueryable<TEntity> query,
        GridDataFetchRequest request,
        IMapper mapper,
        CancellationToken ctn = default)
        where TResponse : class
        where TEntity : class
    {
        var result = new ListResponse<TResponse>();
        var membersToExpand = GetRequestedColumns(request, typeof(TResponse));

        var projectedQuery = query.ProjectTo<TResponse>(mapper.ConfigurationProvider, null, membersToExpand);

        var queryBuilder = await GridQueryBuilder<TResponse, GridDataFetchRequest>.ExecuteAsync(projectedQuery, request);

        result.TotalRowCount = queryBuilder.TotalCount;
        result.Rows = await queryBuilder.Query.ToListAsync(ctn);

        return result;
    }

    private static string[] GetRequestedColumns(GridDataFetchRequest request, Type responseType)
    {
        var propertyInfos = responseType.GetProperties();
        var fields = request.GetPascalcaseFields();

        if (fields.Count == 0)
        {
            return propertyInfos.Select(x => x.Name).ToArray();
        }

        var invalidFields = fields.Except(propertyInfos.Select(x => x.Name)).ToList();

        if (invalidFields.Count > 0)
        {
            throw new InvalidRequestException($"[{string.Join(", ", invalidFields)}] are invalid fields");
        }

        return fields.ToArray();
    }
}
