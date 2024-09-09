package repo

import (
	"context"
	"fmt"
	"product/internal/entity"
	"product/pkg/postgres"
)

const _defaultEntityCap = 64

type ProductRepo struct {
	*postgres.Postgres
}

// New -.
func New(pg *postgres.Postgres) *ProductRepo {
	return &ProductRepo{pg}
}

func (r *ProductRepo) GetProducts(ctx context.Context) ([]entity.Dish, error) {
	rows, err := r.Pool.Query(ctx,
		`SELECT 
			Plat.id As Id_Dish,
			Plat.titre AS Title_Dish,
			Plat.description AS Description_Dish,
			Plat.prix AS Cost_Dish,
			Restaurant.id AS Id_Restaurant,
			Restaurant.nom AS Name_Restaurant,
			Restaurant.description AS Description_Restaurant,
			Restaurant.adresse AS Address_Restaurant,
			Restaurant.code_postal AS CP_Restaurant,
			Restaurant.ville AS City_Restaurant,
			Restaurant.pays AS Country_Restaurant,
			Image.id AS Id_Image,
			Image.url AS Url_Image,
			Image.description AS Description_Image
		FROM 
			Plat
		JOIN 
			Restaurant ON Plat.restaurant_id = Restaurant.id
		LEFT JOIN 
			Image ON Image.plat_id = Plat.id;`)

	if err != nil {
		return nil, fmt.Errorf("ProductRepo - GetHistory - r.Pool.Query: %w", err)
	}
	defer rows.Close()

	entities := make([]entity.Dish, 0, _defaultEntityCap)

	for rows.Next() {
		values, err := rows.Values()
		if err != nil {
			return nil, fmt.Errorf("ProductRepo - GetHistory - rows.Scan: %w", err)
		}

		newEntity := MapProduct(values)

		duplicate := false

		for i := 0; i < len(entities); i++ {
			if entities[i].Id == newEntity.Id {
				duplicate = true

				imgExist := false

				for j := 0; j < len(entities[i].Images); j++ {
					if entities[i].Images[j].Id == newEntity.Images[0].Id {
						imgExist = true
					}
				}

				if !imgExist {
					entities[i].Images = append(entities[i].Images, newEntity.Images[0])

					continue
				}

				continue
			}

		}

		if !duplicate {
			entities = append(entities, newEntity)
		}
	}

	return entities, nil
}

func MapProduct(values []interface{}) entity.Dish {
	dishId := values[0].(int)
	dishTitle := values[1].(string)
	dishDesc := values[2].(string)
	dishCost := values[3].(float64)
	resId := values[4].(int)
	resName := values[5].(string)
	resDesc := values[6].(string)
	resAddr := values[7].(string)
	resCP := values[8].(string)
	resCity := values[9].(string)
	resCountry := values[10].(string)
	imgId := values[11].(int)
	imgUrl := values[12].(string)
	imgDesc := values[13].(string)

	return entity.Dish{
		Id:          dishId,
		Title:       dishTitle,
		Description: dishDesc,
		Cost:        dishCost,
		Images: []entity.Image{
			{
				Id:          imgId,
				Url:         imgUrl,
				Description: imgDesc}},
		Restaurant: entity.Restaurant{
			Id:          resId,
			Name:        resName,
			Description: resDesc,
			Address:     resAddr,
			CP:          resCP,
			City:        resCity,
			Country:     resCountry,
		},
	}
}
