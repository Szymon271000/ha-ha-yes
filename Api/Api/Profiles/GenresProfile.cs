namespace Api.Profiles
{
    public class GenresProfile : Profile
    {
        public GenresProfile()
        {
            CreateMap<GenreUpdateDto, Actor>();
            CreateMap<GenreGetDTO, Genre>();
            CreateMap<Genre, GenreGetDTO>();
            CreateMap<GenreUpdateDto, Genre>().ReverseMap();
        }
    }
}
