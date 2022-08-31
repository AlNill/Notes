using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _dbContext;
        public UpdateNoteCommandHandler(INotesDbContext dbContext) => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note = 
                await _dbContext.Notes.FirstOrDefaultAsync<Note>(x => 
                x.Id == request.Id, cancellationToken);
            if (note == null)
                throw new NotFoundException(nameof(Note), request.Id);

            note.Title = request.Title;
            note.Details = request.Details;
            note.EditTime = DateTime.Now;

            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
