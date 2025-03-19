namespace SharedKarnel.Grids;

public class DataFetchRequest
{
    public List<Search>? Search { get; set; }

    public Sort? Sort { get; set; }
}

public class GridDataFetchRequest : DataFetchRequest
{
    public Pagination? Pagination { get; set; }
    public List<string> Fields { get; set; } = [];
    public List<string> GetPascalcaseFields() => Fields.Select(f => char.ToUpper(f[0]) + f.Substring(1)).ToList();
}

public class Pagination
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }

    public int GetPageNumber => PageNumber > 0 ? PageNumber : 1;
    public int GetPageSize => PageSize > 0 ? PageSize : 10;
}

public class Search
{
    public string? Value { get; set; }
    public string Operator { get; set; } = "contains"; // <, >, <=, >=, =, contains, app-key-equal

    public IEnumerable<string> Fields { get; set; } = [];

    public IEnumerable<string> GetPascalCaseFields => Fields.Select(f => char.ToUpper(f[0]) + f.Substring(1));
}

public class Sort
{
    public string FieldName { get; set; }

    public string Direction { get; set; }
}
