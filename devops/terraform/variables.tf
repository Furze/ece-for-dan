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

variable "container_image_source" {
  type        = string
  description = "Container Registry Endpoint"
  default     = "mapaerscontainerregistry.azurecr.io/eceapi:latest"
}

variable "release_timestamp" {
  type        = string
  description = "Release timestamp in the following format: Tue 13 Apr 2021 12:22:56 NZST"
  default     = ""
}