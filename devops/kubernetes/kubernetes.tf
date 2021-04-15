resource "kubernetes_namespace" "app" {
  metadata {
    name = local.k8s_app_name
  }
}

resource "kubernetes_deployment" "app" {

  metadata {
    name      = local.k8s_app_name
    namespace = kubernetes_namespace.app.metadata.0.name
    labels = {
      app = local.k8s_app_name
    }
  }

  wait_for_rollout = true # Wait for timeout before failing for visibility

  spec {
    replicas = 1 # FIXME: Seems to fail when we have more than one

    selector {
      match_labels = {
        app = local.k8s_app_name
      }
    }

    template {
      metadata {
        labels = {
          app             = local.k8s_app_name
          aadpodidbinding = local.k8s_app_name
        }
      }

      spec {
        container {
          name              = local.k8s_app_name
          image             = var.container_image_source
          image_pull_policy = "Always" # Need this if we use the same "latest-*" tags

          resources {
            limits = {
              cpu    = "200m"
              memory = "300Mi"
            }
            requests = {
              cpu    = "100m"
              memory = "100Mi"
            }
          }

          liveness_probe {
            http_get {
              path = "/ping"
              port = 80
            }

            initial_delay_seconds = 10
            period_seconds        = 5
          }
          readiness_probe {
            http_get {
              path = "/health"
              port = 80
            }

            initial_delay_seconds = 30
            period_seconds        = 30
          }

          env_from {
            config_map_ref {
              name = local.k8s_app_name
            }
          }

          # Hack to make sure the pods are restarted on config map changes
          env {
            name  = "ReleaseDate"
            value = timestamp()
          }

          port {
            container_port = 80
          }
        }
      }
    }
  }

  # Pods should be ready within a few minutes
  timeouts {
    create = "3m"
    update = "3m"
  }
}

resource "kubernetes_service" "app" {
  metadata {
    name      = local.k8s_app_name
    namespace = kubernetes_namespace.app.metadata.0.name
  }
  spec {
    selector = {
      app = local.k8s_app_name
    }
    session_affinity = "ClientIP"
    port {
      port        = 8080
      target_port = 80
    }

    type = "ClusterIP"
  }
}

resource "kubernetes_ingress" "app" {
  metadata {
    name      = local.k8s_app_name
    namespace = kubernetes_namespace.app.metadata.0.name
    annotations = {
      "kubernetes.io/ingress.class"                       = "azure/application-gateway",
      "appgw.ingress.kubernetes.io/health-probe-path"     = "/health",
      "appgw.ingress.kubernetes.io/health-probe-timeout"  = "10",
      "appgw.ingress.kubernetes.io/ssl-redirect"          = "true",
      "appgw.ingress.kubernetes.io/appgw-ssl-certificate" = "subdomain-wildcard"
    }
  }

  spec {
    # BUG: AGIC issue confuses health probes with default backend set.
    # backend {
    #   service_name = local.k8s_app_name
    #   service_port = 8080
    # }

    rule {
      host = local.ingress_fqdn
      http {
        path {
          backend {
            service_name = local.k8s_app_name
            service_port = 8080
          }

          path = "/"
        }
      }
    }
  }
}

resource "kubernetes_config_map" "app" {
  metadata {
    name      = local.k8s_app_name
    namespace = kubernetes_namespace.app.metadata.0.name
  }

  data = {
    ASPNETCORE_ENVIRONMENT              = lower(replace(var.environment, "-", ""))
    APPINSIGHTS_INSTRUMENTATIONKEY      = data.azurerm_application_insights.cmn.instrumentation_key
    OidcSettings__Authority             = "https://${local.identity_ingress_fqdn}/"
    OidcSettings__Issuer                = "https://${local.identity_ingress_fqdn}/"
    KeyVault__Vault                     = data.azurerm_key_vault.env.name
    MartenSettings__ConnectionString    = "host=${local.db_server_endpoint};port=5432;database=${local.db_name};password={{PASSWORD_FROM_KEYVAULT}};username=${local.db_username}@${local.db_server_name};Pooling=true;Ssl Mode=Require;"
    OpaSettings__AuthorisationUrl       = local.opa_authorisation_url
    OpaSettings__RuleBaseUrl            = local.opa_rulebase_url
    ASPNETCORE_FORWARDEDHEADERS_ENABLED = "true"
    ConnectionStrings__ServiceBus       = data.azurerm_servicebus_namespace.cmn.default_primary_connection_string
  }
}

resource "kubectl_manifest" "pod_azureidentity" {
  override_namespace = kubernetes_namespace.app.metadata.0.name

  yaml_body = templatefile("${path.module}/templates/azureidentity.yml.tpl",
    {
      K8S_APP_NAME         = local.k8s_app_name,
      IDENTITY_RESOURCE_ID = data.azurerm_user_assigned_identity.app.id,
      IDENTITY_CLIENT_ID   = data.azurerm_user_assigned_identity.app.client_id
  })
}

resource "kubectl_manifest" "pod_azureidentity_binding" {
  override_namespace = kubernetes_namespace.app.metadata.0.name

  yaml_body = templatefile("${path.module}/templates/azureidentity-binding.yml.tpl",
    {
      K8S_APP_NAME = local.k8s_app_name,
  })
}

resource "kubernetes_network_policy" "app_egress_dns" {
  metadata {
    name      = "allow-egress-dns-policy"
    namespace = kubernetes_namespace.app.metadata.0.name
  }

  spec {
    pod_selector {
      # Apply to all pods in namespace
    }

    egress {
      ports {
        port     = "53"
        protocol = "UDP"
      }
      ports {
        port     = "53"
        protocol = "TCP"
      }
    }

    policy_types = ["Egress"]
  }
}

resource "kubernetes_network_policy" "app_egress_misc" {
  metadata {
    name      = "allow-egress-misc-policy"
    namespace = kubernetes_namespace.app.metadata.0.name
  }

  spec {
    pod_selector {
      # Apply to all pods in namespace
    }

    egress {
      # Keyvault, Service Bus, and other RESTful calls
      ports {
        port     = "443"
        protocol = "TCP"
      }
      # PostgreSQL
      ports {
        port     = "5432"
        protocol = "TCP"
      }
      # Service Bus AMQP
      ports {
        port     = "5671"
        protocol = "TCP"
      }
      ports {
        port     = "5672"
        protocol = "TCP"
      }
    }

    policy_types = ["Egress"]
  }
}

resource "kubernetes_network_policy" "app_ingress_http" {
  metadata {
    name      = "allow-ingress-http-policy"
    namespace = kubernetes_namespace.app.metadata.0.name
  }

  spec {
    pod_selector {
      # Apply to all pods in namespace
    }

    # Allow frontend and ingress controller
    ingress {
      ports {
        port     = "80"
        protocol = "TCP"
      }

      from {
        ip_block {
          cidr = data.azurerm_subnet.appgw_ingress.address_prefixes.0
        }
      }

      from {
        namespace_selector {
          match_labels = {
            role = local.kubernetes_frontend_namespace_label
          }
        }
      }
    }

    policy_types = ["Ingress"]
  }
}