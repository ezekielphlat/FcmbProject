#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["FcmbInterview.Api/FcmbInterview.Api.csproj", "FcmbInterview.Api/"]
COPY ["FcmbInterview.Application/FcmbInterview.Application.csproj", "FcmbInterview.Application/"]
COPY ["FcmbInterview.Domain/FcmbInterview.Domain.csproj", "FcmbInterview.Domain/"]
COPY ["FcmbInterview.Contract/FcmbInterview.Contract.csproj", "FcmbInterview.Contract/"]
COPY ["FcmbInterview.Infrastructure/FcmbInterview.Infrastructure.csproj", "FcmbInterview.Infrastructure/"]
RUN dotnet restore "FcmbInterview.Api/FcmbInterview.Api.csproj"
COPY . .
WORKDIR "/src/FcmbInterview.Api"
RUN dotnet build "FcmbInterview.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FcmbInterview.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FcmbInterview.Api.dll"]