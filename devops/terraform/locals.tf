locals {
  resource_group_name        = upper("${var.environment_prefix}-ERS-${var.environment}-${var.application_name}")
  resource_base_name         = lower("${var.environment_prefix}ers${replace(var.environment, "-", "")}")
  common_resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment}-COMMON")
  master_resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-MASTER")
  environmentgroup_base_name = lower("${var.environment_prefix}ers${var.environment_suffix}")

  environment_number = lower(split("-", var.environment)[1])
  environment_name   = lower(split("-", var.environment)[0])

  db_server_resource_group = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-PSQL-DATABASES")
  db_server_name           = lower("${var.environment_prefix}ers${var.environment_suffix}psqlserver")
  db_server_endpoint       = "${local.db_server_name}.postgres.database.azure.com"
  db_username              = lower("${local.resource_base_name}${var.application_name}")
  db_name                  = lower("${local.resource_base_name}psql${var.application_name}")

  tags_mandatory = {
    agency         = "MOE"
    application    = var.TAG_APPLICATION
    buscontact     = var.TAG_BUSCONTACT
    classification = var.TAG_DATA_CLASSIFICATION
    costcode       = var.TAG_COSTCODE
    department     = var.TAG_DEPARTMENT
    environment    = upper(regex("^[a-zA-Z]+", var.environment))
    fundinginfo    = var.TAG_CONTAINS_FUNDING_INFORMATION
    pii            = var.TAG_CONTAINS_PERSONALLY_IDENTIFIABLE_INFORMATION
    privacyrating  = var.TAG_PRIVACY_RATING
    provider       = "Azure"
    publicfacing   = var.TAG_IS_PUBLIC_FACING
    role           = "ERS ECE API"
    teccontact     = var.TAG_TECCONTACT
  }
  tags_optional = {
    lastupdatedby = data.azurerm_client_config.current.object_id
    creationdate  = timestamp()
  }
  tags = merge(local.tags_mandatory, local.tags_optional)
}