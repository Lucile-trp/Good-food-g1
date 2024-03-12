FROM node:21.5.0-alpine3.18 AS build-env
WORKDIR /app

# Restore packages
COPY package*.json ./
RUN npm install


# Build source sources code
COPY . .
RUN npm run build && npm prune --production

FROM node:21.5.0-alpine3.18
WORKDIR /out

COPY --from=build-env ./app/package*.json ./
COPY --from=build-env ./app/dist/ ./build/
COPY --from=build-env ./app/node_modules/ ./node_modules/

# Build & run application for production
ENTRYPOINT [ "node", "./build/main.js" ]


