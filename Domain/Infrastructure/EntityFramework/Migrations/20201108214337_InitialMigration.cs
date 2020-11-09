using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MoE.ECE.Domain.Infrastructure.EntityFramework.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "referencedata");

            migrationBuilder.CreateTable(
                name: "ece_licencing_detail_date_ranged_parameter",
                schema: "referencedata",
                columns: table => new
                {
                    licencing_detail_history_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ref_organisation_id = table.Column<int>(nullable: false),
                    application_status_id = table.Column<int>(nullable: false),
                    application_status_description = table.Column<string>(nullable: true),
                    licence_status_id = table.Column<int>(nullable: false),
                    licence_status_description = table.Column<string>(nullable: true),
                    licence_class_id = table.Column<int>(nullable: true),
                    licence_class_description = table.Column<string>(nullable: true),
                    service_provision_type_id = table.Column<int>(nullable: true),
                    service_provision_type_description = table.Column<string>(nullable: true),
                    effective_from_date = table.Column<DateTimeOffset>(nullable: false),
                    effective_to_date = table.Column<DateTimeOffset>(nullable: true),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    modified_date = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ece_licencing_detail_date_ranged_parameters", x => x.licencing_detail_history_id);
                });

            migrationBuilder.CreateTable(
                name: "ece_service",
                schema: "referencedata",
                columns: table => new
                {
                    ref_organisation_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organisation_name = table.Column<string>(nullable: false),
                    organisation_number = table.Column<string>(nullable: false),
                    organisation_type_id = table.Column<int>(nullable: false),
                    organisation_type_description = table.Column<string>(nullable: true),
                    organisation_sector_role_id = table.Column<int>(nullable: true),
                    organisation_sector_role_description = table.Column<string>(nullable: true),
                    organisation_status_id = table.Column<int>(nullable: false),
                    organisation_status_description = table.Column<string>(nullable: true),
                    external_provider_id = table.Column<string>(nullable: true),
                    nzbn = table.Column<long>(nullable: true),
                    region_id = table.Column<int>(nullable: true),
                    region_description = table.Column<string>(nullable: true),
                    open_date = table.Column<DateTimeOffset>(nullable: true),
                    ece_service_status_date = table.Column<DateTimeOffset>(nullable: true),
                    ece_service_status_reason_id = table.Column<int>(nullable: true),
                    ece_service_status_reason_description = table.Column<string>(nullable: true),
                    phone_number = table.Column<string>(nullable: true),
                    fax_number = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    other_email = table.Column<string>(nullable: true),
                    website = table.Column<string>(nullable: true),
                    primary_language_id = table.Column<int>(nullable: true),
                    primary_language_description = table.Column<string>(nullable: true),
                    is_funded = table.Column<bool>(nullable: true),
                    ece_service_provider_id = table.Column<int>(nullable: true),
                    ece_service_provider_number = table.Column<string>(nullable: true),
                    ece_service_provider_name = table.Column<string>(nullable: true),
                    location_address_line1 = table.Column<string>(nullable: true),
                    location_address_line2 = table.Column<string>(nullable: true),
                    location_address_line3 = table.Column<string>(nullable: true),
                    location_address_line4 = table.Column<string>(nullable: true),
                    postal_address_line1 = table.Column<string>(nullable: true),
                    postal_address_line2 = table.Column<string>(nullable: true),
                    postal_address_line3 = table.Column<string>(nullable: true),
                    postal_address_line4 = table.Column<string>(nullable: true),
                    service_provision_type_id = table.Column<int>(nullable: true),
                    service_provision_type_description = table.Column<string>(nullable: true),
                    application_status_id = table.Column<int>(nullable: false),
                    application_status_description = table.Column<string>(nullable: true),
                    licence_status_id = table.Column<int>(nullable: false),
                    licence_status_description = table.Column<string>(nullable: true),
                    licence_class_id = table.Column<int>(nullable: true),
                    licence_class_description = table.Column<string>(nullable: true),
                    equity_index_id = table.Column<int>(nullable: true),
                    equity_index_description = table.Column<string>(nullable: true),
                    isolation_index = table.Column<decimal>(type: "decimal(12, 2)", nullable: true),
                    installment_payments = table.Column<bool>(nullable: true),
                    ec_quality_level_id = table.Column<int>(nullable: true),
                    ec_quality_level_description = table.Column<string>(nullable: true),
                    teacher_led_eligible_to_offer_free = table.Column<bool>(nullable: true),
                    parent_led_eligible_to_offer_free = table.Column<bool>(nullable: true),
                    ece_service_provider_ownership_type_id = table.Column<int>(nullable: true),
                    ece_service_provider_ownership_type_description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ece_services", x => x.ref_organisation_id);
                });

            migrationBuilder.CreateTable(
                name: "ece_service_date_ranged_parameter",
                schema: "referencedata",
                columns: table => new
                {
                    history_id = table.Column<int>(nullable: false),
                    attribute_source = table.Column<string>(nullable: false),
                    ref_organisation_id = table.Column<int>(nullable: false),
                    attribute = table.Column<string>(nullable: true),
                    value = table.Column<string>(nullable: true),
                    value_description = table.Column<string>(nullable: true),
                    is_array = table.Column<bool>(nullable: false),
                    effective_from_date = table.Column<DateTimeOffset>(nullable: false),
                    effective_to_date = table.Column<DateTimeOffset>(nullable: true),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    modified_date = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ece_service_date_ranged_parameters", x => new { x.history_id, x.attribute_source });
                });

            migrationBuilder.CreateTable(
                name: "lookup_type",
                schema: "referencedata",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lookup_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ece_operating_session_date_ranged_parameter",
                schema: "referencedata",
                columns: table => new
                {
                    licencing_detail_history_id = table.Column<int>(nullable: false),
                    operating_session_id = table.Column<int>(nullable: false),
                    ref_organisation_id = table.Column<int>(nullable: false),
                    session_day_id = table.Column<int>(nullable: true),
                    session_day_description = table.Column<string>(nullable: true),
                    start_time = table.Column<DateTimeOffset>(nullable: true),
                    end_time = table.Column<DateTimeOffset>(nullable: true),
                    max_children = table.Column<int>(nullable: true),
                    max_children_under2 = table.Column<int>(nullable: true),
                    session_type_id = table.Column<int>(nullable: true),
                    session_type_description = table.Column<string>(nullable: true),
                    session_provision_type_id = table.Column<int>(nullable: true),
                    session_provision_type_description = table.Column<string>(nullable: true),
                    effective_from_date = table.Column<DateTimeOffset>(nullable: false),
                    effective_to_date = table.Column<DateTimeOffset>(nullable: true),
                    created_date = table.Column<DateTimeOffset>(nullable: false),
                    modified_date = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ece_operating_session_date_ranged_parameters", x => new { x.licencing_detail_history_id, x.operating_session_id });
                    table.ForeignKey(
                        name: "fk_ece_operating_session_date_ranged_parameters_ece_licencing_",
                        column: x => x.licencing_detail_history_id,
                        principalSchema: "referencedata",
                        principalTable: "ece_licencing_detail_date_ranged_parameter",
                        principalColumn: "licencing_detail_history_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ece_operating_session",
                schema: "referencedata",
                columns: table => new
                {
                    operating_session_id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ref_organisation_id = table.Column<int>(nullable: false),
                    session_day_id = table.Column<int>(nullable: true),
                    session_day_description = table.Column<string>(nullable: true),
                    start_time = table.Column<DateTimeOffset>(nullable: true),
                    end_time = table.Column<DateTimeOffset>(nullable: true),
                    max_children = table.Column<int>(nullable: true),
                    max_children_under2 = table.Column<int>(nullable: true),
                    session_type_id = table.Column<int>(nullable: true),
                    session_type_description = table.Column<string>(nullable: true),
                    session_provision_type_id = table.Column<int>(nullable: true),
                    session_provision_type_description = table.Column<string>(nullable: true),
                    funded_hours = table.Column<int>(nullable: false),
                    operating_hours = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ece_operating_sessions", x => x.operating_session_id);
                    table.ForeignKey(
                        name: "fk_ece_operating_session_ece_services_ece_service_ref_organisa",
                        column: x => x.ref_organisation_id,
                        principalSchema: "referencedata",
                        principalTable: "ece_service",
                        principalColumn: "ref_organisation_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "lookup",
                schema: "referencedata",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(nullable: false),
                    lookup_type_id = table.Column<int>(nullable: false),
                    edumis_code = table.Column<string>(nullable: true),
                    parent_lookup_id = table.Column<int>(nullable: true),
                    effective_from_date = table.Column<DateTimeOffset>(nullable: false),
                    effective_to_date = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_lookups", x => x.id);
                    table.ForeignKey(
                        name: "fk_lookups_lookup_types_lookup_type_id",
                        column: x => x.lookup_type_id,
                        principalSchema: "referencedata",
                        principalTable: "lookup_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ece_licencing_detail_date_ranged_parameters_ref_organisatio",
                schema: "referencedata",
                table: "ece_licencing_detail_date_ranged_parameter",
                column: "ref_organisation_id");

            migrationBuilder.CreateIndex(
                name: "ix_ece_operating_session_ref_organisation_id",
                schema: "referencedata",
                table: "ece_operating_session",
                column: "ref_organisation_id");

            migrationBuilder.CreateIndex(
                name: "ix_ece_operating_session_date_ranged_parameters_ref_organisati",
                schema: "referencedata",
                table: "ece_operating_session_date_ranged_parameter",
                column: "ref_organisation_id");

            migrationBuilder.CreateIndex(
                name: "ix_ece_service_date_ranged_parameters_ref_organisation_id",
                schema: "referencedata",
                table: "ece_service_date_ranged_parameter",
                column: "ref_organisation_id");

            migrationBuilder.CreateIndex(
                name: "ix_lookups_lookup_type_id",
                schema: "referencedata",
                table: "lookup",
                column: "lookup_type_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ece_operating_session",
                schema: "referencedata");

            migrationBuilder.DropTable(
                name: "ece_operating_session_date_ranged_parameter",
                schema: "referencedata");

            migrationBuilder.DropTable(
                name: "ece_service_date_ranged_parameter",
                schema: "referencedata");

            migrationBuilder.DropTable(
                name: "lookup",
                schema: "referencedata");

            migrationBuilder.DropTable(
                name: "ece_service",
                schema: "referencedata");

            migrationBuilder.DropTable(
                name: "ece_licencing_detail_date_ranged_parameter",
                schema: "referencedata");

            migrationBuilder.DropTable(
                name: "lookup_type",
                schema: "referencedata");
        }
    }
}
