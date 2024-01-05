namespace Host.Core
{
    public static class Queues
    {
        public readonly static string QueueBase = "goodfood.queue.";

        public static string Order => string.Concat(QueueBase, "order");
    }
}
