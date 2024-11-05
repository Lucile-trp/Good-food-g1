package rabbit

import (
	"context"
	"encoding/json"
	"fmt"
	"log"
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
		r.queue,
		"",
		false,
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
		l.Info("Update Started")

		var currencies = map[string]string{}
		err = json.Unmarshal(m.Body, &currencies)
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: unmarshal to map: %w", err))
			continue
		}

		id, err := strconv.Atoi(currencies["dishId"])
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
			continue
		}

		dish, err := p.GetDish(context.Background(), id)
		if err != nil {
			l.Error(fmt.Errorf("postgres: get dish: %w", err))
			continue
		}

		data, err := json.Marshal(dish)
		if err != nil {
			log.Println("map marshalling: ", err)
		}

		err = r.channel.Publish(
			"goodfood.exchange",
			r.queue,
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

	return nil
}
