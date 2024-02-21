FROM mcr.microsoft.com/dotnet/sdk:8.0 as build
WORKDIR /app

COPY ./Application/*.csproj ./Application/
COPY ./Domain/*.csproj ./Domain/
COPY ./Persistence/*.csproj ./Persistence/
COPY ./WebAPI/*.csproj ./WebAPI/
COPY ./libs ./libs/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./WebAPI/*.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_URLS="http://*:5000"
ENTRYPOINT [ "dotnet" , "WebAPI.dll" ]