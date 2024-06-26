# ASP.NET Core Web API with Angular 18 - Docker Compose

This is a simple example of how to create a Web API with ASP.NET Core and Angular 18, and run it in a Docker container using Docker Compose.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [Angular CLI](https://angular.io/cli)
- [Visual Studio Code](https://code.visualstudio.com/) (optional)
- [Docker extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-azuretools.vscode-docker) (optional)

## Getting Started

1. Clone the repository:

```bash
git clone
```

2. Run without Docker;

```bash
cd src/server
dotnet run
```

```bash
cd src/client
npm install
ng s -o
```

3. Run directly with Docker;

```bash
cd src/server
docker build -t todo-server .
docker run -d -p 8080:80 --name todo-server todo-server
```

```bash
cd src/client
docker build -t todo-client .
docker run -d -p 4200:80 --name todo-client todo-client
```

4. Run with Docker Compose;

```bash
## Remove unused and dangling images
docker image prune --all
## Run this command from the directory where you have the "docker-compose-build.yaml" file present
docker-compose -f docker-compose-build.yaml build --parallel
```

```bash
docker-compose up -d
```

5. Open your browser and navigate to `http://localhost:4200/` to access the Angular client application.
6. Open your browser and navigate to `http://localhost:8080/swagger` to access the Swagger UI for the Web API.