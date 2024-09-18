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
        public async Task<ActionResult<Todo>> PostToDoItem(Todo toDoItem)
        {
            //toDoItem.Id = _random.Next(10000, 100000);
            _context.Todos.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoById), new { id = toDoItem.Id }, toDoItem);
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
        public async Task<IActionResult> updateTodo(int id, string Title, string Description, Boolean IsCompleted)
        {
            try
            {
                var existingTodo = await _context.Todos.FindAsync(id);
                if(Description != null) existingTodo.Description = Description;
                if(IsCompleted != null) existingTodo.IsCompleted = IsCompleted;
               if(Title != null) existingTodo.Title = Title;
            }
            catch (Exception ex) {

                return BadRequest(ex.Message);
            }

           

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
