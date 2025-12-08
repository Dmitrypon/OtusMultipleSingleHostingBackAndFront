
# Используем официальный образ .NET SDK для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

RUN apt-get update && apt-get install -y nodejs npm

# Копируем csproj и восстанавливаем зависимости
COPY *.csproj ./
RUN dotnet restore



RUN npm -v
RUN node -v
# Копируем все файлы и собираем приложение
COPY . ./
RUN npm install

WORKDIR /app
RUN dotnet publish -o out -c Release

# Используем официальный образ .NET Runtime для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out ./

# Указываем порт, который будет использоваться приложением
EXPOSE 80

# Запускаем приложение
ENTRYPOINT ["dotnet", "AspReact.dll"]