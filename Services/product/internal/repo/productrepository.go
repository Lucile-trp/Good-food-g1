package repo

import (
	"context"
	"produit/internal/entity"
	"produit/pkg/postgres"
)

const _defaultEntityCap = 64

// TranslationRepo -.
type TranslationRepo struct {
	*postgres.Postgres
}

// New -.
func New(pg *postgres.Postgres) *TranslationRepo {
	return &TranslationRepo{pg}
}

func (r *TranslationRepo) GetProducts(ctx context.Context) ([]entity.Product, error) {
	// sql, _, err := r.Builder.
	// 	Select("source, destination, original, translation").
	// 	From("history").
	// 	ToSql()
	// if err != nil {
	// 	return nil, fmt.Errorf("TranslationRepo - GetHistory - r.Builder: %w", err)
	// }

	// rows, err := r.Pool.Query(ctx, sql)
	// if err != nil {
	// 	return nil, fmt.Errorf("TranslationRepo - GetHistory - r.Pool.Query: %w", err)
	// }
	// defer rows.Close()

	entities := make([]entity.Product, 0, _defaultEntityCap)

	// for rows.Next() {
	// 	e := entity.Product{}

	// 	err = rows.Scan(&e.Source, &e.Destination, &e.Original, &e.Translation)
	// 	if err != nil {
	// 		return nil, fmt.Errorf("TranslationRepo - GetHistory - rows.Scan: %w", err)
	// 	}

	// 	entities = append(entities, e)
	// }

	entities = append(entities, entity.Product{Name: "test"})

	return entities, nil
}
