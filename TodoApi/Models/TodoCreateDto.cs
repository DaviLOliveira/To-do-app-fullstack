using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TodoApi.Models
{
    public class TodoCreateDto
    {
        [Required]
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
