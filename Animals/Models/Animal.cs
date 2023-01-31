using System.ComponentModel.DataAnnotations;

namespace Animals.Models
{
    public class Animal : IValidatableObject
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                yield return new ValidationResult("Name is required", new[] { nameof(Name) });
            }
            if (Name.Length > 200)
            {
                yield return new ValidationResult("Name is too long", new[] { nameof(Name) });
            }
            if (Description is { Length: > 200 })
            {
                yield return new ValidationResult("Description is too long", new[] { nameof(Description) });
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                yield return new ValidationResult("Category is required", new[] { nameof(Category) });
            }
            if (Category.Length > 200)
            {
                yield return new ValidationResult("Category is too long", new[] { nameof(Category) });
            }
            if (string.IsNullOrWhiteSpace(Area))
            {
                yield return new ValidationResult("Area is required", new[] { nameof(Area) });
            }
            if (Area.Length > 200)
            {
                yield return new ValidationResult("Area is too long", new[] { nameof(Area) });
            }
        }

        public static Animal Convert(object id, object name, object description, object category, object area)
        {
            return new Animal
            {
                IdAnimal = System.Convert.ToInt32(id),
                Name = System.Convert.ToString(name),
                Description = System.Convert.ToString(description),
                Category = System.Convert.ToString(category),
                Area = System.Convert.ToString(area)
            };
        }
    }
}
