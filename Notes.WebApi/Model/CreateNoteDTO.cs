using AutoMapper;
using System.ComponentModel.DataAnnotations;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.Commands.CreateNote;

namespace Notes.WebApi.Model;

public class CreateNoteDTO : IMapWith<CreateNoteCommand>
{
    [Required] 
    public string Title { get; set; } = default!;
    public string Details { get; set; } = default!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateNoteDTO, CreateNoteCommand>()
            .ForMember(command => command.Title, 
                opt => opt.MapFrom(dto => dto.Title))!
            .ForMember(command => command.Details,
                opt => opt.MapFrom(dto => dto.Details));
    }
}