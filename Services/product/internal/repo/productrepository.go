package repo

import (
	"context"
	"product/internal/entity"
	"product/pkg/postgres"
)

//const _defaultEntityCap = 64

// TranslationRepo -.
type ProductRepo struct {
	*postgres.Postgres
}

// New -.
func New(pg *postgres.Postgres) *ProductRepo {
	return &ProductRepo{pg}
}

func (r *ProductRepo) GetProducts(ctx context.Context) ([]entity.Product, error) {
	// sql, _, err := r.Builder.
	// 	Select("source, destination, original, translation").
	// 	From("history").
	// 	ToSql()
	// if err != nil {
	// 	return nil, fmt.Errorf("ProductRepo - GetHistory - r.Builder: %w", err)
	// }

	// rows, err := r.Pool.Query(ctx, sql)
	// if err != nil {
	// 	return nil, fmt.Errorf("ProductRepo - GetHistory - r.Pool.Query: %w", err)
	// }
	// defer rows.Close()

	// entities := make([]entity.Product, 0, _defaultEntityCap)

	// for rows.Next() {
	// 	e := entity.Product{}

	// 	err = rows.Scan(&e.Name)
	// 	if err != nil {
	// 		return nil, fmt.Errorf("ProductRepo - GetHistory - rows.Scan: %w", err)
	// 	}

	// 	entities = append(entities, e)
	// }

	entities := []entity.Product{
		{Name: "Fishs and Chips"},
		{Name: "Pates & cordon-bleu"}}

	return entities, nil
}
