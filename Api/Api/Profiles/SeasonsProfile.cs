namespace Api.Profiles
{
    public class SeasonsProfile : Profile
    {
        public SeasonsProfile()
        {
            CreateMap<SeasonUpdateDto, Actor>();
        }
    }
}
