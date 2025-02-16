FROM mcr.microsoft.com/dotnet/aspnet:8.0-bookworm-slim AS base
USER $APP_UID
WORKDIR /app

# build
FROM mcr.microsoft.com/dotnet/sdk:8.0-bookworm-slim AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /TalentHub.ResumeService

COPY ["./ResumeApi/ResumeApi.csproj", "TalentHub.ResumeService/ResumeApi/"]
COPY ["./ResumeBll/ResumeBll.csproj", "TalentHub.ResumeService/ResumeBll"]
COPY ["./ResumeDataAccess/ResumeDataAccess.csproj", "TalentHub.ResumeService/ResumeDataAccess/"]
RUN dotnet restore "TalentHub.ResumeService/ResumeApi/ResumeApi.csproj"
COPY . .
WORKDIR "/TalentHub.ResumeService/ResumeApi"
RUN dotnet build "ResumeApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ResumeApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://+:5001
EXPOSE 5001
ENTRYPOINT ["dotnet", "ResumeApi.dll"]