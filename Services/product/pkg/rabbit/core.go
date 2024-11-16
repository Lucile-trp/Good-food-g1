package rabbit

import (
	"fmt"

	amqp "github.com/rabbitmq/amqp091-go"
)

type Rabbit struct {
	ch      *amqp.Channel
	publish string
	consume string
}

func (r Rabbit) Close() error {
	err := r.ch.Close()

	if err != nil {
		return fmt.Errorf("close consume channel failed: %w", err)
	}

	err = r.ch.Close()

	if err != nil {
		return fmt.Errorf("close publish channel failed: %w", err)
	}

	return nil
}

func Start(amqpUrl string) (*Rabbit, error) {
	sendDishId := "sendDishId"
	getDish := "getDish"
	queueBase := "goodfood.queue."
	exchange := "goodfood.exchange"

	ch, err := CreateChannel(amqpUrl, exchange, queueBase, "sendDishId")

	if err != nil {
		return nil, fmt.Errorf("create consume channel failed: %w", err)
	}
	return &Rabbit{
		ch:      ch,
		consume: queueBase + sendDishId,
		publish: queueBase + getDish,
	}, nil
}

func CreateChannel(amqpUrl string, exchange string, queueBase string, queueName string) (*amqp.Channel, error) {
	conn, err := amqp.Dial(amqpUrl)

	if err != nil {
		return nil, fmt.Errorf("dialing rabbitmq: %w", err)
	}

	ch, err := conn.Channel()

	if err != nil {
		return nil, fmt.Errorf("opening channel: %w", err)
	}

	_, err = DeclareQueue(ch, queueBase+queueName)

	if err != nil {
		return nil, fmt.Errorf("declaring consumer queue: %w", err)
	}

	return ch, nil
}

func DeclareQueue(ch *amqp.Channel, queue string) (string, error) {
	q, err := ch.QueueDeclare(
		queue, //name
		false, //durable
		false, //delete when usused
		false, //exclusive
		false, //no-wait
		nil,   //arguments
	)

	if err != nil {
		return "", fmt.Errorf("declaring queue: %w", err)
	}

	return q.Name, err
}
