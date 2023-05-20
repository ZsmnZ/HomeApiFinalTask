using FluentValidation;
using HomeApi.Contracts.Models.Rooms;
using System.Linq;

namespace HomeApi.Contracts.Validation
{
    public class EditRoomRequestValidator:AbstractValidator<EditRoomRequest>
    {

        public EditRoomRequestValidator()
        {
            RuleFor(x => x.NewName)
                .NotEmpty()
                .Must(BeSupported)
                .WithMessage($"Please choose one of the following locations: {string.Join(", ", Values.ValidRooms)}");
        }

        /// <summary>
        ///  Метод кастомной валидации для свойства location
        /// </summary>
        private bool BeSupported(string location)
        {
            // Проверим, содержится ли значение в списке допустимых
            return Values.ValidRooms.Any(e => e == location);
        }
    }
}
