package main

import (
	"log"
	"produit/config"
	"produit/internal/app"
)

func main() {
	// Configuration
	cfg, err := config.NewConfig()
	if err != nil {
		log.Fatalf("Environnement variable error: %s", err)
	}

	app.Run(cfg)
}
