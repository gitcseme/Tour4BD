namespace Application;

public static class AppConstants
{
    public const string TenantDbConnectionStringName = "TenantDbConnection";

    // Roles
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Manager = "Manager";

        public static string[] GetAll() => new string[] { Admin, User, Manager };
    } 
}
