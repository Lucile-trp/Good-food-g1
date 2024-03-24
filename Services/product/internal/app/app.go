package app

import (
	"fmt"
	"os"
	"os/signal"
	"product/config"
	"product/internal/controller"
	"product/internal/handler"
	"product/internal/repo"
	"product/pkg/httpserver"
	"product/pkg/logger"
	"product/pkg/postgres"
	"product/pkg/rmqrpc/server"
	"syscall"

	"github.com/gin-gonic/gin"
)

func Run(cfg config.Config) {
	l := logger.New(cfg.LogMode)

	// Repository
	pg, err := postgres.New(cfg.ConnexionString, postgres.MaxPoolSize(2))
	if err != nil {
		l.Fatal(fmt.Errorf("app - Run - postgres.New: %w", err))
	}
	defer pg.Close()

	// RabbitMQ RPC Server
	rmqRouter := handler.NewRouter(l, *repo.New(pg))

	rmqServer, err := server.New(cfg.RmqURL, "goodfood.exchange", "goodfood.queue.productMsgQ", "goodfood.queue.*", rmqRouter, l)
	if err != nil {
		l.Fatal(fmt.Errorf("app - Run - rmqServer - server.New: %w", err))
	}

	// HTTP Server
	gin.SetMode(cfg.GinMode)
	handler := gin.New()
	controller.NewRouter(handler, l, *repo.New(pg))
	httpServer := httpserver.New(handler, httpserver.Port("8080"))

	// Waiting signal
	interrupt := make(chan os.Signal, 1)
	signal.Notify(interrupt, os.Interrupt, syscall.SIGTERM)

	select {
	case s := <-interrupt:
		l.Info("app - Run - signal: " + s.String())
	case err = <-httpServer.Notify():
		l.Error(fmt.Errorf("app - Run - httpServer.Notify: %w", err))
	case err = <-rmqServer.Notify():
		l.Error(fmt.Errorf("app - Run - rmqServer.Notify: %w", err))
	}

	// Shutdown
	err = httpServer.Shutdown()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - httpServer.Shutdown: %w", err))
	}

	err = rmqServer.Shutdown()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - rmqServer.Shutdown: %w", err))
	}
}
