variable "location" {
  type        = string
  description = ""
  default     = "australiaeast"
}

variable "environment" {
  type        = string
  description = "Environment identifier. Eg. DEV, SIT."
  default     = "DEV-00"
}

# POSTGRES DATABASE
variable "psql_server_version" {
  type        = string
  description = ""
  default     = "10.0"
}

variable "sql_charset" {
  type        = string
  description = "View postgres documentation for valid options"
  default     = "UTF8"
}

variable "sql_collation" {
  type        = string
  description = "View postgres documentation for valid options"
  default     = "English_United States.1252"
}

# APP SERVICE
variable "application_name" {
  type        = string
  description = "ECE API Service"
  default     = "ECE"
}

variable "azurerm_container_registry_name" {
  type        = string
  description = ""
  default     = "mapaerscontainerregistry"
}

variable "azurerm_container_registry_resource_group_name" {
  type        = string
  description = ""
  default     = "MAPA-ERS-CONTAINERREGISTRY"
}

variable "actiongroup_name" {
  type        = string
  description = ""
  default     = "NonProd-ActionGroup"
}

variable "actiongroup_resource_group" {
  type        = string
  description = ""
  default     = "MATA-ERS-0-0-COMMON"
}

variable "environment_prefix" {
  type        = string
  description = "MATA or MAPA"
  default     = "MATA"
}

variable "environment_suffix" {
  type        = string
  description = "DEVTEST, PRODUAT or PROD"
  default     = "DEVTEST"
}

variable "acr_subscriptionid" {
  type        = string
  description = "The subscription ID used to identify where ACR lives"
  default     = "7a23d165-be95-4e5b-bb55-118372287e9e"
}

variable "decom_date" {
  type        = string
  description = "Date for which resource should be deleted. Format is YYYY-MM-DD. ie. 2020-11-21."
  default     = "" # If left empty then default will be set to 2 weeks from creation unless specified. If in MAPA sub then will be set to never expire (date very far in future) 
}

variable "TAG_CONTAINS_FUNDING_INFORMATION" {
  type        = string
  description = "Contains funding information?"
  default     = "true"
}

variable "TAG_CONTAINS_PERSONALLY_IDENTIFIABLE_INFORMATION" {
  type        = string
  description = "Contains personally identifiable information?"
  default     = "true"
}

variable "TAG_IS_PUBLIC_FACING" {
  type        = string
  description = "Is this a public facing app?"
  default     = "true"
}

variable "TAG_DATA_CLASSIFICATION" {
  type        = string
  description = "Data classification of the resource"
  default     = "SENSITIVE"
}

variable "TAG_PRIVACY_RATING" {
  type        = string
  description = "Privacy rating of the resource"
  default     = ""
}

variable "TAG_COSTCODE" {
  type        = string
  description = "Tag cost code for resource"
  default     = "8008"
}

variable "TAG_DEPARTMENT" {
  type        = string
  description = "Department for the application"
  default     = "SE&S"
}

variable "TAG_APPLICATION" {
  type        = string
  description = "Abbreviation of application"
  default     = "ERS"
}

variable "TAG_BUSCONTACT" {
  type        = string
  description = "Business Contact/Owner for the product"
  default     = "Steve.Botica@education.govt.nz"
}

variable "TAG_TECCONTACT" {
  type        = string
  description = "Technical contact for the product"
  default     = "Graeme.Davies@education.govt.nz"
}