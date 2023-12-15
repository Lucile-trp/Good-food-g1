using Host.Core.Models;

namespace Host.Command.Models
{
    public class Command : ModelBase
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
    }
}
