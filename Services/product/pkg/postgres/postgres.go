// Package postgres implements postgres connection.
package postgres

import (
	"context"
	"fmt"
	"log"
	"time"

	"github.com/jackc/pgx/v5"
)

const (
	_defaultMaxPoolSize  = 1
	_defaultConnAttempts = 10
	_defaultConnTimeout  = time.Second
)

// Postgres -.
type Postgres struct {
	maxPoolSize  int
	connAttempts int
	connTimeout  time.Duration

	Pool *pgx.Conn
}

// New -.
func New(url string) (*Postgres, error) {
	pg := &Postgres{
		maxPoolSize:  _defaultMaxPoolSize,
		connAttempts: _defaultConnAttempts,
		connTimeout:  _defaultConnTimeout,
	}

	for pg.connAttempts > 0 {

		poolConfig, err := pgx.ParseConfig(url)
		if err != nil {
			return nil, fmt.Errorf("postgres - NewPostgres - pgxpool.ParseConfig: %w", err)
		}

		pg.Pool, err = pgx.ConnectConfig(context.Background(), poolConfig)

		if err == nil {
			break
		}

		log.Printf("Postgres is trying to connect, attempts left: %d", pg.connAttempts)

		time.Sleep(pg.connTimeout)

		pg.connAttempts--
	}

	return pg, nil
}

// Close -.
func (p *Postgres) Close() {
	if p.Pool != nil {
		p.Pool.Close(context.Background())
	}
}
