FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Services/CancunHotel.Services.API/CancunHotel.Services.API.csproj", "CancunHotel.Services.API/"]
RUN dotnet restore "src/Services/CancunHotel.Services.API/CancunHotel.Services.API.csproj"
COPY . .
WORKDIR "/src/CancunHotel.Services.API"
RUN dotnet build "CancunHotel.Services.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CancunHotel.Services.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CancunHotel.Services.API.dll"]
