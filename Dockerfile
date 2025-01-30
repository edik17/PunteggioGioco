# Usa l'immagine ufficiale di .NET SDK per compilare l'app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia solo il file .csproj per ottimizzare la cache
COPY PunteggioGioco/PunteggioGioco.csproj ./PunteggioGioco/
WORKDIR /app/PunteggioGioco
RUN dotnet restore

# Copia il resto del progetto e compila
COPY PunteggioGioco/. ./PunteggioGioco/
RUN dotnet publish -c Release -o /publish

# Usa l'immagine runtime di .NET per eseguire l'app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .
CMD ["dotnet", "PunteggioGioco.dll"]
