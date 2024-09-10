package entity

type Image struct {
	Id          int32   `json:"id"`
	Url         *string `json:"url"`
	Description *string `json:"description"`
	DishId      int32   `json:"dishId"`
}
