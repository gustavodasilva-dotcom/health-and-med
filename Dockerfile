FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ./ ./
RUN dotnet restore 

RUN dotnet publish ./src/App/App.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0

RUN apt-get update && apt-get install -y libkrb5-dev && rm -rf /var/lib/apt/lists/*
RUN apt-get update && apt-get install -y krb5-user && rm -rf /var/lib/apt/lists/*


WORKDIR /app
COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "App.dll"]
