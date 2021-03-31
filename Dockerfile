FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.sln ./
COPY FitnessRecords/*.csproj ./FitnessRecords/
RUN dotnet restore

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
EXPOSE 5000
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "FitnessRecords.dll"]
