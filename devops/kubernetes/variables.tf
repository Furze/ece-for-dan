variable "location" {
  type        = string
  description = ""
  default     = "australiaeast"
}

variable "environment" {
  type        = string
  description = "Environment identifier. Eg. EXP-01, DEV-00, PPD-01, PRO-01."
  default     = "DEV-00"
}

# APP SERVICE
variable "application_name" {
  type        = string
  description = "eg. Rolls, FIRST"
  default     = "ece"
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

variable "container_image_source" {
  type        = string
  description = "Container Registry Endpoint"
  default     = "mapaerscontainerregistry.azurecr.io/eceapi:latest"
}

variable "kubernetes_ingress_domain_suffix" {
  type        = string
  description = "Domain suffix for the ingress"
  default     = "devtest.ers.education.govt.nz"
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

variable "decom_date" {
  type        = string
  description = "Date for which resource should be deleted. Format is YYYY-MM-DD. ie. 2020-11-21."
  default     = "" # If left empty then default will be set to 2 weeks from creation unless specified. If in MAPA sub then will be set to never expire (date very far in future) 
}