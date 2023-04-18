using API.Models;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employee;
        public EmployeeController(IEmployeeRepository employee)
        {
            _employee = employee;
        }

        [HttpGet]
        public async Task<IActionResult> getall()
        {
            var identity = await _employee.GetAllAsync();

            return Ok(identity);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> getdatabyid(string id)
        {
            var identity = await _employee.GetByIdAsync(id);
            return Ok(identity);
        }

        [HttpGet("{name}", Name = "GetByUniversitas")]

        public async Task<IActionResult> GetByUniversitas(String name)
        {
            var id = 1;
            var getEmployee = await _employee.GetDataByRoleAsync(name, id);
            return Ok(getEmployee);
        }

        [HttpDelete]
        public async Task<IActionResult> removeEmployee(string id)
        {
            var identity = await _employee.DeleteAsync(id);

            return Ok(identity);
        }
        [HttpPost]
        public async Task<IActionResult> InserEmployee(Employee employee)
        {
            var identity = await _employee.InsertAsync(employee);

            return Ok(identity);
        }

    }
}
