namespace Api.Profiles
{
    public class SeriesProfile: Profile
    {
        public SeriesProfile()
        {
            CreateMap<Serie, SimpleSerieDTO>();
            CreateMap<SerieUpdateDto, Serie>().ReverseMap();
            CreateMap<Serie, SerieWithSeasonsDTO>()
                .ForMember(dest => dest.Seasons, opt => opt.MapFrom(src => src.SerieSeasons));
        }
    }
}
