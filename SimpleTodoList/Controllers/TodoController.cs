using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleTodoList.Contracts;
using SimpleTodoList.DataAccess;
using SimpleTodoList.Models;

namespace SimpleTodoList.Controllers;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    private readonly TodoDbContext _dbContext;
    
    public TodoController(TodoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateTodo([FromBody]CreateTodoRequest request, CancellationToken ct)
    {
        var todo = new Todo(request.Title, request.Description);

        await _dbContext.Todos.AddAsync(todo, ct);
        await _dbContext.SaveChangesAsync(ct);
        
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetTodo([FromQuery]GetTodosRequest request, CancellationToken ct)
    {
        var todosQuery = _dbContext.Todos
            .Where(t => string.IsNullOrWhiteSpace(request.Search) ||
                        t.Title.ToLower().Contains(request.Search.ToLower()));

        Expression<Func<Todo, object>> selectorKey = request.SortItem?.ToLower() switch
        {
            "date" => todo => todo.Date,
            "title" => todo => todo.Title,
            _ => todo => todo.Id
        };
        
        todosQuery = request.SortOrder == "desc" 
            ? todosQuery.OrderByDescending(selectorKey) 
            : todosQuery.OrderBy(selectorKey);

        var todos = await todosQuery
            .Select(t => new TodoDto(t.Id, t.Title, t.Description, t.Date, t.IsCompleted))
            .ToListAsync(ct);

        return Ok(new GetTodosResponse(todos));
    }
}