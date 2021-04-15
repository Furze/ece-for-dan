data "azurerm_kubernetes_cluster" "aks" {
  name                = lower("${local.environmentgroup_base_name}aks")
  resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-AKS")
}

data "azurerm_key_vault" "env" {
  name                = "${local.resource_base_name}secrets"
  resource_group_name = local.master_resource_group_name
}

data "azurerm_application_insights" "cmn" {
  name                = "${local.resource_base_name}appinsights"
  resource_group_name = local.common_resource_group_name
}

data "azurerm_user_assigned_identity" "app" {
  name                = local.aks_app_msi_name
  resource_group_name = data.azurerm_kubernetes_cluster.aks.node_resource_group
}

data "azurerm_subnet" "appgw_ingress" {
  name                 = "${local.environmentgroup_base_name}subnetingress"
  virtual_network_name = "${local.environmentgroup_base_name}vnet"
  resource_group_name  = local.master_resource_group_name
}

data "azurerm_servicebus_namespace" "cmn" {
  name                = "${local.resource_base_name}servicebus"
  resource_group_name = local.common_resource_group_name
}