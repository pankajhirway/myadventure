#!/bin/bash

#docker run -d -p 27017:27017 --name test-mongo -v mongo-data:/data/db -e MONGODB_INITDB_ROOT_USERNAME=sample-db-user -e MONGODB_INITDB_ROOT_PASSWORD=sample-password mongo:latest
docker-compose up --build --force-recreate