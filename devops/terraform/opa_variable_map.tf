variable "environment_to_opa_domain" {
  type = map(string)
  default = {
    "dev" = "https://moe-opa-test.custhelp.com/"
    "tst" = "https://moe-opa-test.custhelp.com/"
    "uat" = "https://moe-opa-uat.custhelp.com/"
    "ppd" = "https://moe-opa-uat.custhelp.com/"
    "pro" = "https://moe-opa.custhelp.com/"
  }
}

variable "environment_to_opa_rulebase" {
  type = map(string)
  default = {
    "dev" = "_DT2"
    "tst" = "_IT2"
    "uat" = "_UAT-PPD"
    "ppd" = "_UAT-PPD"
    "pro" = "_PR2"
  }
}

variable "environment_to_opa_rulebase_overrides" {
  type = map(string)
  default = {
    "DEV04" = "_IT2"
  }
}

locals {
  # Check the overrides first and if there are none, then use the default rulebase mapping
  opa_rulebase = lookup(var.environment_to_opa_rulebase_overrides, upper(replace(var.environment, "-", "")),
    lookup(var.environment_to_opa_rulebase, local.environment_name, "_UNMAPPED OPA ENVIRONMENT")
  )
  opa_authorisation_url = "${lookup(var.environment_to_opa_domain, local.environment_name, "UNMAPPED OPA DOMAIN")}opa-hub/api/auth"
  opa_rulebase_url      = "${lookup(var.environment_to_opa_domain, local.environment_name, "UNMAPPED OPA DOMAIN")}determinations-server/batch/12.2.7/policy-models/{rulebase}${local.opa_rulebase}/assessor/"
}