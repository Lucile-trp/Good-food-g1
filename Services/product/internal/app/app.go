package app

import (
	"fmt"
	"product/config"
	"product/internal/controller"
	"product/internal/repo"
	"product/pkg/httpserver"
	"product/pkg/logger"
	"product/pkg/postgres"
	"product/pkg/rabbit"

	"github.com/gin-gonic/gin"
)

func Run(cfg config.Config) {
	l := logger.New(cfg.LogMode)

	// Repository
	pg, err := postgres.New(cfg.ConnectionString)
	if err != nil {
		l.Fatal(fmt.Errorf("app - Run - postgres.New: %w", err))
	}
	defer pg.Close()

	// RabbitMQ RPC Server
	r, err := rabbit.Start(cfg.RmqURL)
	if err != nil {
		l.Fatal("app - Run - connecting to rabbitmq: %w", err)
	}

	// HTTP Server
	p := repo.New(pg)
	gin.SetMode(cfg.GinMode)
	handler := gin.New()
	controller.NewRouter(handler, l, *p)
	httpServer := httpserver.New(handler, httpserver.Port("8080"))

	// Waiting signal
	l.Error(fmt.Errorf("app - Run - rabbit.Listen: %w", r.Listen(l, p)))

	err = <-httpServer.Notify()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - httpServer.Notify: %w", err))
	}

	// Shutdown
	err = httpServer.Shutdown()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - httpServer.Shutdown: %w", err))
	}

	err = r.Close()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - rmqServer.Shutdown: %w", err))
	}
}
