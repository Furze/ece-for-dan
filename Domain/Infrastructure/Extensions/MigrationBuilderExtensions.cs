using Microsoft.EntityFrameworkCore.Migrations;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class MigrationBuilderExtensions
    {
        /// <summary>
        ///     This drops the reference data tables. It allows (during development) for us to just
        ///     regenerate the initial migration each time.
        /// </summary>
        public static void DropReferenceDataTables(this MigrationBuilder migrationBuilder)
        {
            const string sql = @"
                TODO";

            migrationBuilder.Sql(sql);
        }
    }
}