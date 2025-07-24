namespace TodoApi.Models
{
    public class Todo
    {
        public int Id { get; set; } // Chave Primária
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        // Chave Estrangeira para ligar esta tarefa a um usuário.
        public int UserId { get; set; }
        // Propriedade de Navegação para o EF Core entender o relacionamento.
        public User? User { get; set; }
    }
}