using API.Models;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationController : ControllerBase
    {
        private readonly IEducationRepository _educations;

        public EducationController(IEducationRepository educations)
        {
            _educations = educations;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var identity = await _educations.GetAllAsync();
            return Ok(identity);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Getbyid(int id)
        {
            var identity = await _educations.GetByIdAsync(id);
            return Ok(identity);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Education education)
        {
            var identity = await _educations.InsertAsync(education);
            return Ok(identity);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Education education)
        {
            var identity = await _educations.UpdateAsync(education);
            return Ok(identity);
        }
        [HttpDelete]

        public async Task<IActionResult> Delete(int id)
        {
            var identity = await _educations.DeleteAsync(id);
            return Ok(identity);
        }
    }
}
