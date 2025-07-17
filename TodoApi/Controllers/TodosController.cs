using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TodosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            var todos = await _context.Todos.Where(t => t.UserId == 1).ToListAsync();
            return todos;
        }

        // POST: api/todos
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo([FromBody] Todo todo)
        {
            if (string.IsNullOrWhiteSpace(todo.Text))
            {
                return BadRequest("O texto da tarefa não pode ser vazio.");
            }
            todo.UserId = 1;
            todo.CreatedAt = DateTime.UtcNow;
            todo.IsCompleted = false;
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }

        // GET: api/todos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null || todo.UserId != 1)
            {
                return NotFound();
            }
            return todo;
        }

        // ***** MÉTODO PUT REESCRITO E SIMPLIFICADO *****
        // PUT: api/todos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, [FromBody] Todo todoUpdatePayload)
        {
            // 1. Encontra a tarefa existente no banco de dados
            var todoToUpdate = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);

            // 2. Se não encontrar ou se não pertencer ao usuário, retorna erro
            if (todoToUpdate == null || todoToUpdate.UserId != 1)
            {
                return NotFound();
            }

            // 3. Atualiza apenas os campos que permitimos mudar
            todoToUpdate.Text = todoUpdatePayload.Text;
            todoToUpdate.IsCompleted = todoUpdatePayload.IsCompleted;

            // 4. Salva as alterações. O EF Core já sabe o que foi modificado.
            await _context.SaveChangesAsync();

            return NoContent(); // Sucesso
        }

        // DELETE: api/todos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null || todo.UserId != 1)
            {
                return NotFound();
            }
            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}