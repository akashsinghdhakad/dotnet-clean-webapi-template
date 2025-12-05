using System.ComponentModel.DataAnnotations;

namespace DotnetWebApiCoreCBA.Models.DTOs.Todo
{
    public class TodoCreateRequest
    {
        [Required]
        public string Title { get; set; } = default!;
    }
}
