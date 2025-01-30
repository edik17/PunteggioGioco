# Usa l'immagine ufficiale di .NET SDK per compilare l'app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia il file csproj e ripristina le dipendenze
COPY *.csproj ./
RUN dotnet restore

# Copia il resto del progetto e compila
COPY . ./
RUN dotnet publish -c Release -o /publish

# Usa l'immagine runtime di .NET per eseguire l'app
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .
CMD ["dotnet", "TopSecret.dll"]
