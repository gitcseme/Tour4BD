using API.Helpers;

namespace API.Extensions;

public static class MigrationHelperExtensions
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        var migrationHelper = new MigrationHelper(app);
        await migrationHelper.SeedAsync();

    }
}
