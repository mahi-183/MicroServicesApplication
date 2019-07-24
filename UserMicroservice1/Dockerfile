FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["UserMicroservice1/UserMicroservice1.csproj", "UserMicroservice1/"]
RUN dotnet restore "UserMicroservice1/UserMicroservice1.csproj"
COPY . .
WORKDIR "/src/UserMicroservice1"
RUN dotnet build "UserMicroservice1.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "UserMicroservice1.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "UserMicroservice1.dll"]