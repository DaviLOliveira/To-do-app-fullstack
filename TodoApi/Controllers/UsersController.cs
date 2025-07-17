using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")] // Define a rota base: /api/users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        // O construtor recebe o DbContext via injeção de dependência
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: /api/users
        // Este método será usado para criar (registrar) um novo usuário.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
         

            _context.Users.Add(user); // Adiciona o novo usuário ao contexto do EF
            await _context.SaveChangesAsync(); // Salva as mudanças no banco de dados

            // Retorna um status 201 (Created) com o usuário recém-criado.
            // O CreatedAtAction gera uma URL para o novo recurso, o que é uma boa prática de API REST.
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // GET: /api/users/{id}
        // Um método para buscar um usuário específico pelo ID.
        // Precisamos dele para o CreatedAtAction funcionar corretamente.
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(); // Retorna 404 se o usuário não for encontrado
            }

            return user; // Retorna o usuário encontrado com status 200 (OK)
        }
    }
}
            