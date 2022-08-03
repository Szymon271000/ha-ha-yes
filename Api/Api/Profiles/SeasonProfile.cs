

namespace Api.Profiles
{
    public class SeasonProfile: Profile
    {
        public SeasonProfile()
        {
            CreateMap<Season, SimpleSeasonDTO>();
        }
    }
}
