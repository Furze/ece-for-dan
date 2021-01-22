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

variable "environment_to_opa_rulebase" {
  type = map(string)
  default = {
    "dev" = "_DT2"
    "tst" = "_IT2"
    "uat" = "_UT2"
    "ppr" = "_PP2"
    "pro" = "_PR2"
  }
}

variable "environment_to_opa_rulebase_overrides" {
  type = map(string)
  default = {
    "UAT00" = "_UT2",
    "UAT03" = "_UT3",
    "UAT04" = "_UT4",
    "UAT05" = "_UT5",
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