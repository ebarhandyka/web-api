using API.Repositories.Contracts;
using API.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var identity = await _accountRepository.RegisterAsync(registerVM);

            return Ok(identity);
        }
        [HttpPost("login")]
        public async Task<IActionResult> login(LoginVM loginVM)
        {
            var identity = await _accountRepository.LoginAsync(loginVM);
            return Ok(identity);
        }
    }
}
