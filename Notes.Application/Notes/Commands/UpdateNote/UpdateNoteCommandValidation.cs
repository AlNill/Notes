using FluentValidation;
using Notes.Application.Notes.UpdateNote;

namespace Notes.Application.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidation: AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidation()
        {
            RuleFor(updateCommand => updateCommand.Title).NotEmpty().MaximumLength(250);
            RuleFor(updateCommand => updateCommand.UserId).NotEqual(Guid.Empty);
            RuleFor(updateCommand => updateCommand.Id).NotEqual(Guid.Empty);
        }
    }
}
