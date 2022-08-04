
namespace Api.Profiles
{
    public class ActorsProfile : Profile
    {
        public ActorsProfile()
        {
            CreateMap<Actor, ActorGetDTO>();
            CreateMap<ActorGetDTO, Actor>();
            CreateMap<ActorUpdateDto, Actor>().ReverseMap();
        }
    }
}
