using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers.User.DTO;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.User
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<String>> Login([FromBody] LoginDTO dto)
        {
            try
            {
                return Ok("Criando a api");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpPost]
        [Route("RegisterUser")]
        public async Task<ActionResult<String>> RegisterUser([FromBody] RegisterDTO dto)
        {
            try
            {
                return Ok("Criando a api");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
        [HttpPost]
        [Route("RegisterUserAdmin")]
        public async Task<ActionResult<String>> RegisterUserAdmin([FromBody] RegisterDTO dto)
        {
            try
            {
                return Ok("Criando a api");
            }
            catch (Exception e)
            {
                return StatusCode(500, "Ocorreu um erro interno no servidor: " + e.Message);
            }
        }
    }
}