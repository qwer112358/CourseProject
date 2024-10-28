FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CourseProject.Presentation/CourseProject.Presentation.csproj", "CourseProject.Presentation/"]
COPY ["CourseProject.Application/CourseProject.Application.csproj", "CourseProject.Application/"]
COPY ["CourseProject.Domain/CourseProject.Domain.csproj", "CourseProject.Domain/"]
COPY ["CourseProject.DataAccess/CourseProject.DataAccess.csproj", "CourseProject.DataAccess/"]
RUN dotnet restore "CourseProject.Presentation/CourseProject.Presentation.csproj"
COPY . .
WORKDIR "/src/CourseProject.Presentation"
RUN dotnet build "CourseProject.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "CourseProject.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CourseProject.Presentation.dll"]
