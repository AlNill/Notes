using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notes.Application.Common.Exceptions;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;
using Notes.Domain;

namespace Notes.Application.Notes.Queries.GetNoteDetails
{
    public class GetNoteDetailsQueryHandler : IRequestHandler<GetNoteDetailsQuery, NoteDetailsVm>
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandler(INotesDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<NoteDetailsVm> Handle(
            GetNoteDetailsQuery request, CancellationToken cancellationToken)
        {
            var note =
                await _dbContext.Notes.FirstOrDefaultAsync<Note>(x =>
                x.Id == request.Id, cancellationToken);

            if (note == null)
                throw new NotFoundException(nameof(Note), request.Id);

            return _mapper.Map<NoteDetailsVm>(note);
        }
    }
}
