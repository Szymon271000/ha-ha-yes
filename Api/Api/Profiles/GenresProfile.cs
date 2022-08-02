namespace Api.Profiles
{
    public class GenresProfile : Profile
    {
        public GenresProfile()
        {
            CreateMap<GenreUpdateDto, Actor>();
        }
    }
}
