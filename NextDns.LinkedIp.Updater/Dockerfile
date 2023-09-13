FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["NextDns.LinkedIp.Updater/NextDns.LinkedIp.Updater.csproj", "NextDns.LinkedIp.Updater/"]
RUN dotnet restore "NextDns.LinkedIp.Updater/NextDns.LinkedIp.Updater.csproj"
COPY . .
WORKDIR "/src/NextDns.LinkedIp.Updater"
RUN dotnet build "NextDns.LinkedIp.Updater.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NextDns.LinkedIp.Updater.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NextDns.LinkedIp.Updater.dll"]
