﻿namespace Api.Profiles
{
    public class EpisodesProfile : Profile
    {
        public EpisodesProfile()
        {
            CreateMap<EpisodeUpdateDto, Actor>();
        }
    }
}