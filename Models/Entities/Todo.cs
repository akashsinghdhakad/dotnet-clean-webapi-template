namespace dotnetWebApiCoreCBA.Models.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public bool IsCompleted { get; set; }
    }
}
