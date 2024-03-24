package config

import (
	"fmt"
	"os"
)

type Config struct {
	ConnexionString string
	RmqURL          string
	GinMode         string
	LogMode         string
}

// NewConfig returns app config.
func NewConfig() (Config, error) {
	var cfg Config
	var err error

	cfg.ConnexionString, err = GetRequiredVariableEnv("CONNEXION_STRING")
	if err != nil {
		return cfg, err
	}

	cfg.RmqURL, err = GetRequiredVariableEnv("RABBITMQ_URL")
	if err != nil {
		return cfg, err
	}

	cfg.GinMode, err = GetRequiredVariableEnv("GIN_MODE")
	if err != nil {
		return cfg, err
	}

	cfg.LogMode, err = GetRequiredVariableEnv("LOG_MODE")
	if err != nil {
		return cfg, err
	}

	return cfg, err
}

func GetRequiredVariableEnv(key string) (string, error) {
	value, found := os.LookupEnv(key)
	if !found {
		return "", fmt.Errorf("%s is missing in environment variable", key)
	}

	return value, nil
}
