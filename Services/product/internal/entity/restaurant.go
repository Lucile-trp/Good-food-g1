package entity

type Restaurant struct {
	Id          int    `json:"id"`
	Name        string `json:"name"`
	Description string `json:"description"`
	Address     string `json:"address"`
	CP          string `json:"cp"`
	City        string `json:"city"`
	Country     string `json:"country"`
}
