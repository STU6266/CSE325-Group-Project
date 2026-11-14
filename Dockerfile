FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build

WORKDIR /src

COPY ReadingTracker/ReadingTracker.csproj ReadingTracker/

RUN dotnet restore ReadingTracker/ReadingTracker.csproj

COPY ReadingTracker/ ReadingTracker/

RUN dotnet publish ReadingTracker/ReadingTracker.csproj \
    -c Release \
    -o /publish \
    --no-restore \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0

WORKDIR /app

COPY --from=build /publish .

ENV ASPNETCORE_ENVIRONMENT=Production

EXPOSE 8080

ENTRYPOINT ["/bin/sh", "-c", "exec dotnet ReadingTracker.dll --urls http://0.0.0.0:${PORT:-8080}"]
