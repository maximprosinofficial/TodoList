namespace SimpleTodoList.Models;

public class Todo
{
    public Todo(string title, string description)
    {
        Title = title;
        Description = description;
        Date = DateTime.UtcNow;
        IsCompleted = false;
    }
    
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }
}