FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS sdk
WORKDIR /app
COPY . ./
RUN dotnet publish ./sauron/src/Sauron/Sauron.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS runtime
WORKDIR /app
RUN mkdir /https
COPY --from=sdk /app/https/certificate.p12 /https/certificate.p12
COPY --from=sdk /app/out .

ENTRYPOINT ["dotnet", "Sauron.dll"]