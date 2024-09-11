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
	dishes, err := GetDishes(ctx, r)
	if err != nil {
		return nil, err
	}

	images, err := GetImages(ctx, r)
	if err != nil {
		return nil, err
	}
	for i := 0; i < len(dishes); i++ {
		for j := 0; j < len(images); j++ {
			if images[j].DishId == dishes[i].Id {
				dishes[i].Images = append(dishes[i].Images, images[j])
				continue
			}
		}
	}

	return dishes, nil
}

func GetDishes(ctx context.Context, r *ProductRepo) ([]entity.Dish, error) {
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
			Restaurant.pays AS Country_Restaurant
		FROM 
			Plat
		JOIN 
			Restaurant ON Plat.restaurant_id = Restaurant.id`)

	if err != nil {
		return nil, fmt.Errorf("ProductRepo - GetDishes - r.Pool.Query: %w", err)
	}
	defer rows.Close()

	entities := make([]entity.Dish, 0, _defaultEntityCap)

	for rows.Next() {
		e := entity.Dish{}
		e.Restaurant = entity.Restaurant{}

		err := rows.Scan(&e.Id, &e.Title, &e.Description, &e.Cost, &e.Restaurant.Id, &e.Restaurant.Name, &e.Restaurant.Description,
			&e.Restaurant.Address, &e.Restaurant.CP, &e.Restaurant.City, &e.Restaurant.Country)

		if err != nil {
			return nil, fmt.Errorf("ProductRepo - GetDishes - rows.Scan: %w", err)
		}

		entities = append(entities, e)
	}

	return entities, nil
}

func GetImages(ctx context.Context, r *ProductRepo) ([]entity.Image, error) {
	rows, err := r.Pool.Query(ctx,
		`SELECT
			Image.id AS Id_Image,
			Image.url AS Url_Image,
			Image.description AS Description_Image,
			Image.plat_Id 
		FROM 
			Image;`)

	if err != nil {
		return nil, fmt.Errorf("ProductRepo - GetImages - r.Pool.Query: %w", err)
	}
	defer rows.Close()

	entities := make([]entity.Image, 0, _defaultEntityCap)

	for rows.Next() {
		e := entity.Image{}

		err := rows.Scan(&e.Id, &e.Url, &e.Description, &e.DishId)

		if err != nil {
			return nil, fmt.Errorf("ProductRepo - GetImages - rows.Scan: %w", err)
		}

		entities = append(entities, e)
	}

	return entities, nil
}

func (r *ProductRepo) InsertProduct(ctx context.Context, entity entity.Dish) error {
	_ = InsertRestaurant(ctx, r, entity.Restaurant)

	err := InsertDish(ctx, r, entity)

	if err != nil {
		return err
	}

	for i := 0; i < len(entity.Images); i++ {
		InsertImage(ctx, r, entity.Images[i])
	}

	return nil
}

func InsertDish(ctx context.Context, r *ProductRepo, dish entity.Dish) error {
	query := `INSERT INTO Plat (titre, description, prix, restaurant_id) VALUES (@titre, @description, @prix, @restaurant_id)`

	_, err := r.Pool.Exec(ctx, query,
		dish.Title,
		dish.Description,
		dish.Cost,
		dish.Restaurant.Id)

	if err != nil {
		return fmt.Errorf("unable to insert dish row: %w", err)
	}

	return nil
}

func InsertRestaurant(ctx context.Context, r *ProductRepo, restaurant entity.Restaurant) error {
	query := `INSERT INTO Restaurant (nom, description, address, code_postal, ville, pays) VALUES (@nom, @description, @address, @code_postal, @ville, @pays)`

	_, err := r.Pool.Exec(ctx, query,
		restaurant.Name,
		restaurant.Description,
		restaurant.Address,
		restaurant.CP,
		restaurant.City,
		restaurant.Country)

	if err != nil {
		return fmt.Errorf("unable to insert restaurant row: %w", err)
	}

	return nil
}

func InsertImage(ctx context.Context, r *ProductRepo, img entity.Image) error {
	query := `INSERT INTO Image (url, description, plat_id) VALUES (@url, @description, @plat_id)`

	_, err := r.Pool.Exec(ctx, query,
		img.Url,
		img.Description,
		img.DishId)

	if err != nil {
		return fmt.Errorf("unable to insert image row: %w", err)
	}

	return nil
}
