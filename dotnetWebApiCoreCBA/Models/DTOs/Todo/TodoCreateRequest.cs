using System.ComponentModel.DataAnnotations;

namespace dotnetWebApiCoreCBA.Models.DTOs.Todo
{
    public class TodoCreateRequest
    {
        [Required]
        public string Title { get; set; } = default!;
    }
}
