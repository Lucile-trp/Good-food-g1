package app

import (
	"fmt"
	"produit/config"
	"produit/internal/controller"
	"produit/internal/repo"
	"produit/pkg/httpserver"
	"produit/pkg/logger"
	"produit/pkg/postgres"

	"github.com/gin-gonic/gin"
)

func Run(cfg *config.Config) {
	l := logger.New("debug")

	// Repository
	pg, err := postgres.New(cfg.ConnectionString, postgres.MaxPoolSize(2))
	if err != nil {
		l.Fatal(fmt.Errorf("app - Run - postgres.New: %w", err))
	}
	defer pg.Close()

	// HTTP Server
	handler := gin.New()
	controller.NewRouter(handler, l, *repo.New(pg))
	httpServer := httpserver.New(handler, httpserver.Port("8080"))

	// Shutdown
	err = httpServer.Shutdown()
	if err != nil {
		l.Error(fmt.Errorf("app - Run - httpServer.Shutdown: %w", err))
	}
}
