#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Aohost.Blog.HttpApi.Hosting/Aohost.Blog.HttpApi.Hosting.csproj", "src/Aohost.Blog.HttpApi.Hosting/"]
COPY ["src/Aohost.Blog.EntityFrameworkCore.DbMigrations/Aohost.Blog.EntityFrameworkCore.DbMigrations.csproj", "src/Aohost.Blog.EntityFrameworkCore.DbMigrations/"]
COPY ["src/Aohost.Blog.EntityFrameworkCore/Aohost.Blog.EntityFrameworkCore.csproj", "src/Aohost.Blog.EntityFrameworkCore/"]
COPY ["src/Aohost.Blog.Domain/Aohost.Blog.Domain.csproj", "src/Aohost.Blog.Domain/"]
COPY ["src/Aohost.Blog.Domain.Shared/Aohost.Blog.Domain.Shared.csproj", "src/Aohost.Blog.Domain.Shared/"]
COPY ["src/Aohost.Blog.BackgroundJobs/Aohost.Blog.BackgroundJobs.csproj", "src/Aohost.Blog.BackgroundJobs/"]
COPY ["src/Aohost.Blog.Application.Contracts/Aohost.Blog.Application.Contracts.csproj", "src/Aohost.Blog.Application.Contracts/"]
COPY ["src/Aohost.Blog.ToolKits/Aohost.Blog.ToolKits.csproj", "src/Aohost.Blog.ToolKits/"]
COPY ["src/Aohost.Blog.Application/Aohost.Blog.Application.csproj", "src/Aohost.Blog.Application/"]
COPY ["src/Aohost.Blog.Caching/Aohost.BlogApplication.Caching.csproj", "src/Aohost.Blog.Caching/"]
COPY ["src/Aohost.Blog.HttpApi/Aohost.Blog.HttpApi.csproj", "src/Aohost.Blog.HttpApi/"]
COPY ["src/Aohost.Blog.Swagger/Aohost.Blog.Swagger.csproj", "src/Aohost.Blog.Swagger/"]
RUN dotnet restore "src/Aohost.Blog.HttpApi.Hosting/Aohost.Blog.HttpApi.Hosting.csproj"
COPY . .
WORKDIR "/src/src/Aohost.Blog.HttpApi.Hosting"
RUN dotnet build "Aohost.Blog.HttpApi.Hosting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aohost.Blog.HttpApi.Hosting.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aohost.Blog.HttpApi.Hosting.dll"]