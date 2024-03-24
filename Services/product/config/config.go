package config

import (
	"fmt"
	"os"
)

type Config struct {
	ConnexionString string
	RmqHostname     string
	RmqUsername     string
	RmqPassword     string
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

	cfg.RmqHostname, err = GetRequiredVariableEnv("RABBITMQ_HOSTNAME")
	if err != nil {
		return cfg, err
	}

	cfg.RmqUsername, err = GetRequiredVariableEnv("RABBITMQ_USERNAME")
	if err != nil {
		return cfg, err
	}

	cfg.RmqPassword, err = GetRequiredVariableEnv("RABBITMQ_PASSWORD")
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
