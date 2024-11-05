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

func (r *ProductRepo) GetDishes(ctx context.Context) ([]entity.Dish, error) {
	rows, err := r.Pool.Query(ctx,
		`SELECT 
			Plat.id,
			Plat.titre,
			Plat.description,
			Plat.prix,
			Plat.restaurant_id
		FROM
			Plat;`)

	if err != nil {
		return nil, fmt.Errorf("ProductRepo - GetDishes - r.Pool.Query: %w", err)
	}
	defer rows.Close()

	entities := make([]entity.Dish, 0, _defaultEntityCap)

	for rows.Next() {
		e := entity.Dish{}

		err := rows.Scan(&e.Id, &e.Title, &e.Description, &e.Cost, &e.RestaurantId)

		if err != nil {
			return nil, fmt.Errorf("ProductRepo - GetDishes - rows.Scan: %w", err)
		}

		entities = append(entities, e)
	}

	return entities, nil
}

func (r *ProductRepo) GetDish(ctx context.Context, id int) (entity.Dish, error) {
	var e entity.Dish

	err := r.Pool.QueryRow(ctx,
		`SELECT 
			Plat.id,
			Plat.titre,
			Plat.description,
			Plat.prix,
			Plat.restaurant_id
		FROM
			Plat
		WHERE
			Plat.id = $1`, id).Scan(&e.Id, &e.Title, &e.Description, &e.Cost, &e.RestaurantId)

	if err != nil {
		return e, fmt.Errorf("ProductRepo - GetDish - rows.Scan: %w", err)
	}

	return e, nil
}

func (r *ProductRepo) InsertDish(ctx context.Context, dish entity.Dish) error {
	query := `INSERT INTO Plat (titre, description, prix, restaurant_id) VALUES (@titre, @description, @prix, @restaurant_id)`

	_, err := r.Pool.Exec(ctx, query,
		dish.Title,
		dish.Description,
		dish.Cost,
		dish.RestaurantId)

	if err != nil {
		return fmt.Errorf("unable to insert dish row: %w", err)
	}

	return nil
}

func (r *ProductRepo) GetRestaurants(ctx context.Context) ([]entity.Restaurant, error) {
	rows, err := r.Pool.Query(ctx,
		`SELECT 
			Restaurant.id,
			Restaurant.nom,
			Restaurant.description,
			Restaurant.adresse,
			Restaurant.cp,
			Restaurant.ville,
			Restaurant.pays
		FROM
			Restaurant;`)

	if err != nil {
		return nil, fmt.Errorf("ProductRepo - GetRestaurant - r.Pool.Query: %w", err)
	}
	defer rows.Close()

	entities := make([]entity.Restaurant, 0, _defaultEntityCap)

	for rows.Next() {
		e := entity.Restaurant{}

		err := rows.Scan(&e.Id, &e.Name, &e.Description, &e.Address, &e.CP, &e.City, &e.Country)

		if err != nil {
			return nil, fmt.Errorf("ProductRepo - GetRestaurants - rows.Scan: %w", err)
		}

		entities = append(entities, e)
	}

	return entities, nil
}

func (r *ProductRepo) GetRestaurant(ctx context.Context, id int) (entity.Restaurant, error) {
	var e entity.Restaurant

	err := r.Pool.QueryRow(ctx,
		`SELECT 
			Restaurant.id,
			Restaurant.nom,
			Restaurant.description,
			Restaurant.adresse,
			Restaurant.cp,
			Restaurant.ville,
			Restaurant.pays
		FROM
			Restaurant
		WHERE
			Restaurant.id = $1`, id).Scan(&e.Id, &e.Name, &e.Description, &e.Address, &e.CP, &e.City, &e.Country)

	if err != nil {
		return e, fmt.Errorf("ProductRepo - GetRestaurant - rows.Scan: %w", err)
	}

	return e, nil
}

func (r *ProductRepo) InsertRestaurant(ctx context.Context, restaurant entity.Restaurant) error {
	query := `INSERT INTO Restaurant (nom, description, address, code_postal, ville, pays) 
		VALUES (@nom, @description, @address, @code_postal, @ville, @pays)`

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

func (r *ProductRepo) GetImages(ctx context.Context) ([]entity.Image, error) {
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

func (r *ProductRepo) GetImage(ctx context.Context, id int) (entity.Image, error) {
	var e entity.Image

	err := r.Pool.QueryRow(ctx,
		`SELECT
			Image.id AS Id_Image,
			Image.url AS Url_Image,
			Image.description AS Description_Image,
			Image.plat_Id 
		FROM 
			Image;
		WHERE
			Image.id = $1`, id).Scan(&e.Id, &e.Url, &e.Description, &e.DishId)

	if err != nil {
		return e, fmt.Errorf("ProductRepo - GetImage - rows.Scan: %w", err)
	}

	return e, nil
}

func (r *ProductRepo) InsertImage(ctx context.Context, img entity.Image) error {
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
