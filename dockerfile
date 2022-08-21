# Build runtime image
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
WORKDIR /src/MyAdventureAPI
RUN dotnet publish MyAdventureAPI.csproj -c Release -o /app


FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as api
WORKDIR /app
COPY --from=build /app .
EXPOSE 8080
ENTRYPOINT ["dotnet", "MyAdventureAPI.dll"]

# ### STAGE 1: Build ###
# FROM node:16.10-alpine AS FrontEndbuild
# WORKDIR /usr/src/app
# COPY ./frontend/package.json ./frontend/package-lock.json ./
# RUN npm install
# COPY ./frontend/ .
# RUN npm run build
# ### STAGE 2: Run ###
# FROM nginx:1.17.1-alpine AS app
# COPY ./frontend/nginx.conf /etc/nginx/nginx.conf
# COPY --from=FrontEndbuild /usr/src/app/dist/adventure /usr/share/nginx/html
