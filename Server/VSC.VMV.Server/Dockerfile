FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

ARG BUILDNUM

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["VSC.VMV.Server/VSC.VMV.Server.csproj", "VSC.VMV.Server/VSC.VMV.Server/"]
RUN dotnet restore "VSC.VMV.Server/VSC.VMV.Server/VSC.VMV.Server.csproj"
COPY . .
WORKDIR "/src/VSC.VMV.Server/VSC.VMV.Server"
RUN dotnet build "VSC.VMV.Server.csproj" -c Release -o /app/build

ARG BUILDNUM
ENV BUILDNUMenv=${BUILDNUM}

FROM build AS publish
RUN dotnet publish "VSC.VMV.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false /p:Version=${BUILDNUMenv}

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VSC.VMV.Server.dll"]