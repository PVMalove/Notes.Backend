using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.UpdateNote;

namespace Notes.WebApi.Model;

public class UpdateNoteDTO : IMapWith<UpdateNoteCommand>
{
    public Guid ID { get; set; }
    public string? Title { get; set; }
    public string? Details { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateNoteDTO, UpdateNoteCommand>()
            .ForMember(noteCommand => noteCommand.ID,
                opt => opt.MapFrom(noteDto => noteDto.ID))
            .ForMember(noteCommand => noteCommand.Title,
                opt => opt.MapFrom(noteDto => noteDto.Title))
            .ForMember(noteCommand => noteCommand.Details,
                opt => opt.MapFrom(noteDto => noteDto.Details));
    }
}