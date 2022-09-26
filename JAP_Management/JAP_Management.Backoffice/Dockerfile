#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["JAP_Management.Backoffice/JAP_Management.Backoffice.csproj", "JAP_Management.Backoffice/"]
RUN dotnet restore "JAP_Management.Backoffice/JAP_Management.Backoffice.csproj"
COPY . .
WORKDIR "/src/JAP_Management.Backoffice"
RUN dotnet build "JAP_Management.Backoffice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JAP_Management.Backoffice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JAP_Management.Backoffice.dll"]