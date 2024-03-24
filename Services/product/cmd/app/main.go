package main

import (
	"log"
	"product/config"
	"product/internal/app"
)

func main() {
	// Configuration
	cfg, err := config.NewConfig()
	if err != nil {
		log.Fatalf("Environnement variable error: %s", err)
	}

	app.Run(cfg)
}
