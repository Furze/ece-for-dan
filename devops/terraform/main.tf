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
    lastupdated   = timestamp()
    creationdate  = timestamp()
  }
  tags = merge(local.tags_mandatory, local.tags_optional)
}

# Default provider that is based on the active subscription
provider "azurerm" {
  features {
    key_vault {
      purge_soft_delete_on_destroy = true
    }
  } # This is required for version 2+
}

# Pinned provider to the subscription where ACR and other single instance resources live
provider "azurerm" {
  alias           = "acr_subscription"
  subscription_id = var.acr_subscriptionid
  features {} # This is required for version 2+
  skip_provider_registration = true
}

provider "random" {
}

# Use active azure subscription to query tenant_id
data "azurerm_subscription" "current" {
}

data "azurerm_client_config" "current" {
}

data "azurerm_resource_group" "cmn" {
  name = local.common_resource_group_name
}

data "azurerm_key_vault" "master" {
  name                = "${local.resource_base_name}secrets"
  resource_group_name = local.master_resource_group_name
}

data "azurerm_container_registry" "cmn" {
  provider = azurerm.acr_subscription

  name                = var.azurerm_container_registry_name
  resource_group_name = var.azurerm_container_registry_resource_group_name
}

data "azurerm_app_service_plan" "cmn" {
  name                = "${local.resource_base_name}asplinux"
  resource_group_name = local.common_resource_group_name
}

data "azurerm_servicebus_namespace" "cmn" {
  name                = "${local.resource_base_name}servicebus"
  resource_group_name = local.common_resource_group_name
}

data "azurerm_application_insights" "cmn" {
  name                = "${local.resource_base_name}appinsights"
  resource_group_name = local.common_resource_group_name
}

data "azurerm_kubernetes_cluster" "envgroup" {
  name                = lower("${local.environmentgroup_base_name}aks")
  resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-AKS")
}

#                                                                                                 
#   #####  ######  ####   ####  #    # #####   ####  ######     ####  #####   ####  #    # #####  
#   #    # #      #      #    # #    # #    # #    # #         #    # #    # #    # #    # #    # 
#   #    # #####   ####  #    # #    # #    # #      #####     #      #    # #    # #    # #    # 
#   #####  #           # #    # #    # #####  #      #         #  ### #####  #    # #    # #####  
#   #   #  #      #    # #    # #    # #   #  #    # #         #    # #   #  #    # #    # #      
#   #    # ######  ####   ####   ####  #    #  ####  ######     ####  #    #  ####   ####  #      
#                                                                                                 

resource "azurerm_resource_group" "app" {
  name     = local.resource_group_name
  location = var.location
  tags     = local.tags
  lifecycle {
    ignore_changes = [
      tags["creationdate"]
    ]
  }
}

#                                                                                  
#    ####   ####  #         #####    ##   #####   ##   #####    ##    ####  ###### 
#   #      #    # #         #    #  #  #    #    #  #  #    #  #  #  #      #      
#    ####  #    # #         #    # #    #   #   #    # #####  #    #  ####  #####  
#        # #  # # #         #    # ######   #   ###### #    # ######      # #      
#   #    # #   #  #         #    # #    #   #   #    # #    # #    # #    # #      
#    ####   ### # ######    #####  #    #   #   #    # #####  #    #  ####  ###### 
#                                                                                  

# Generate random password for Postgres Server
resource "random_password" "psqlpassword" {
  length           = 16
  special          = true
  min_special      = 2
  min_upper        = 2
  min_lower        = 2
  min_numeric      = 2
  override_special = "_%<>|#~+-"
}

# Save SQL Password to Keyvault
resource "azurerm_key_vault_secret" "sqlpassword" {
  name         = "${lower(var.application_name)}-sqlpassword"
  value        = random_password.psqlpassword.result
  key_vault_id = data.azurerm_key_vault.master.id
  tags = {
    createdby     = data.azurerm_client_config.current.object_id
    purgeOnDelete = "true"
  }
}

resource "azurerm_postgresql_database" "app" {
  name                = local.db_name
  resource_group_name = local.db_server_resource_group
  server_name         = local.db_server_name
  charset             = var.sql_charset
  collation           = var.sql_collation
}

#                                                                       
#     ##   #####  #####      ####  ###### #####  #    # #  ####  ###### 
#    #  #  #    # #    #    #      #      #    # #    # # #    # #      
#   #    # #    # #    #     ####  #####  #    # #    # # #      #####  
#   ###### #####  #####          # #      #####  #    # # #      #      
#   #    # #      #         #    # #      #   #   #  #  # #    # #      
#   #    # #      #          ####  ###### #    #   ##   #  ####  ###### 
#                                                                       

resource "azurerm_app_service" "app" {
  name                    = "${local.resource_base_name}${lower(var.application_name)}"
  location                = var.location
  resource_group_name     = azurerm_resource_group.app.name
  app_service_plan_id     = data.azurerm_app_service_plan.cmn.id
  client_affinity_enabled = false
  identity {
    type = "SystemAssigned"
  }

  app_settings = {
    DOCKER_CUSTOM_IMAGE_NAME            = "" # Set by the release task in the pipeline when image is deployed
    DOCKER_REGISTRY_SERVER_URL          = "https://${data.azurerm_container_registry.cmn.login_server}"
    DOCKER_REGISTRY_SERVER_USERNAME     = data.azurerm_container_registry.cmn.admin_username
    DOCKER_REGISTRY_SERVER_PASSWORD     = data.azurerm_container_registry.cmn.admin_password
    ASPNETCORE_ENVIRONMENT              = lower(replace(var.environment, "-", ""))
    WEBSITES_ENABLE_APP_SERVICE_STORAGE = "false"
    WEBSITE_TIME_ZONE                   = "New Zealand Standard Time"
    ReleaseDate                         = ""
    APPINSIGHTS_INSTRUMENTATIONKEY      = data.azurerm_application_insights.cmn.instrumentation_key
    OidcSettings__Authority             = "https://${local.resource_base_name}identity.azurewebsites.net/"
    OidcSettings__Issuer                = "https://${local.resource_base_name}identity.azurewebsites.net/"
    KeyVault__Vault                     = data.azurerm_key_vault.master.name
    MartenSettings__ConnectionString    = "host=${local.db_server_endpoint};port=5432;database=${local.db_name};password={{PASSWORD_FROM_KEYVAULT}};username=${local.db_username}@${local.db_server_name};Pooling=true;Ssl Mode=Require;"
    OpaSettings__AuthorisationUrl       = local.opa_authorisation_url
    OpaSettings__RuleBaseUrl            = local.opa_rulebase_url
    ASPNETCORE_FORWARDEDHEADERS_ENABLED = "true"
  }

  https_only = true

  connection_string {
    name  = "ServiceBus"
    type  = "Custom"
    value = data.azurerm_servicebus_namespace.cmn.default_primary_connection_string
  }
  site_config {
    linux_fx_version  = "DOCKER"
    ftps_state        = "Disabled"
    health_check_path = "/health"
    http2_enabled     = true
  }
  tags = local.tags
  lifecycle {
    ignore_changes = [
      site_config[0].linux_fx_version,
      app_settings["ReleaseDate"],
      app_settings["DOCKER_CUSTOM_IMAGE_NAME"],
      tags["creationdate"]
    ]
  }
}

# Sets local variable for App Service MSI ID for use in setting access to other resources
locals {
  app_msi_id = azurerm_app_service.app.identity[0].principal_id
}

# Add App Service MSI to Key Vault Access Policy
resource "azurerm_key_vault_access_policy" "app" {
  key_vault_id = data.azurerm_key_vault.master.id
  tenant_id    = data.azurerm_subscription.current.tenant_id
  object_id    = local.app_msi_id

  secret_permissions = [
    "Get",
    "List"
  ]
}

data "azurerm_resource_group" "aks_node_pool" {
  name = data.azurerm_kubernetes_cluster.envgroup.node_resource_group
}

# Store managed identity in the AKS Node Pool group
resource "azurerm_user_assigned_identity" "aks_app" {
  resource_group_name = data.azurerm_resource_group.aks_node_pool.name
  location            = data.azurerm_resource_group.aks_node_pool.location

  name = lower("${local.resource_base_name}aks${var.application_name}")
}

locals {
  aks_app_msi_id = azurerm_user_assigned_identity.aks_app.principal_id
}

# Allow the identity to read/validate its own role against the vmss in the aks node pool group
resource "azurerm_role_assignment" "appmsi_to_aksmc_reader" {
  scope                = data.azurerm_resource_group.aks_node_pool.id
  role_definition_name = "Reader"
  principal_id         = local.aks_app_msi_id
}

# Add App Service MSI to Key Vault Access Policy
resource "azurerm_key_vault_access_policy" "aks_app" {
  key_vault_id = data.azurerm_key_vault.master.id
  tenant_id    = data.azurerm_subscription.current.tenant_id
  object_id    = local.aks_app_msi_id

  secret_permissions = [
    "Get",
    "List"
  ]
}

# Generate a GUID for Web Test
resource "random_uuid" "webtest" {}

# Create Web/Availability Test
resource "azurerm_application_insights_web_test" "webtest" {
  name                    = "${azurerm_app_service.app.name}webtest"
  location                = data.azurerm_application_insights.cmn.location
  resource_group_name     = data.azurerm_application_insights.cmn.resource_group_name
  application_insights_id = data.azurerm_application_insights.cmn.id
  description             = ""
  kind                    = "ping"
  frequency               = 300
  timeout                 = 60
  enabled                 = true
  geo_locations           = ["apac-sg-sin-azr", "apac-hk-hkn-azr", "emea-au-syd-edge"]
  retry_enabled           = true

  tags = {
    "hidden-link:${data.azurerm_application_insights.cmn.id}" = "Resource"
  }

  configuration = <<XML
  <WebTest         Name="${azurerm_app_service.app.name}-webtest"         Id="${random_uuid.webtest.result}"         Enabled="True"         CssProjectStructure=""         CssIteration=""         Timeout="60"         WorkItemIds=""         xmlns="http://microsoft.com/schemas/VisualStudio/TeamTest/2010"         Description=""         CredentialUserName=""         CredentialPassword=""         PreAuthenticate="True"         Proxy="default"         StopOnError="False"         RecordedResultFile=""         ResultsLocale="">
    <Items>        
        <Request         Method="GET"         Guid="${random_uuid.webtest.result}"         Version="1.1"         Url="https://${azurerm_app_service.app.default_site_hostname}/health"         ThinkTime="0"         Timeout="60"         ParseDependentRequests="True"         FollowRedirects="True"         RecordResult="True"         Cache="False"         ResponseTimeGoal="0"         Encoding="utf-8"         ExpectedHttpStatusCode="200"         ExpectedResponseUrl=""         ReportingName=""         IgnoreHttpStatusCode="False" />        
    </Items>
    <ValidationRules>
        <ValidationRule         Classname="Microsoft.VisualStudio.TestTools.WebTesting.Rules.ValidationRuleFindText, Microsoft.VisualStudio.QualityTools.WebTestFramework, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"         DisplayName="Find Text"         Description="Verifies the existence of the specified text in the response."         Level="High"         ExectuionOrder="BeforeDependents">
            <RuleParameters>
                <RuleParameter Name="FindText" Value="&#x22;status&#x22;&#x3A;&#x20;&#x22;Healthy&#x22;" />
                <RuleParameter Name="IgnoreCase" Value="False" />
                <RuleParameter Name="UseRegularExpression" Value="False" />
                <RuleParameter Name="PassIfTextFound" Value="True" />
            </RuleParameters>
        </ValidationRule>
    </ValidationRules>
  </WebTest>
  XML
}

data "azurerm_monitor_action_group" "actiongroup" {
  name                = var.actiongroup_name
  resource_group_name = var.actiongroup_resource_group
}

# Currently the azurerm_monitor_metric_alert resource only allows for 1 item in the scopes attribute whereas the alert needed for availability test metric requires 2
# There is currently a feature request here: https://github.com/terraform-providers/terraform-provider-azurerm/issues/5063
# Therefore we still require using the original method of an ARM template
resource "azurerm_template_deployment" "webtest_metric_alert" {
  name                = "${azurerm_app_service.app.name}healthalert-deploy"
  resource_group_name = data.azurerm_resource_group.cmn.name
  deployment_mode     = "Incremental"

  template_body = <<DEPLOY
{
    "$schema": "https://schema.management.azure.com/schemas/2015-01-01/deploymentTemplate.json#",
    "contentVersion": "1.0.0.0",
    "resources": [
        {
            "type": "Microsoft.Insights/metricAlerts",
            "apiVersion": "2018-03-01",
            "name": "${azurerm_app_service.app.name}-pingmetricalert",
            "location": "global",
            "tags": {
                "hidden-link:${data.azurerm_application_insights.cmn.id}": "Resource",
                "hidden-link:${azurerm_application_insights_web_test.webtest.id}": "Resource"
            },
            "properties": {
                "description": "${azurerm_app_service.app.name}health",
                "severity": 1,
                "enabled": "true",
                "scopes": [
                    "${data.azurerm_application_insights.cmn.id}",
                    "${azurerm_application_insights_web_test.webtest.id}"
                ],
                "evaluationFrequency": "PT1M",
                "windowSize": "PT5M",
                "criteria": {
                    "odata.type": "Microsoft.Azure.Monitor.WebtestLocationAvailabilityCriteria",
                    "webTestId": "${azurerm_application_insights_web_test.webtest.id}",
                    "componentId": "${data.azurerm_application_insights.cmn.id}",
                    "failedLocationCount": 2
                },
                "actions": [
                    {
                        "actionGroupId": "${data.azurerm_monitor_action_group.actiongroup.id}",
                        "webhookProperties": {
                        }
                    }
                ]
            }
        }
    ]
}
  DEPLOY
}

#                                                                       
#    ####  ###### #####  #    # #  ####  ######    #####  #    #  ####  
#   #      #      #    # #    # # #    # #         #    # #    # #      
#    ####  #####  #    # #    # # #      #####     #####  #    #  ####  
#        # #      #####  #    # # #      #         #    # #    #      # 
#   #    # #      #   #   #  #  # #    # #         #    # #    # #    # 
#    ####  ###### #    #   ##   #  ####  ######    #####   ####   ####  
#                                                                       

# Service Bus Topic
resource "azurerm_servicebus_topic" "ece" {
  name                = "ece"
  resource_group_name = local.common_resource_group_name
  namespace_name      = data.azurerm_servicebus_namespace.cmn.name
}

# Service Bus Subscription
resource "azurerm_servicebus_subscription" "workflow" {
  name                = "workflow"
  resource_group_name = local.common_resource_group_name
  namespace_name      = data.azurerm_servicebus_namespace.cmn.name
  topic_name          = azurerm_servicebus_topic.ece.name
  max_delivery_count  = 10
}

output "run_by_object_id" {
  value = data.azurerm_client_config.current.object_id
}
