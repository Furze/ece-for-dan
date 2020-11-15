using Microsoft.EntityFrameworkCore.Migrations;

namespace MoE.ECE.Domain.Infrastructure.Extensions
{
    public static class MigrationBuilderExtensions
    {
        /// <summary>
        /// This drops the reference data tables. It allows (during development) for us to just
        /// regenerate the initial migration each time.
        /// </summary>
        public static void DropReferenceDataTables(this MigrationBuilder migrationBuilder)
        {
            const string sql = @"
                drop table if exists referencedata.lookup
                drop table if exists referencedata.lookup_type

                drop table if exists referencedata.ece_operating_session_date_ranged_parameter
                drop table if exists referencedata.ece_operating_session_date_ranged_parameter
                drop table if exists referencedata.ece_service_date_ranged_parameter

                drop table if exists referencedata.ece_operating_session
                drop table if exists referencedata.ece_service";

            migrationBuilder.Sql(sql);
        }
    }
}