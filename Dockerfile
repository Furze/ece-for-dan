FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
LABEL forBuild=true
WORKDIR /src
COPY . .
ARG buildno
ARG ERSNUGETFEED_ACCESSTOKEN

ENV VSS_NUGET_EXTERNAL_FEED_ENDPOINTS \
    "{\"endpointCredentials\": [{\"endpoint\":\"https://moeedunz.pkgs.visualstudio.com/ERS/_packaging/api-library/nuget/v3/index.json\", \"username\":\"docker\", \"password\":\"${ERSNUGETFEED_ACCESSTOKEN}\"}]}"

RUN curl -L https://raw.githubusercontent.com/Microsoft/artifacts-credprovider/master/helpers/installcredprovider.sh  | bash

RUN dotnet restore "Web/Web.csproj"
RUN dotnet build "Web/Web.csproj" -c Release -o /app 
RUN dotnet restore "CLI/CLI.csproj"
RUN dotnet build "CLI/CLI.csproj" -c Release -o /cli

FROM build AS publish
ARG buildno
RUN dotnet publish "Web/Web.csproj" -c Release -o /app /p:AssemblyVersion=$buildno /p:AssemblyVersion=$buildno 
RUN dotnet publish "CLI/CLI.csproj" -c Release -o /cli /p:AssemblyVersion=$buildno /p:AssemblyVersion=$buildno

FROM base AS final
ARG buildno
ENV VERSION_TAG=$buildno
WORKDIR /cli
COPY --from=publish /cli .
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Web.dll"]