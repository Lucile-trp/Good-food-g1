package handler

import (
	"context"
	"fmt"
	"product/internal/repo"
	"product/pkg/logger"
	"product/pkg/rmqrpc/server"

	"github.com/streadway/amqp"
)

type productRoutes struct {
	l logger.Interface
	p repo.ProductRepo
}

func NewRouter(l logger.Interface, p repo.ProductRepo) map[string]server.CallHandler {
	r := productRoutes{l, p}

	routes := make(map[string]server.CallHandler)
	{
		routes["getProducts"] = r.GetProducts()
	}

	return routes
}

func (r productRoutes) GetProducts() server.CallHandler {
	return func(d *amqp.Delivery) (interface{}, error) {
		products, err := r.p.GetProducts(context.Background())
		if err != nil {
			r.l.Error(err, "GetProducts on product")
			return nil, fmt.Errorf("productRoutes - GetProducts - r.p.GetProducts: %w", err)
		}

		return products, nil
	}
}
