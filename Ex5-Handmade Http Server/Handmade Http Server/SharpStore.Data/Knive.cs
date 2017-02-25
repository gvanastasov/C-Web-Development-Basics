using System.ComponentModel.DataAnnotations;

namespace SharpStore.Data
{
    public class Knive
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

    }
}
