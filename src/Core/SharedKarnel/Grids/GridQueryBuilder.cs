using Microsoft.EntityFrameworkCore;

namespace SharedKarnel.Grids;

public class GridQueryBuilder<T, TGridRequest>
    where T : class
    where TGridRequest : GridDataFetchRequest
{
    private IQueryable<T> _query;
    private readonly TGridRequest _request;

    public GridQueryBuilder(IQueryable<T> query, TGridRequest request)
    {
        _query = query;
        _request = request;
    }

    public async Task<(IQueryable<T> Query, int TotalCount)> ExecuteAsync(bool includeTotalCount = true)
    {
        if (_request.Search != null)
        {
            _query = GridOperations.Search(_query, _request.Search);
        }

        var totalCount = includeTotalCount ? await _query.CountAsync() : 0;

        if (_request.Sort != null)
        {
            _query = GridOperations.Sort(_query, _request.Sort);
        }

        if (_request.Pagination != null)
        {
            _query = GridOperations.Paginate(_query, _request.Pagination);
        }

        return (_query, totalCount);
    }
}