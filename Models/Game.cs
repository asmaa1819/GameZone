﻿
namespace GameZone.Models
{
    public class Game :BaseEntity
    {
        

        [MaxLength(2500)]
        public string Descreption { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Cover { get; set; } = string.Empty;
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }
        public ICollection<GameDevice> Device { get; set; }= new List<GameDevice>();



    }
}
