using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Notes.UpdateNote;

namespace Notes.WebApi.Models
{
    public class UpdateNoteDto: IMapWith<UpdateNoteCommand>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteDto, UpdateNoteCommand>()
                .ForMember(updateNoteCommand => updateNoteCommand.Id,
                opt => opt.MapFrom(updateNoteDto => updateNoteDto.Id))
                .ForMember(updateNoteCommand => updateNoteCommand.Title,
                opt => opt.MapFrom(updateNoteDto => updateNoteDto.Title))
                .ForMember(updateNoteCommand => updateNoteCommand.Details,
                opt => opt.MapFrom(updateNoteDto => updateNoteDto.Details));
        }
    }
}
