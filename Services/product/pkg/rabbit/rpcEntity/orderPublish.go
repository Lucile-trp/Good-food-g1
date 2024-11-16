package rpcEntity

import (
	"product/internal/entity"
)

type OrderPublish struct {
	Dish              []entity.Dish `json:"dish"`
	CustomerId        int           `json:"customerId"`
	DeliveryId        int           `json:"deliveryId"`
	DeliveryAdresseId int           `json:"deliveryAdresseId"`
}
