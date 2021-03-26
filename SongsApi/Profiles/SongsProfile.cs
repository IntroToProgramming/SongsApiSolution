using AutoMapper;
using SongsApi.Domain;
using SongsApi.Models.Songs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongsApi.Profiles
{
    public class SongsProfile : Profile
    {
        public SongsProfile()
        {

            CreateMap<Song, GetSongsResponse>();
            CreateMap<Song, SongSummaryItem>();
            CreateMap<Song, GetSASongResponse>();

            CreateMap<PostSongRequest, Song>()
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom((_) => true))
                .ForMember(dest => dest.AddedToInventory, opt => opt.MapFrom(_ => DateTime.Now));

        }
    }
}
