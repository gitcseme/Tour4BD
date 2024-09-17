using System.Collections.Generic;

namespace Application;

public static class AppConstants
{
    public const string TenantDbConnectionStringName = "TenantDbConnection";
    public const string TenantDbContextDIKey = "tenantUOW";
    public const string ApplicationDbContextDIKey = "applicationUOW";

    public const string TokenItem = "jwt_token";

    // Roles
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string Manager = "Manager";
        public const string MarketingManager = "MarketingManager";
        public const string ContentCreator = "ContentCreator";


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
        public const string TenantConnectionString = "DatabaseConnectionString";
    }

    public static class DefaultMessages
    {
        public const string CreateSuccess = "Data created successfully";
        public const string CreateFailed = "Data created successfully";

    }
}