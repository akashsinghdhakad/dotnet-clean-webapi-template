using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using dotnetWebApiCoreCBA.Common;
using dotnetWebApiCoreCBA.Models.DTOs.Todo;
using dotnetWebApiCoreCBA.Services.Interfaces;

namespace dotnetWebApiCoreCBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // require JWT by default
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _service;

        public TodoController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<TodoResponse>>>> GetAll()
        {
            var data = await _service.GetAllAsync();
            return Ok(ApiResponse<IEnumerable<TodoResponse>>.Ok(data));
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ApiResponse<TodoResponse>>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null)
                return NotFound(ApiResponse<TodoResponse>.Fail("NOT_FOUND", "Todo not found"));

            return Ok(ApiResponse<TodoResponse>.Ok(item));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<TodoResponse>>> Create([FromBody] TodoCreateRequest request)
        {
            var created = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<TodoResponse>.Ok(created));
        }
    }
}
