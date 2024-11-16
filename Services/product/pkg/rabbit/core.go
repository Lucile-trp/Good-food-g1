package rabbit

import (
	"fmt"

	amqp "github.com/rabbitmq/amqp091-go"
)

type RabbitChannel struct {
	channel *amqp.Channel
	queue   string
}

type Rabbit struct {
	publish RabbitChannel
	consume RabbitChannel
}

func (r Rabbit) Close() error {
	err := r.consume.channel.Close()

	if err != nil {
		return fmt.Errorf("close consume channel failed: %w", err)
	}

	err = r.publish.channel.Close()

	if err != nil {
		return fmt.Errorf("close publish channel failed: %w", err)
	}

	return nil
}

func Start(amqpUrl string) (*Rabbit, error) {
	queueBase := "goodfood.queue."
	exchange := "goodfood.exchange"

	consume, err := CreateChannel(amqpUrl, exchange, queueBase, "sendDishId")

	if err != nil {
		return nil, fmt.Errorf("create consume channel failed: %w", err)
	}

	publish, err := CreateChannel(amqpUrl, exchange, queueBase, "getDish")

	if err != nil {
		return nil, fmt.Errorf("create publish channel failed: %w", err)
	}

	return &Rabbit{
		consume: *consume,
		publish: *publish,
	}, nil
}

func CreateChannel(amqpUrl string, exchange string, queueBase string, queueName string) (*RabbitChannel, error) {
	conn, err := amqp.Dial(amqpUrl)

	if err != nil {
		return nil, fmt.Errorf("dialing rabbitmq: %w", err)
	}

	ch, err := conn.Channel()

	if err != nil {
		return nil, fmt.Errorf("opening channel: %w", err)
	}

	queue, err := DeclareQueue(ch, queueBase+queueName)

	if err != nil {
		return nil, fmt.Errorf("declaring consumer queue: %w", err)
	}

	return &RabbitChannel{
		channel: ch,
		queue:   queue,
	}, nil
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
