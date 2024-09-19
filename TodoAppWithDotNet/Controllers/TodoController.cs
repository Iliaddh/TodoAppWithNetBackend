using Microsoft.AspNetCore.Mvc;
using TodoAppWithDotNet.Data;
using TodoAppWithDotNet.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace TodoAppWithDotNet.Controllers
{
    public class TodoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly Random _random = new Random();
        public TodoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("/")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        //Adding todo
        [HttpPost("/")]
        public async Task<ActionResult<Todo>> PostToDoItem([FromBody] Todo toDoItem)
        {
            //toDoItem.Id = _random.Next(10000, 100000);
            try
            {
                _context.Todos.Add(toDoItem);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { 
                return BadRequest("Problem occured when adding todo to database");
            
            }

            return CreatedAtAction(nameof(GetTodoById), new { id = toDoItem.Id }, toDoItem);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if(todo == null)
            {
                return NotFound("Todo Not Found!");
            }

            return Ok(todo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] Todo toDoItem)
        {
            try
            {
                // Find the existing Todo by id
                var existingTodo = await _context.Todos.FindAsync(id);
                if (existingTodo == null)
                {
                    return NotFound("Todo not found.");
                }

                // Destructure the body and update the fields conditionally
                if (toDoItem.Description != null)
                    existingTodo.Description = toDoItem.Description;

                if (toDoItem.IsCompleted != null)
                    existingTodo.IsCompleted = toDoItem.IsCompleted;

                if (toDoItem.Title != null)
                    existingTodo.Title = toDoItem.Title;

                // Save changes to the database
                await _context.SaveChangesAsync();

                return NoContent(); // Return NoContent to indicate success with no body
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id) {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound("Todo doesnt exist!");
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();

        }


        
    }
}
