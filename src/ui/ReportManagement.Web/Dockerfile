#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ui/ReportManagement.Web/ReportManagement.Web.csproj", "src/ui/ReportManagement.Web/"]
RUN dotnet restore "src/ui/ReportManagement.Web/ReportManagement.Web.csproj"
COPY . .
WORKDIR "/src/src/ui/ReportManagement.Web"
RUN dotnet build "ReportManagement.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ReportManagement.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ReportManagement.Web.dll"]