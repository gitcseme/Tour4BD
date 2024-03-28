using Application.Enums;
using System.Collections.Generic;
using System.Linq;

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
        public static readonly Dictionary<string, IEnumerable<string>> PermissionsDict = new()
        {
            { 
                Roles.Admin, 
                new List<Permission> { Permission.Read, Permission.Add, Permission.Delete, Permission.Update }.Select(p => p.ToString()) 
            },
            {
                Roles.Manager,
                new List<Permission> { Permission.Read, Permission.Add, Permission.Update }.Select(p => p.ToString())
            },
            {
                Roles.User,
                new List<Permission> { Permission.Read, Permission.Add }.Select(p => p.ToString())
            }
        };
    }

    public static class CustomClaim
    {
        public const string Permissions = "Permissions";
    }
}