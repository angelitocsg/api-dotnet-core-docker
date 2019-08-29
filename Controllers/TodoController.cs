using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreDocker.Context;
using DotNetCoreDocker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TodoController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> Get()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET /api/todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> Get(int? id)
        {
            if (id == null) { return NotFound(); }

            var todo = await _context.Todos.FirstOrDefaultAsync(t => t.ID == id);

            if (todo == null) { return NotFound(); }

            return todo;
        }

        // POST /api/todo
        [HttpPost]
        public async Task<ActionResult<Todo>> Post([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            _context.Add(todo);

            await _context.SaveChangesAsync();

            return todo;
        }

        // PUT /api/todo/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Todo>> Put(int id, [FromBody] Todo todo)
        {
            if (id != todo.ID) { return NotFound(); }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _context.Update(todo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id)) { NotFound(); }
                else { throw; }
            }

            return todo;
        }

        // DELETE api/todo/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Todo>> Delete(int id)
        {
            var todo = await _context.Todos.FindAsync(id);

            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
            
            return todo;
        }

        private bool TodoExists(int id)
        {
            return _context.Todos.Any(t => t.ID == id);
        }
    }
}