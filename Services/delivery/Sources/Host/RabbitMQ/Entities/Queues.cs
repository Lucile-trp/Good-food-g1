namespace RabbitMQ
{
    public static class Queues
    {
        public readonly static string QueueBase = "goodfood.queue.";

        public static string GetDish => string.Concat(QueueBase, "getDish");
        public static string SendDish => string.Concat(QueueBase, "sendDishId");
    }
}
