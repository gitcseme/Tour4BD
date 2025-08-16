namespace SharedKarnel;

public static class AppConstants
{
    public const string MsSqlConnection = "MsSqlConnection";
    public const string CreateSuccess = "Data created successfully";
    public const string UpdateSuccess = "Data updated successfully";

    public const string RedisConnection = "RedisConnection";
}

public static class Operators
{
    public const string Eql = "=";
    public const string Greater = ">";
    public const string GreaterOrEql = ">=";
    public const string Less = "<";
    public const string LessOrEql = "<=";
    public const string Contains = "contains";
    public const string ContainsOrEql = "contains-or-equal";
    public const string Fts = "fts";
}