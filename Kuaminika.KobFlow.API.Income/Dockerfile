# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Kuaminika.KobFlow.API.Income/Kuaminika.KobFlow.API.Income.csproj", "Kuaminika.KobFlow.API.Income/"]
COPY ["Kuaminika.KobFlow.IncomeService/Kuaminika.KobFlow.IncomeService.csproj", "Kuaminika.KobFlow.IncomeService/"]
COPY ["Kuaniminka.KobFlow.ToolBox/Kuaniminka.KobFlow.ToolBox.csproj", "Kuaniminka.KobFlow.ToolBox/"]
RUN dotnet restore "./Kuaminika.KobFlow.API.Income/Kuaminika.KobFlow.API.Income.csproj"
COPY . .
WORKDIR "/src/Kuaminika.KobFlow.API.Income"
RUN dotnet build "./Kuaminika.KobFlow.API.Income.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kuaminika.KobFlow.API.Income.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["Kuaminika.KobFlow.API.Income/appsettings-sample.json", "appsettings.json"]
ENTRYPOINT ["dotnet", "Kuaminika.KobFlow.API.Income.dll"]