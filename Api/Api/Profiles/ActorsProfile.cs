
namespace Api.Profiles
{
    public class ActorsProfile : Profile
    {
        public ActorsProfile()
        {
            CreateMap<ActorUpdateDto, Actor>();
        }
    }
}
