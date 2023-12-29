using AutoMapper;
using VirtualPetCareAPI.Data.DTOs;
using VirtualPetCareAPI.Data.Entities;
namespace VirtualPetCareAPI.Service.Mapping;

public class MapProfile : Profile
{
    public MapProfile()
    {
        // User
        CreateMap<UserDTO, User>().ReverseMap();

        // Pet
        CreateMap<PetDTO, Pet>().ReverseMap();

        // Activity
        CreateMap<ActivityDTO, Activity>().ReverseMap();

        // HealthStatus
        CreateMap<HealthStatusDTO, HealthStatus>().ReverseMap();

        // Nutrient
        CreateMap<NutrientDTO, Nutrient>().ReverseMap();

        // SocialInteraction
        CreateMap<SocialInteractionDTO, SocialInteraction>().ReverseMap();

        // Training
        CreateMap<TrainingDTO, Training>().ReverseMap();
    }
}
