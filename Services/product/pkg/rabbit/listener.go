package rabbit

import (
	"encoding/json"
	"fmt"
	"product/internal/repo"
	"product/pkg/logger"
	"strconv"
	"strings"
)

func (r Rabbit) Listen(l logger.Interface, p *repo.ProductRepo) error {
	msg, err := r.consume.channel.Consume(
		r.consume.queue,
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
			err = r.publish.Publish(l, p, id, orderId)
			if err != nil {
				l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
			}
		}
	}

	return nil
}
