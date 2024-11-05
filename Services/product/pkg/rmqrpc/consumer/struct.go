package rabbit

import (
	"context"
	"encoding/json"
	"fmt"
	"log"
	"product/internal/repo"

	"github.com/streadway/amqp"
)

type Rabbit struct {
	channel *amqp.Channel
	query   string
}

func (r Rabbit) Close() error {
	return r.channel.Close()
}

func (r Rabbit) Listen(p *repo.ProductRepo) error {

	msg, err := r.channel.Consume(
		r.query,
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

	log.Println("Listening Started")

	for m := range msg {
		log.Println("Update Started")

		var currencies = map[string]string{}
		err = json.Unmarshal(m.Body, &currencies)
		if err != nil {
			log.Println(fmt.Errorf("rabbitmq: unmarshal to map: %w", err))
			continue
		}

		dish, err := p.GetDish(context.Background(), 1)
		if err != nil {
			log.Println(fmt.Errorf("postgres: insert: %w", err))
			continue
		}

		err = r.channel.Publish(
			"currs.fanout",
			"queue",
			false,
			false,
			amqp.Publishing{
				ContentType: "application/json",
				Body:        dish,
			},
		)

		if err != nil {
			return fmt.Errorf("publishing: %w", err)
		}

		return nil
	}

	return nil
}
