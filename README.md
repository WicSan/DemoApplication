# DemoApplication

## Azure devops server self-hosted agent

Build agent:
```
docker build -t dockeragent:latest .
```

Start docker agent **(This in the docker-compose as well)**:
```
docker run -e AZP_URL="https://dev-ops.azure.local/DefaultCollection" -e AZP_TOKEN="iilxozaek4ycx6j4ixnyei7sz2jej6gzcxvqzscluz4obyzl3ktq" -e AZP_AGENT_NAME=dockeragent -e AZP_POOL=Self-Hosted -e AZP_AGENT_DOWNGRADE_DISABLED=true -d -v /var/run/docker.sock:/var/run/docker.sock --privileged --name dockeragent dockeragent:latest
```

[Link to source](https://docs.microsoft.com/en-us/azure/devops/pipelines/agents/docker?view=azure-devops)

## Docker registry setup

Run following command to create the password file:
```
docker run --rm --entrypoint htpasswd httpd:2 -Bbn user Passw0rd! | Set-Content -Encoding ASCII auth/htpasswd
docker container stop registry
```

Creating certificate without password:
```
openssl req -nodes -new -x509 -keyout registry.key -out registry.crt -days 365
```

Login to registry:
```
{
    UserName: "user",
    Password: "Passw0rd!"
}
```

[Link to source](https://docs.docker.com/registry/deploying/)

## Setup kubernetes cluster

Add the secret for the docker registry:
```
kubectl create secret docker-registry regcred --docker-server=localhost:5000 --docker-username=user --docker-password=Passw0rd!
```

Enable ingress to be able to access the apps:
```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.0.3/deploy/static/provider/cloud/deploy.yaml
```