FROM stuartferguson/txnprocbase:latest AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src
COPY ["EstateAdministrationUI/NuGet.Config", "."]
COPY ["EstateAdministrationUI/EstateAdministrationUI.csproj", "EstateAdministrationUI/"]
COPY ["EstateAdministrationUI.BusinessLogic/EstateAdministrationUI.BusinessLogic.csproj", "EstateAdministrationUI.BusinessLogic/"]
RUN dotnet restore "EstateAdministrationUI/EstateAdministrationUI.csproj"
COPY . .
WORKDIR "/src/EstateAdministrationUI"
RUN dotnet build "EstateAdministrationUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EstateAdministrationUI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EstateAdministrationUI.dll"]