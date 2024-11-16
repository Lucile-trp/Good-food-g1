package rabbit

import (
	"context"
	"encoding/json"
	"fmt"
	"product/internal/entity"
	"product/internal/repo"
	"product/pkg/logger"
	"time"

	amqp "github.com/rabbitmq/amqp091-go"
)

func (ch RabbitChannel) Publish(l logger.Interface, p *repo.ProductRepo, id int, orderId int) error {
	dish, err := p.GetDish(context.Background(), id)

	if err != nil {
		return fmt.Errorf("postgres: get dish: %w", err)
	}

	ordering := entity.Ordering{
		Dish:    dish,
		OrderId: orderId,
	}

	data, err := json.Marshal(ordering)

	if err != nil {
		return fmt.Errorf("map marshalling: %w", err)
	}

	body := string(data)

	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	err = ch.channel.PublishWithContext(ctx,
		"",
		ch.queue,
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

	l.Info("%s sent: %s", ch.queue, body)

	return nil
}
