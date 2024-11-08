package entity

type Ordering struct {
	Dish    Dish `json:"dish"`
	OrderId int  `json:"orderId"`
}
