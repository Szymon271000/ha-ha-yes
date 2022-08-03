namespace Api.Profiles
{
    public class SeasonsProfile : Profile
    {
        public SeasonsProfile()
        {
            CreateMap<SeasonUpdateDto, Actor>();
            CreateMap<SeasonCreateDTO, Season>();
            CreateMap<Season, SeasonCreateDTO>();
            CreateMap<Season, SeasonGetDTO>();
            CreateMap<SeasonGetDTO, Season>();
        }
    }
}
