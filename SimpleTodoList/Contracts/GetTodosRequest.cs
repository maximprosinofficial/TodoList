namespace SimpleTodoList.Contracts;

public record GetTodosRequest(string? Search, string? SortItem, string? SortOrder);