namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Admin")]
    public class UsersController(IUnitOfWork unitOfWork, IMapper mapper) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllUsers = await _unitOfWork.UserRepository.GetAll();

            if (getAllUsers is null)
                return NotFound();

            var model = _mapper.Map<IEnumerable<UserDto>>(getAllUsers);

            return Ok(model);
        }

        [HttpGet("PetsByUser/{userId:int}")]
        public async Task<IActionResult> GetPetsByUser(int userId)
        {
            var petsByUser = await _unitOfWork.UserRepository.GetPetsByUserAsync(userId);

            if (petsByUser is null || !petsByUser.Any())
                return NotFound();

            return Ok(petsByUser);
        }
        
        [HttpGet("PetsByUserName/{userName}")]
        public async Task<IActionResult> GetPetsByUserName(string userName)
        {
            var petsByUser = await _unitOfWork.UserRepository.GetPetsByUserNameAsync(userName);

            if (petsByUser is null || !petsByUser.Any())
                return NotFound();

            return Ok(petsByUser);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var getUserById = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (getUserById is null)
                return NotFound();

            return Ok(getUserById);
        }

        [HttpGet("GetUserByEmail/{email}")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            if (email is null)
                return NotFound();

            var getUserByEmail = await _unitOfWork.UserRepository.GetUserByEmail(email);

            if (getUserByEmail is null)
                return NotFound();

            return Ok(getUserByEmail);
        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromForm] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (userDto is null)
                return BadRequest();

            var model = _mapper.Map<UserDto, User>(userDto);

            await _unitOfWork.UserRepository.Add(model);

            return Ok(userDto);
        }

        [HttpPut("UpdateUser/{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (userDto is null)
                return BadRequest();

            User user = await _unitOfWork.UserRepository.GetByIdAsync(id);
            if (user is null)
                return NotFound();

            _mapper.Map(userDto, user);

            await _unitOfWork.UserRepository.Update(id, user);

            return Ok(user);
        }

        [HttpPut("UpdatePassword/{id:int}")]
        public async Task<IActionResult> UpdatePassword(int id, [FromForm] ChangePasswordDto changePasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user is null)
                return BadRequest();

            user.Password = changePasswordDto.NewPassword;

            await _unitOfWork.UserRepository.UpdatePassword(id, changePasswordDto.NewPassword);

            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user is null)
                return BadRequest();

            await _unitOfWork.UserRepository.Delete(id);

            return Ok(user);
        }

        [HttpGet("CountUsers")]
        public async Task<IActionResult> GetCountUsers()
        {
            var userCount = await _unitOfWork.UserRepository.Count();

            return Ok(userCount);
        }
    }
}
