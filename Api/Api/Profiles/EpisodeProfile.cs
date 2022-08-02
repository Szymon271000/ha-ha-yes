namespace Api.Profiles
{
    public class EpisodeProfile : Profile
    {
        public EpisodeProfile() 
        {
            CreateMap<Episode, SimpleEpisodeDTO>();
        }
    }
}
