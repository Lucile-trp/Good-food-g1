FROM golang:1.21.5 AS build-env
WORKDIR /app

COPY . ./
# Restore packages
RUN go mod download
# build source
Run go build -o product -work

ENTRYPOINT ["/app/product"]
