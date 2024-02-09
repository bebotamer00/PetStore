using Microsoft.AspNetCore.Mvc;
using PetStore.Core.Dtos;
using PetStore.Core.Interfaces;
using PetStore.Core.Models;

namespace PetStore.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize("Admin")]
    public class UserssController(IUnitOfWork unitOfWork) : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var getAllUsers = await _unitOfWork.UserRepository.GetAll();
            
            if (getAllUsers is null)
                return NotFound();
            
            return Ok(getAllUsers);
        }

        [HttpGet("Details/{id:int}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var getUserById = await _unitOfWork.UserRepository.GetById(id);
            
            if (getUserById is null)
                return NotFound();
            
            return Ok(getUserById);
        }

        [HttpPost("AddNewUser")]
        public async Task<IActionResult> AddNewUser([FromForm]UserDto userDto)
        {
            if (!ModelState.IsValid) 
                return BadRequest();

            if (userDto is null)
                return BadRequest();

            User user = new()
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password,
                CreatedDate = DateTime.Now,
            };

            await _unitOfWork.UserRepository.Add(user);

            return Ok(userDto);
        }

        [HttpPut("UpdateUser/{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromForm]UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (userDto is null)
                return BadRequest();

            User user = await _unitOfWork.UserRepository.GetById(id);
            if (user is null) 
                return NotFound();

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.CreatedDate = DateTime.Now;

            await _unitOfWork.UserRepository.Update(id, user);

            return Ok(user);
        }

        [HttpDelete("DeleteUser/{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var user = _unitOfWork.UserRepository.GetById(id);

            if (user is null)
                return BadRequest();

            await _unitOfWork.UserRepository.Delete(id);

            return Ok(user);
        }
    }
}
