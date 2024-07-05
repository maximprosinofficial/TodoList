FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["SimpleTodoList/SimpleTodoList.csproj", "SimpleTodoList/"]
RUN dotnet restore "SimpleTodoList/SimpleTodoList.csproj"
COPY . .
WORKDIR "/src/SimpleTodoList"
RUN dotnet build "SimpleTodoList.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SimpleTodoList.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SimpleTodoList.dll"]
