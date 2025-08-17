using Microsoft.EntityFrameworkCore;

namespace SharedKernel.Grids;

public static class GridQueryBuilder<T, TGridRequest>
    where T : class
    where TGridRequest : GridDataFetchRequest
{
    public static async Task<(IQueryable<T> Query, int TotalCount)> ExecuteAsync(
        IQueryable<T> query, 
        TGridRequest request, 
        bool includeTotalCount = true)
    {
        if (request.Search != null)
        {
            query = GridOperations.Search(query, request.Search);
        }

        var totalCount = includeTotalCount ? await query.CountAsync() : 0;

        if (request.Sort != null)
        {
            query = GridOperations.Sort(query, request.Sort);
        }

        if (request.Pagination != null)
        {
            query = GridOperations.Paginate(query, request.Pagination);
        }

        return (query, totalCount);
    }
}