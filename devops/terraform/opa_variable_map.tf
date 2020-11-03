variable "environment_to_opa_domain" {
  type = map(string)
  default = {
    "dev" = "https://moe-opa-test.custhelp.com/"
    "tst" = "https://moe-opa-test.custhelp.com/"
    "uat" = "https://moe-opa-uat.custhelp.com/"
    "ppr" = "https://moe-opa-uat.custhelp.com/"
    "prd" = "https://moe-opa.custhelp.com/"
  }
}

variable "environment_to_opa_environment" {
  type = map(string)
  default = {
    "dev" = "_DT"
    "tst" = "_IT"
    "uat" = "_UT"
    "ppr" = "_PP"
    "prd" = "_PR"
  }
}

variable "environment_number_to_opa_environment_number_overrides" {
  type = map(string)
  default = {
    "00" = "2"
  }
}

locals {
  opa_authorisation_url = "${lookup(var.environment_to_opa_domain, local.environment_name, "UNMAPPED OPA DOMAIN")}opa-hub/api/auth"
  opa_rulebase_url      = "${lookup(var.environment_to_opa_domain, local.environment_name, "UNMAPPED OPA DOMAIN")}determinations-server/batch/12.2.7/policy-models/{rulebase}${lookup(var.environment_to_opa_environment, local.environment_name, "_UNMAPPED OPA ENVIRONMENT")}${lookup(var.environment_number_to_opa_environment_number_overrides, local.environment_number, parseint(local.environment_number, 10))}/assessor/"
}