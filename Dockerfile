#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Aohost.Blog.HttpApi.Hosting/Aohost.Blog.HttpApi.Hosting.csproj", "Aohost.Blog.HttpApi.Hosting/"]
COPY ["Aohost.Blog.EntityFrameworkCore.DbMigrations/Aohost.Blog.EntityFrameworkCore.DbMigrations.csproj", "Aohost.Blog.EntityFrameworkCore.DbMigrations/"]
COPY ["Aohost.Blog.EntityFrameworkCore/Aohost.Blog.EntityFrameworkCore.csproj", "Aohost.Blog.EntityFrameworkCore/"]
COPY ["Aohost.Blog.Domain/Aohost.Blog.Domain.csproj", "Aohost.Blog.Domain/"]
COPY ["Aohost.Blog.Domain.Shared/Aohost.Blog.Domain.Shared.csproj", "Aohost.Blog.Domain.Shared/"]
COPY ["Aohost.Blog.BackgroundJobs/Aohost.Blog.BackgroundJobs.csproj", "Aohost.Blog.BackgroundJobs/"]
COPY ["Aohost.Blog.Application.Contracts/Aohost.Blog.Application.Contracts.csproj", "Aohost.Blog.Application.Contracts/"]
COPY ["Aohost.Blog.ToolKits/Aohost.Blog.ToolKits.csproj", "Aohost.Blog.ToolKits/"]
COPY ["Aohost.Blog.Application/Aohost.Blog.Application.csproj", "Aohost.Blog.Application/"]
COPY ["Aohost.Blog.Caching/Aohost.BlogApplication.Caching.csproj", "Aohost.Blog.Caching/"]
COPY ["Aohost.Blog.HttpApi/Aohost.Blog.HttpApi.csproj", "Aohost.Blog.HttpApi/"]
COPY ["Aohost.Blog.Swagger/Aohost.Blog.Swagger.csproj", "Aohost.Blog.Swagger/"]
RUN dotnet restore "Aohost.Blog.HttpApi.Hosting/Aohost.Blog.HttpApi.Hosting.csproj"
COPY . .
WORKDIR "/src/Aohost.Blog.HttpApi.Hosting"
RUN dotnet build "Aohost.Blog.HttpApi.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aohost.Blog.HttpApi.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aohost.Blog.HttpApi.Hosting.dll"]