locals {
  k8s_app_name               = lower("${local.resource_base_name}${var.application_name}")
  aks_app_msi_name           = lower("${local.resource_base_name}aks${var.application_name}")
  resource_base_name         = lower("${var.environment_prefix}ers${replace(var.environment, "-", "")}")
  environmentgroup_base_name = lower("${var.environment_prefix}ers${var.environment_suffix}")
  master_resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-MASTER")
  common_resource_group_name = upper("${var.environment_prefix}-ERS-${var.environment}-COMMON")
  environment_number         = lower(split("-", var.environment)[1])
  environment_name           = lower(split("-", var.environment)[0])

  # No prefix for prod, everything else we strip out the 0, so dev03 -> dev3
  short_ingress_url_prefix = lower(local.environment_name) == "pro" ? "" : "${local.environment_name}${trimprefix(local.environment_number, "0")}"

  ingress_fqdn = lower("ece${local.short_ingress_url_prefix}.${var.kubernetes_ingress_domain_suffix}")

  default_internal_ingress_fqdn = lower("int${local.short_ingress_url_prefix}.${var.kubernetes_ingress_domain_suffix}")
  default_external_ingress_fqdn = lower("${local.short_ingress_url_prefix}.${var.kubernetes_ingress_domain_suffix}")
  apigw_ingress_fqdn            = lower("api${local.short_ingress_url_prefix}.${var.kubernetes_ingress_domain_suffix}")
  identity_ingress_fqdn         = lower("identity${local.short_ingress_url_prefix}.${var.kubernetes_ingress_domain_suffix}")
  opaserver_ingress_fqdn        = lower("opaserver${local.short_ingress_url_prefix}.${var.kubernetes_ingress_domain_suffix}")

  kubernetes_frontend_namespace_label = lower("${local.resource_base_name}frontend") # This label must be added to any "frontend" namspaces

  db_server_resource_group = upper("${var.environment_prefix}-ERS-${var.environment_suffix}-PSQL-DATABASES")
  db_server_name           = lower("${var.environment_prefix}ers${var.environment_suffix}psqlserver")
  db_server_endpoint       = "${local.db_server_name}.postgres.database.azure.com"
  db_username              = lower("${local.resource_base_name}${var.application_name}")     # mataersdev00identity
  db_name                  = lower("${local.resource_base_name}psql${var.application_name}") # mataersdev00psqlidentity  


  # Application Configuration Environment Mapping

  environment_to_opa_domain = {
    dev = "https://moe-opa-test.custhelp.com/"
    tst = "https://moe-opa-test.custhelp.com/"
    uat = "https://moe-opa-uat.custhelp.com/"
    ppd = "https://moe-opa-uat.custhelp.com/"
    pro = "https://moe-opa.custhelp.com/"
  }

  environment_to_opa_rulebase = {
    dev = "_DT2"
    tst = "_IT2"
    uat = "_UAT-PPD"
    ppd = "_UAT-PPD"
    pro = "_PR2"
  }

  environment_to_opa_rulebase_overrides = {
    DEV04 = "_IT2"
  }

  # Check the overrides first and if there are none, then use the default rulebase mapping
  opa_rulebase = lookup(local.environment_to_opa_rulebase_overrides, upper(replace(var.environment, "-", "")),
    lookup(local.environment_to_opa_rulebase, local.environment_name, "_UNMAPPED OPA ENVIRONMENT")
  )
  opa_authorisation_url = "${lookup(local.environment_to_opa_domain, local.environment_name, "UNMAPPED OPA DOMAIN")}opa-hub/api/auth"
  opa_rulebase_url      = "${lookup(local.environment_to_opa_domain, local.environment_name, "UNMAPPED OPA DOMAIN")}determinations-server/batch/12.2.7/policy-models/{rulebase}${local.opa_rulebase}/assessor/"
}

