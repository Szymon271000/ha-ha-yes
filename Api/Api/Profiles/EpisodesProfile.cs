namespace Api.Profiles
{
    public class EpisodesProfile : Profile
    {
        public EpisodesProfile()
        {
            CreateMap<EpisodeUpdateDto, Actor>();
            CreateMap<EpisodeCreateDTO, Episode>();
            CreateMap<Episode, EpisodeCreateDTO>();
            CreateMap<Episode, EpisodeGetDTO>();
            CreateMap<EpisodeGetDTO, Episode>();
            CreateMap<EpisodeUpdateDto, Episode>().ReverseMap();
        }
    }
}
