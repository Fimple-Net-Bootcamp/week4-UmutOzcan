using VirtualPetCareAPI.Data.Entities.Abstract;

namespace VirtualPetCareAPI.Data.Entities;

public class User : BaseEntity<int>
{
    public string Name { get; set; }
    public virtual List<Pet> Pets { get; set; }
}
