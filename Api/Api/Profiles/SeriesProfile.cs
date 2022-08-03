﻿namespace Api.Profiles
{
    public class SeriesProfile: Profile
    {
        public SeriesProfile()
        {
            CreateMap<Serie, SimpleSerieDTO>();
            CreateMap<SerieUpdateDto, Serie>().ReverseMap();
        }
    }
}
