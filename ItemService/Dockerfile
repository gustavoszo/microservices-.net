FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY ItemService.csproj ./
RUN dotnet restore

COPY . ./
RUN dotnet publish "ItemService.csproj" -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 5001

ENTRYPOINT [ "dotnet", "ItemService.dll" ]
