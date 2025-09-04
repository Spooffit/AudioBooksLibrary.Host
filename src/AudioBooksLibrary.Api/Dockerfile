FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AudioBooksLibrary.Api/AudioBooksLibrary.Api.csproj", "src/AudioBooksLibrary.Api/"]
COPY ["src/AudioBooksLibrary.Application/AudioBooksLibrary.Application.csproj", "src/AudioBooksLibrary.Application/"]
COPY ["src/AudioBooksLibrary.Application.Abstractions/AudioBooksLibrary.Application.Abstractions.csproj", "src/AudioBooksLibrary.Application.Abstractions/"]
COPY ["src/AudioBooksLibrary.Core/AudioBooksLibrary.Core.csproj", "src/AudioBooksLibrary.Core/"]
COPY ["src/AudioBooksLibrary.Infrastructure/AudioBooksLibrary.Infrastructure.csproj", "src/AudioBooksLibrary.Infrastructure/"]
RUN dotnet restore "src/AudioBooksLibrary.Api/AudioBooksLibrary.Api.csproj"
COPY . .
WORKDIR "/src/src/AudioBooksLibrary.Api"
RUN dotnet build "./AudioBooksLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AudioBooksLibrary.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AudioBooksLibrary.Api.dll"]
