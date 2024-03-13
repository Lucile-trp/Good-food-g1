FROM node:21-alpine
WORKDIR /app

# Restore packages
COPY . .
RUN npm install

# Build & run application for production
ENTRYPOINT [ "node", "./server.js" ]


