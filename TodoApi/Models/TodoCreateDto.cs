using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class TodoCreateDto
    {
        [Required]
        public string Text { get; set; }
    }
}
