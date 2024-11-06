package rabbit

import (
	"fmt"

	"github.com/streadway/amqp"
)

type Rabbit struct {
	channel *amqp.Channel
	queueConsume   string
	queuePublish   string
}

func Start(amqpUrl string) (*Rabbit, error) {
	conn, err := amqp.Dial(amqpUrl)

	if err != nil {
		return nil, fmt.Errorf("dialing rabbitmq: %w", err)
	}

	ch, err := conn.Channel()
	if err != nil {
		return nil, fmt.Errorf("opening channel: %w", err)

	}

	err = ch.ExchangeDeclare(
		"goodfood.exchange",
		"topic",
		false,
		false,
		false,
		false,
		nil,
	)
	if err != nil {
		return nil, fmt.Errorf("declaring exchange: %w", err)
	}

	queueConsume, err := DeclareQueue(ch, "goodfood.queue.sendDishId")
	if err != nil {
		return nil, fmt.Errorf("declaring consumer queue: %w", err)
	}

	queuePublish, err := DeclareQueue(ch, "goodfood.queue.getdish")
	if err != nil {
		return nil, fmt.Errorf("declaring consumer queue: %w", err)
	}

	return &Rabbit{
		channel: ch,
		queueConsume: queueConsume,
		queuePublish: queuePublish,
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

	err = ch.QueueBind(
		q.Name,              //queue name
		"goodfood.queue.*",  //routing key
		"goodfood.exchange", //exchange
		false,
		nil,
	)
	if err != nil {
		return "", fmt.Errorf("binding queue: %w", err)
	}

	return q.Name, nil
}
