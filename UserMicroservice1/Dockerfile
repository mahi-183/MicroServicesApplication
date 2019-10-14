#FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM microsoft/dotnet:2.2-sdk AS build
#WORKDIR /src
#COPY UserMicroservice1/UserMicroservice.csproj UserMicroservice1/
#COPY UserBusinessManager/UserBusinessManager.csproj UserBusinessManager/
#COPY UserModel/UserModel.csproj UserModel/
#COPY UserRepositoryManager/UserRepositoryManager.csproj UserRepositoryManager/
#RUN dotnet restore UserMicroservice1/UserMicroservice.csproj
#COPY . .
#WORKDIR /src/UserMicroservice1
#RUN dotnet build UserMicroservice.csproj -c Release -o /app
#
#FROM build AS publish
#RUN dotnet publish UserMicroservice.csproj -c Release -o /app
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "UserMicroservice.dll"]
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /app
ENV ASPNETCORE_URLS http://+:98
EXPOSE 94
COPY ${source:-obj/Docker/publish} .
ENTRYPOINT ["dotnet", "UserMicroservice.dll"]
