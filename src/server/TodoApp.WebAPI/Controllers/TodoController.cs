using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.WebAPI;

/// <summary>
/// Controller for managing Todo items.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TodoController(TodoDbContext context) : ControllerBase
{
    private readonly TodoDbContext _context = context;

    /// <summary>
    /// Get all Todo items.
    /// </summary>
    /// <returns>A list of Todo items.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Todo>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
    {
        var result = await _context.Todos.ToListAsync();
        return Ok(result);
    }

    /// <summary>
    /// Get a Todo item by its ID.
    /// </summary>
    /// <param name="id">The ID of the Todo item.</param>
    /// <returns>The Todo item with the specified ID.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> GetTodoById(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        return Ok(todo);
    }

    /// <summary>
    /// Create a new Todo item.
    /// </summary>
    /// <param name="todo">The Todo item to create.</param>
    /// <returns>The created Todo item.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status201Created)]
    public async Task<ActionResult<Todo>> CreateTodo(Todo todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
    }

    /// <summary>
    /// Update an existing Todo item.
    /// </summary>
    /// <param name="id">The ID of the Todo item to update.</param>
    /// <param name="todo">The updated Todo item.</param>
    /// <returns>The updated Todo item.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> UpdateTodo(int id, Todo todo)
    {
        if (id != todo.Id)
        {
            return BadRequest();
        }

        _context.Entry(todo).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (await _context.Todos.FindAsync(id) == null)
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(todo);
    }

    /// <summary>
    /// Delete a Todo item by its ID.
    /// </summary>
    /// <param name="id">The ID of the Todo item to delete.</param>
    /// <returns>The deleted Todo item.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Todo), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Todo>> DeleteTodoById(int id)
    {
        var todo = await _context.Todos.FindAsync(id);

        if (todo == null)
        {
            return NotFound();
        }

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();

        return Ok(todo);
    }
}