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
		routes["getDishes"] = r.GetDishes()
		routes["getRestaurants"] = r.GetRestaurants()
		routes["getImages"] = r.GetImages()
	}

	return routes
}

func (r productRoutes) GetDishes() server.CallHandler {
	return func(d *amqp.Delivery) (interface{}, error) {
		dishes, err := r.p.GetDishes(context.Background())
		if err != nil {
			r.l.Error(err, "GetDishes on dish")
			return nil, fmt.Errorf("productRoutes - GetDishes - r.p.GetDishes: %w", err)
		}

		return dishes, nil
	}
}

func (r productRoutes) GetRestaurants() server.CallHandler {
	return func(d *amqp.Delivery) (interface{}, error) {
		restaurants, err := r.p.GetRestaurants(context.Background())
		if err != nil {
			r.l.Error(err, "GetRestaurants on restaurants")
			return nil, fmt.Errorf("productRoutes - GetRestaurants - r.p.GetRestaurants: %w", err)
		}

		return restaurants, nil
	}
}

func (r productRoutes) GetImages() server.CallHandler {
	return func(d *amqp.Delivery) (interface{}, error) {
		images, err := r.p.GetImages(context.Background())
		if err != nil {
			r.l.Error(err, "GetImages on images")
			return nil, fmt.Errorf("productRoutes - GetImages - r.p.GetImages: %w", err)
		}

		return images, nil
	}
}
