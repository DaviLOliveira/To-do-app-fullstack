
namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; } // Chave Primária
        public string Username { get; set; }
        public string Password { get; set; }

        // Propriedade de Navegação: Um usuário pode ter uma lista de tarefas.
        public List<Todo> Todos { get; set; } = new List<Todo>();
    }
}