using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace PetStore.Core.Dtos.PetDto
{
    public record CreatePetDto(
        [MaxLength(50)] string Name,
        [Length(50, 250, ErrorMessage = ErrorMessages.MaxAndMinValidation)] string Description,
        short Age,
        List<IFormFile> Images,
        double Weight,
        Gender Gender,
        decimal Price,
        int UserId
    );
}
