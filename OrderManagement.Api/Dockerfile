#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["OrderManagement/OrderManagement.Api.csproj", "OrderManagement/"]
RUN dotnet restore "OrderManagement/OrderManagement.Api.csproj"
COPY . .
WORKDIR "/src/OrderManagement"
RUN dotnet build "OrderManagement.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "OrderManagement.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderManagement.Api.dll"]