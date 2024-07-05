namespace SimpleTodoList.Contracts;

public record TodoDto(Guid Id, string Title, string Description, DateTime Date, bool IsCompleted);