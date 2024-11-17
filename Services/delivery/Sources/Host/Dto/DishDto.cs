namespace Host.Dto
{
    public class DishDto
    {
        public int Id { get; set; }

        public string Title { get; set; }   

        public string Description { get; set; }

        public double Cost { get; set; }

        public int RestaurantId { get; set; }

        public int OrderId { get; set; }
    }
}