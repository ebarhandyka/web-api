using API.Models;
using API.Repositories.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversityController : ControllerBase
    {
        private readonly IUniversityRepository _university;
        public UniversityController(IUniversityRepository university)
        {
            _university = university;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUniversity()
        {
            var identity = await _university.GetAllAsync();
            try
            {
                if (identity == null)
                {
                    return NotFound(new
                    {
                        code = StatusCodes.Status404NotFound,
                        status = HttpStatusCode.NotFound.ToString(),
                        data = new
                        {
                            message = "Data Not Found!"
                        }
                    });

                }
                return Ok(new
                {
                    Code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = identity,
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Error");
            }
        }
        [HttpGet("{id}", Name = "GetAllUniversityiId")]
        public async Task<IActionResult> GetAllUniversityiId(int id)
        {
            var identity = await _university.GetByIdAsync(id);
            try
            {
                if (identity == null)
                {
                    return NotFound(new
                    {
                        Code = StatusCodes.Status404NotFound,
                        status = HttpStatusCode.NotFound,
                        data = new

                        {
                            identity,
                            message = "data not found"
                        }

                    });
                }
                return Ok(new
                {
                    Code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = new

                    {
                        identity,
                        message = "data find"
                    }

                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(University university)
        {

            var identity = await _university.InsertAsync(university);

            try
            {
                if (university != null)
                {
                    return BadRequest(new
                    {
                        Code = StatusCodes.Status401Unauthorized,
                        status = HttpStatusCode.MultiStatus,
                        data = identity
                    });
                }
                return Ok(new
                {
                    Code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = new
                    {
                        identity,
                        message = "Insert Success"
                    }

                });

            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Error");
            }
        }
        [HttpDelete("id")]
        public async Task<IActionResult> Remove(int id)
        {
            var getdata = await _university.GetByIdAsync(id);
            var identity = await _university.DeleteAsync(id);
            if (getdata == null)
            {
                return NotFound(new
                {
                    Code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound,
                    data = new
                    {
                        identity,
                        messege = "Delete failed"
                    }
                });

            }
            return Ok(new
            {
                Code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK,
                data = new
                {
                    identity,
                    messege = "Delete Success"
                }
            });


        }

        [HttpPut]
        public async Task<IActionResult> updtae(University university)
        {
            var identity = await _university.UpdateAsync(university);
            return Ok(new
            {
                Code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK,
                data = identity

            });
        }
    }
}
