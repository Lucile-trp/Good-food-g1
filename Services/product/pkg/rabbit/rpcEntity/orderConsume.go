package rpcEntity

type OrderConsume struct {
	DishesId          []int `json:"dishesId"`
	CustomerId        int   `json:"customerId"`
	DeliveryId        int   `json:"deliveryId"`
	DeliveryAdresseId int   `json:"deliveryAdresseId"`
}
