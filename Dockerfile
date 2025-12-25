# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["SistemaChamados.Api/SistemaChamados.Api.csproj", "SistemaChamados.Api/"]
RUN dotnet restore "SistemaChamados.Api/SistemaChamados.Api.csproj"

COPY . .
WORKDIR "/src/SistemaChamados.Api"
RUN dotnet build "SistemaChamados.Api.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "SistemaChamados.Api.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "SistemaChamados.Api.dll"]
