apiVersion: "aadpodidentity.k8s.io/v1"
kind: AzureIdentity
metadata:
  name: "${K8S_APP_NAME}"
spec:
  type: 0
  resourceID: ${IDENTITY_RESOURCE_ID}
  clientID: ${IDENTITY_CLIENT_ID}