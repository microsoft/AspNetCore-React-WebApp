# to build local image from PowerShell (e.g. when you're authoring this dockerfile):
# docker build -f ./dockerfile . --build-arg APP_ENV=dev

# build backend
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS serviceBuild

# copy backend files into 'service' folder in prep for dotnet commands
COPY service /service

# ensure tests are passing
ARG ASPNETCORE_ENVIRONMENT
WORKDIR /service/Microsoft.DSX.ProjectTemplate.API
# build the service separately to generate the typescript client with better error output
RUN dotnet build 
# skip build on test since it was just done previously
RUN dotnet test --no-build

# setup frontend
FROM node:12 AS clientBuild
ARG APP_ENV
RUN echo APP_ENV = ${APP_ENV}
RUN npm config set unsafe-perm true
COPY client /client

# copy auto-generated TS files from API bulid
COPY --from=serviceBuild /client/src/app/generated/. client/src/app/generated/

# build frontend
WORKDIR /client
RUN npm ci
ENV REACT_APP_ENV=${APP_ENV}
RUN npm run build

# copy our frontend into published app's wwwroot folder
FROM serviceBuild AS publisher
COPY --from=clientBuild /client/build /app/wwwroot/

# build & publish our API
ARG ASPNETCORE_ENVIRONMENT
RUN dotnet publish /service/Microsoft.DSX.ProjectTemplate.API/Microsoft.DSX.ProjectTemplate.API.csproj -c Release -o /app

# build runtime image (contains full stack)
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
EXPOSE 80
COPY --from=publisher /app ./
ENTRYPOINT ["dotnet", "Microsoft.DSX.ProjectTemplate.API.dll"]
