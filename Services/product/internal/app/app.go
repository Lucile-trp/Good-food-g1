package app

import (
	"fmt"
	"os"
	"os/signal"
	"produit/config"
	"produit/internal/controller"
	"produit/internal/repo"
	"produit/pkg/httpserver"
	"produit/pkg/logger"
	"produit/pkg/postgres"
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
	}

	// Shutdown
	err = httpServer.Shutdown()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - httpServer.Shutdown: %w", err))
	}
}
