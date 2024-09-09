package entity

type Dish struct {
	Id          int        `json:"id"`
	Title       string     `json:"title"`
	Description string     `json:"description"`
	Cost        float64    `json:"cost"`
	Images      []Image    `json:"images"`
	Restaurant  Restaurant `json:"restaurant"`
}
