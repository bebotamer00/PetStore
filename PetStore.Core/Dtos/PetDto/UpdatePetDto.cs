using Microsoft.AspNetCore.Http;
using PetStore.Core.Models;

namespace PetStore.Core.Dtos.PetDto
{
    public record UpdatePetDto([MaxLength(50)] string Name,
        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)] string Description,
        short Age,
        string OldImage,
        IFormFile NewImage,
        double Weight,
        Gender Gender,
        decimal Price,
        int UserId
    );
}
