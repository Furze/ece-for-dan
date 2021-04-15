terraform {
  required_providers {
    kubernetes = {
      source = "hashicorp/kubernetes"
    }
    azurerm = {
      source = "hashicorp/azurerm"
    }
    random = {
      source = "hashicorp/random"
    }
    kubectl = {
      source = "gavinbunney/kubectl"
    }
  }
}

provider "kubernetes" {
  # Configuration options
  #config_path = "~/.kube/config"
  host                   = data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.host
  client_certificate     = base64decode(data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.client_certificate)
  client_key             = base64decode(data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.client_key)
  cluster_ca_certificate = base64decode(data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.cluster_ca_certificate)
}

# Default provider that is based on the active subscription
provider "azurerm" {
  features {}
}

provider "random" {
}

provider "kubectl" {
  # Configuration options
  host                   = data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.host
  client_certificate     = base64decode(data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.client_certificate)
  client_key             = base64decode(data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.client_key)
  cluster_ca_certificate = base64decode(data.azurerm_kubernetes_cluster.aks.kube_admin_config.0.cluster_ca_certificate)
}