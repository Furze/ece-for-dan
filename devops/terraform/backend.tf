terraform {
  backend "azurerm" {
    resource_group_name  = "MATA-ERS-DEVTEST-TFSTATE"
    storage_account_name = "mataersdevtesttfstate"
    container_name       = "tfstate-dev-00"
    key                  = "ece"
  }
}