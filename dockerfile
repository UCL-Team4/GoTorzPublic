FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY GoTorz/GoTorz.csproj ./GoTorz/
WORKDIR /src/GoTorz

RUN dotnet restore

WORKDIR /src
COPY . .

WORKDIR /src/GoTorz
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "GoTorz.dll"]
