package rpcEntity

import (
	"product/internal/entity"
)

type OrderPublish struct {
	Dishes            []entity.Dish `json:"dishes"`
	CustomerId        int           `json:"customerId"`
	DeliveryId        int           `json:"deliveryId"`
	DeliveryAdresseId int           `json:"deliveryAdresseId"`
}
