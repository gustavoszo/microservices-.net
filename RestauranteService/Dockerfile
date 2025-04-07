FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY RestauranteService.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish "RestauranteService.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5101

ENTRYPOINT [ "dotnet", "RestauranteService.dll" ]
