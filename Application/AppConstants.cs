using System.Collections.Generic;

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

        public static string[] GetAll() => [Admin, User, Manager];
    }

    public static class Permissions
    {
        public static IEnumerable<string> GetDefaultPermissions()
        {
            return [ "Add", "Read", "Update", "Delete" ];
        }
    }

    public static class CustomClaim
    {
        public const string Permissions = "Permissions";
    }
}