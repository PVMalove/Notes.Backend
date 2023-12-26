using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteList;

public class NoteLookupDTO : IMapWith<Note>
{
    public Guid ID { get; set; }
    public string Title { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Note, NoteLookupDTO>()
            .ForMember(noteDTO => noteDTO.ID,
                opt => opt.MapFrom(note => note.ID))
            .ForMember(noteDTO => noteDTO.Title,
                opt => opt.MapFrom(note => note.Title));
    }
}