using System.ComponentModel.DataAnnotations;

namespace Animals.Models
{
    public class Animal
    {
        [Key]
        public int IdAnimal { get; set; }
        [Required, StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string? Description { get; set; }
        [Required, StringLength(200)]
        public string Category { get; set; }
        [Required, StringLength(200)]
        public string Area { get; set; }

        public Animal()
        {
        }

        public Animal(int idAnimal, string name, string description, string category, string area)
        {
            IdAnimal = idAnimal;
            Name = name;
            Description = description;
            Category = category;
            Area = area;
        }

        public Animal(string name, string description, string category, string area)
        {
            Name = name;
            Description = description;
            Category = category;
            Area = area;
        }

        public override string ToString()
        {
            return $"Animal: {IdAnimal}, {Name}, {Description}, {Category}, {Area}";
        }

    }
}
