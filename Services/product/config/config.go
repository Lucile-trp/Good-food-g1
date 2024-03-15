package config

import (
	"fmt"
	"os"

	"github.com/joho/godotenv"
)

type (
	Config struct {
		ConnectionString string
		RabbitMQHostname string
		RabbitMQUsername string
		RabbitMQPassword string
	}
)

// NewConfig returns app config.
func NewConfig() (*Config, error) {
	cfg := &Config{}
	var err error

	godotenv.Load()

	cfg.ConnectionString, err = GetRequiredVariableEnv("CONNECTION_STRING")
	if err != nil {
		return nil, err
	}

	cfg.RabbitMQHostname, err = GetRequiredVariableEnv("RABBITMQ_HOSTNAME")
	if err != nil {
		return nil, err
	}

	cfg.RabbitMQUsername, err = GetRequiredVariableEnv("RABBITMQ_USERNAME")
	if err != nil {
		return nil, err
	}

	cfg.RabbitMQPassword, err = GetRequiredVariableEnv("RABBITMQ_PASSWORD")
	if err != nil {
		return nil, err
	}

	return cfg, err
}

func GetRequiredVariableEnv(key string) (string, error) {
	value := os.Getenv(key)

	if value == "" {
		return "", fmt.Errorf("%s is missing in environnement variable", key)
	}

	return value, nil
}
