using Microsoft.AspNetCore.Mvc;
using TodoAppWithDotNet.Data;
using TodoAppWithDotNet.Models;
using System.Threading;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TodoAppWithDotNet.Controllers
{
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if(todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> updateTodo(int id, Todo todo)
        {
            if(id != todo.Id)
            {
                return BadRequest();
            }

            var existingTodo = await _context.Todos.FindAsync(id);
            if (existingTodo == null)
            {
                return NotFound();
            }

            existingTodo.Description = todo.Description;
            existingTodo.IsCompleted = todo.IsCompleted;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id) {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();

        }


        
    }
}
