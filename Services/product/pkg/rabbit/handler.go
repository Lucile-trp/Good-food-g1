package rabbit

import (
	"context"
	"encoding/json"
	"fmt"
	"product/internal/repo"
	"product/pkg/logger"
	"strconv"

	"github.com/streadway/amqp"
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

		var currencies = map[string]string{}
		err = json.Unmarshal(m.Body, &currencies)
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: unmarshal to map: %w", err))
		}

		id, err := strconv.Atoi(currencies["dishId"])
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
		}

		if err == nil && m.Body != nil {
			err = r.PublishDish(l, p, id)
			if err != nil {
				l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
			}
		}
	}

	return nil
}

func (r Rabbit) PublishDish(l logger.Interface, p *repo.ProductRepo, id int) error {
	dish, err := p.GetDish(context.Background(), id)
	if err != nil {
		return fmt.Errorf("postgres: get dish: %w", err)
	}

	data, err := json.Marshal(dish)
	if err != nil {
		return fmt.Errorf("map marshalling: %w", err)
	}

	err = r.channel.Publish(
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
