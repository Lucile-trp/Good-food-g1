FROM golang:1.21.5-alpine3.18 AS build-env
WORKDIR /app

COPY . ./
# Restore packages
RUN go mod download
# build source
RUN go build -o product -work ./cmd/app/main.go

FROM scratch
COPY --from=build-env ./app/product ./product
CMD [ "./product" ]
