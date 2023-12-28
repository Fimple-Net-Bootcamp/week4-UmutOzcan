namespace VirtualPetCareAPI.Data.Entities.Abstract;

public abstract class BaseEntity<Entity>
{
    public required Entity Id { get; set; }
}
