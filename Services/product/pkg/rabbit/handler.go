package rabbit

import (
	"context"
	"encoding/json"
	"fmt"
	"product/internal/entity"
	"product/internal/repo"
	"product/pkg/logger"
	"strconv"
	"strings"
	"time"

	amqp "github.com/rabbitmq/amqp091-go"
)

func (r Rabbit) Close() error {
	return r.channel.Close()
}

func (r Rabbit) Listen(l logger.Interface, p *repo.ProductRepo) error {

	msg, err := r.channel.Consume(
		r.queueConsume,
		"",
		true,
		false,
		false,
		false,
		nil,
	)

	if err != nil {
		return fmt.Errorf("consuming channel: %w", err)
	}

	l.Info("Listening Started")

	for m := range msg {
		str := strings.Replace(string(m.Body), "\\", "", -1)

		l.Info(str)

		var currencies = map[string]string{}
		err = json.Unmarshal([]byte(str), &currencies)
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: unmarshal to map: %w", err))
		}

		id, err := strconv.Atoi(currencies["dishId"])
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
		}

		orderId, err := strconv.Atoi(currencies["orderId"])
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
		}

		if err == nil && m.Body != nil {
			err = r.PublishDish(l, p, id, orderId)
			if err != nil {
				l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
			}
		}
	}

	return nil
}

func (r Rabbit) PublishDish(l logger.Interface, p *repo.ProductRepo, id int, orderId int) error {
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

	l.Info(string(data))

	ctx, cancel := context.WithTimeout(context.Background(), 5*time.Second)
	defer cancel()

	err = r.channel.PublishWithContext(ctx,
		"goodfood.exchange",
		r.queuePublish,
		false,
		false,
		amqp.Publishing{
			ContentType: "application/json",
			Body:        data,
		},
	)

	if err != nil {
		return fmt.Errorf("publishing: %w", err)
	}

	return nil
}
