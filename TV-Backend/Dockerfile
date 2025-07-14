 # 1. Build aşaması: Kodun derlenmesi
   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
   WORKDIR /app

   # Proje dosyalarını kopyala ve restore et
   COPY . ./
   RUN dotnet restore "TV-Backend.csproj"
   RUN dotnet publish "TV-Backend.csproj" -c Release -o out

   # 2. Runtime aşaması: Sadece çalıştırmak için gerekli dosyalar
   FROM mcr.microsoft.com/dotnet/aspnet:8.0
   WORKDIR /app
   COPY --from=build /app/out ./

   # Uygulamayı başlat
   ENTRYPOINT ["dotnet", "TV-Backend.dll"]