# Default provider that is based on the active subscription
provider "azurerm" {
  features {}
}

# Remove this after app services are decommissioned
provider "azurerm" {
  alias           = "acr_subscription"
  subscription_id = "7a23d165-be95-4e5b-bb55-118372287e9e"
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

data "azurerm_servicebus_namespace" "cmn" {
  name                = "${local.resource_base_name}servicebus"
  resource_group_name = local.common_resource_group_name
}

data "azurerm_kubernetes_cluster" "envgroup" {
  name                = lower("${local.environmentgroup_base_name}aks")
  resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-AKS")
}

data "azurerm_resource_group" "aks_node_pool" {
  name = data.azurerm_kubernetes_cluster.envgroup.node_resource_group
}

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

# Store managed identity in the AKS Node Pool group
resource "azurerm_user_assigned_identity" "aks_app" {
  resource_group_name = data.azurerm_resource_group.aks_node_pool.name
  location            = data.azurerm_resource_group.aks_node_pool.location

  name = lower("${local.resource_base_name}aks${var.application_name}")

  tags = local.tags
  lifecycle {
    ignore_changes = [
      tags["creationdate"]
    ]
  }
}

# Allow the identity to read/validate its own role against the vmss in the aks node pool group
resource "azurerm_role_assignment" "appmsi_to_aksmc_reader" {
  scope                = data.azurerm_resource_group.aks_node_pool.id
  role_definition_name = "Reader"
  principal_id         = azurerm_user_assigned_identity.aks_app.principal_id
}

# Add App Service MSI to Key Vault Access Policy
resource "azurerm_key_vault_access_policy" "aks_app" {
  key_vault_id = data.azurerm_key_vault.master.id
  tenant_id    = data.azurerm_subscription.current.tenant_id
  object_id    = azurerm_user_assigned_identity.aks_app.principal_id

  secret_permissions = [
    "Get",
    "List"
  ]
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
