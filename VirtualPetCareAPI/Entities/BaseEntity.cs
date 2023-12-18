using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VirtualPetCareAPI.Entities;

public abstract class BaseEntity<T> // generic entity tanımı
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required T Id { get; set; }
    public required string Name { get; set; }
    public required T PetId { get; set; }

}
