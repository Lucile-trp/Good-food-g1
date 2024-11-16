package rabbit

import (
	"encoding/json"
	"fmt"
	"product/internal/repo"
	"product/pkg/logger"
	"product/pkg/rabbit/rpcEntity"
	"strings"
)

func (r Rabbit) Listen(l logger.Interface, p *repo.ProductRepo) error {
	msg, err := r.ch.Consume(
		r.consume,
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

		var order rpcEntity.OrderConsume
		err = json.Unmarshal([]byte(str), &order)
		if err != nil {
			l.Error(fmt.Errorf("rabbitmq: unmarshal to map: %w", err))
		}

		if err == nil && m.Body != nil {
			err = r.PublishDish(l, p, order)
			if err != nil {
				l.Error(fmt.Errorf("rabbitmq: convert to id: %w", err))
			}
		}
	}

	return nil
}
