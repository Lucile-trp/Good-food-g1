FROM node:slim
WORKDIR /app

# Restore packages
COPY . .
RUN npm install

# Build & run application for production
CMD npm run start


