namespace dotnetWebApiCoreCBA.Models.DTOs.Todo
{
    public class TodoResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public bool IsCompleted { get; set; }
    }
}
