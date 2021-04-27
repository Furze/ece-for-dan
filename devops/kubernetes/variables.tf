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

variable "release_timestamp" {
  type        = string
  description = "Release timestamp in the following format: Tue 13 Apr 2021 12:22:56 NZST"
  default     = ""
}