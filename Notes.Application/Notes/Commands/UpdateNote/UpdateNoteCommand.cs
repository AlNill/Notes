using MediatR;
using Notes.Domain;

namespace Notes.Application.Notes.UpdateNote
{
    public class UpdateNoteCommand : IRequest
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
