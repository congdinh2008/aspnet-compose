# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /source

COPY *.sln .
# copy csproj and restore as distinct layers
COPY **/*.csproj .
COPY . .
RUN dotnet restore "TodoApp.WebAPI" -a $TARGETARCH

# copy and publish app and libraries
COPY TodoApp.WebAPI/. .
RUN dotnet publish "TodoApp.WebAPI" -a $TARGETARCH --no-restore -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
EXPOSE 8080
WORKDIR /app
COPY --from=build /app .
USER $APP_UID
ENTRYPOINT ["./TodoApp.WebAPI"]