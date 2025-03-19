namespace SharedKarnel.Grids;

public class ListResponse<T>
{
    public int TotalRowCount { get; set; }

    public IEnumerable<T> Rows { get; set; }
}