apiVersion: "aadpodidentity.k8s.io/v1"
kind: AzureIdentityBinding
metadata:
  name: "${K8S_APP_NAME}-binding"
spec:
  azureIdentity: "${K8S_APP_NAME}"
  selector: "${K8S_APP_NAME}"