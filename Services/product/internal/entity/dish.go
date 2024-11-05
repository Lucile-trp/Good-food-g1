package entity

type Dish struct {
	Id           int32   `json:"id"`
	Title        *string `json:"title"`
	Description  *string `json:"description"`
	Cost         float64 `json:"cost"`
	RestaurantId int32   `json:"restaurantId"`
}
