using Microsoft.AspNetCore.Mvc;
using OrderManagement.Application.DTO.ApiUsers;
using OrderManagement.Application.Repositories;

namespace OrderManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenRepository _tokenRepository;

        public TokenController(ITokenRepository tokenRepository)
        {
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Token(ApiUserModel model)
        {
            var result = await _tokenRepository.GetToken(model);
            return Ok(result);
        }
    }
}