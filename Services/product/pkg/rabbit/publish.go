package rabbit

import (
	"context"
	"encoding/json"
	"fmt"
	"product/internal/entity"
	"product/internal/repo"
	"product/pkg/logger"
	"product/pkg/rabbit/rpcEntity"
	"time"

	amqp "github.com/rabbitmq/amqp091-go"
)

func (r Rabbit) PublishDish(l logger.Interface, p *repo.ProductRepo, order rpcEntity.OrderConsume) error {

	var dishes []entity.Dish

	for _, dishId := range order.DishesId {
		dish, err := p.GetDish(context.Background(), dishId)

		if err != nil {
			return fmt.Errorf("postgres: get dish: %w", err)
		}

		dishes = append(dishes, dish)
	}

	ordering := rpcEntity.OrderPublish{
		Dish:              dishes,
		CustomerId:        order.CustomerId,
		DeliveryId:        order.DeliveryId,
		DeliveryAdresseId: order.DeliveryAdresseId,
	}

	data, err := json.Marshal(ordering)

	if err != nil {
		return fmt.Errorf("map marshalling: %w", err)
	}

	body := string(data)

	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	err = r.ch.PublishWithContext(ctx,
		"",
		r.publish,
		false,
		false,
		amqp.Publishing{
			ContentType: "application/json",
			Body:        []byte(body),
		},
	)

	if err != nil {
		return fmt.Errorf("publishing: %w", err)
	}

	l.Info("%s sent: %s", r.publish, body)

	return nil
}
