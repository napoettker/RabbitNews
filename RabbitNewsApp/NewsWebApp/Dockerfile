#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["NewsWebApp/RabbitNews.Web.csproj", "NewsWebApp/"]
COPY ["RabbitNews.Infrastructure/RabbitNews.Infrastructure.csproj", "RabbitNews.Infrastructure/"]
COPY ["RabbitNews.Domain/RabbitNews.Domain.csproj", "RabbitNews.Domain/"]
COPY ["Application/RabbitNews.Application.csproj", "Application/"]
RUN dotnet restore "NewsWebApp/RabbitNews.Web.csproj"
COPY . .
WORKDIR "/src/NewsWebApp"
RUN dotnet build "RabbitNews.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitNews.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitNews.Web.dll"]