using AudioBooksLibrary.Application.Abstractions.DTOs;
using AudioBooksLibrary.Core.Entities;
using AutoMapper;

namespace AudioBooksLibrary.Application.Mapping;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<Audiobook, AudiobookDto>()
            .ForMember(d => d.PartsCount, m => m.MapFrom(s => s.Parts.Count));

        CreateMap<Audiobook, AudiobookDetailsDto>()
            .ForMember(d => d.Parts, m => m.MapFrom(s => s.Parts.OrderBy(p => p.Index)));

        CreateMap<AudiobookPart, AudiobookPartDto>();

        CreateMap<PlaybackProgress, PlaybackProgressDto>();

        CreateMap<TimelineMarker, TimelineMarkerDto>()
            .ForMember(d => d.CreatedByUserName, m => m.MapFrom(s => s.CreatedByUser.DisplayName))
            .ForMember(d => d.LikesCount, m => m.MapFrom(s => s.Likes.Count));
    }
}