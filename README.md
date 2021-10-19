# DemoApplication

## Setup azure devops

- API token for agent
- Variable for octopus
- Docker service connection (Port: 5000)

## Azure devops server self-hosted agent

Build agent:
```
docker build -t registry.docker.local:5000/vsts-agent .
docker push registry.docker.local:5000/vsts-agent
```

Start docker agent **(This in the docker-compose as well)**:
```
docker run -e AZP_URL="https://dev-ops.azure.local/DefaultCollection" -e AZP_TOKEN="mpxqau7neidswiurn7hmmznvkufhvdttbvf54nvbnsyi6svxw3ia" -e AZP_AGENT_NAME=dockeragent -e AZP_POOL=Self-Hosted -e AZP_AGENT_DOWNGRADE_DISABLED=true -d -v /var/run/docker.sock:/var/run/docker.sock --network=repo_default --privileged --name dockeragent dockeragent:latest
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

## Create service account

To create the service account for octopus execute the following command in the octopus folder:
```
kubectl apply -f .\serviceaccount.yaml
```

## Octopus tencale

Can be used as a worker:
```
docker run --publish 10931:10933 --tty --interactive --network="repo_default" --env "ListeningPort=10931" --env "ServerApiKey=API-ZTEOSMZPURXPFBF6CKDLI7WNUKDR8A" --env "TargetEnvironment=Dev" --env "TargetRole=app-server" --env "ServerUrl=http://repo-octopus-server-1:8080" --env "ACCEPT_EULA=Y" octopusdeploy/tentacle
```

## Octopus retention
Runs every four hours. Can be adjust on the lifecycle or and each individual phase.