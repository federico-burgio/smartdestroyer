# SmartDestroyer App
This app was created for the Microservices class @ UniMe.

## Build docker image
### Login into docker registry (you need an account @ hub.docker.com)
docker login

### Build the image
docker build . --rm -f Dockerfile -t smartdestroyer:latest

### Tag it and send it to the registry
docker tag smartdestroyer smartdestroyer:latest & docker push smartdestroyer:latest

### Run on your local docker
docker run -d -p 5000:5000 smartdestroyer:latest

## Run it with Minikube
### Create deployment
kubectl run smartdestroyer smartdestroyer:latest --port=5000

### Create a service for the deployment
kubectl expose deployment smartdestroyer --type=NodePort

### Get the url of the service
minikube service smartdestroyer --url

## Useful Links
### Docker
https://www.docker.com

### Docker Hub - Official Registry
https://hub.docker.com

### Visual Studio Code
https://code.visualstudio.com

## Credits
Federico Burgio - Lead Architect @ BaxEnergy
http://www.baxenergy.com
http://www.freemindfoundry.com