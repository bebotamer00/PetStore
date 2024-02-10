namespace PetStore.Core.Dtos.UserDto
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "Current password is required"), DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "New password is required"), DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Passwords do not match"), DataType(DataType.Password)]
        public string ConfirmNewPassword { get; set; }
    }
}
