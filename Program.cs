using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi;
using wt_lab6_6minimalapi_findrik.Models;
using wt_lab6_6minimalapi_findrik.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Регистрация Swashbuckle (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "IT Tasks API", Version = "v1" });
});

var app = builder.Build();

// 2. Настройка middleware для Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ================== ЭНДПОИНТЫ ==================

app.MapGet("/", () => "Minimal API работает!");

app.MapGet("/api/info", () =>
{
    return new
    {
        Application = "Web Technologies",
        Technology = "ASP.NET Core Minimal API",
        Version = "1.0",
        Domain = "Управление IT-проектами"
    };
});

// 1. GET: Получить все задачи (с фильтрацией)
app.MapGet("/api/tasks", (string? search) =>
{
    var tasks = ItTaskStore.Tasks.AsEnumerable();
    if (!string.IsNullOrWhiteSpace(search))
    {
        tasks = tasks.Where(x => x.Title.Contains(search, StringComparison.OrdinalIgnoreCase));
    }
    return Results.Ok(tasks);
});

// 2. GET: Получить задачу по ID
app.MapGet("/api/tasks/{id}", (int id) =>
{
    var task = ItTaskStore.Tasks.FirstOrDefault(x => x.Id == id);
    if (task == null) return Results.NotFound(new { Message = "Задача не найдена" });
    return Results.Ok(task);
});

// 3. POST: Создать задачу
app.MapPost("/api/tasks", (ItTask newTask) =>
{
    if (string.IsNullOrWhiteSpace(newTask.Title))
        return Results.BadRequest(new { Message = "Название задачи обязательно" });

    newTask.Id = ItTaskStore.Tasks.Count == 0 ? 1 : ItTaskStore.Tasks.Max(x => x.Id) + 1;
    ItTaskStore.Tasks.Add(newTask);
    return Results.Created($"/api/tasks/{newTask.Id}", newTask);
});

// 4. PUT: Обновить задачу
app.MapPut("/api/tasks/{id}", (int id, ItTask updatedTask) =>
{
    var task = ItTaskStore.Tasks.FirstOrDefault(x => x.Id == id);
    if (task == null) return Results.NotFound(new { Message = "Задача не найдена" });

    task.Title = updatedTask.Title;
    task.ProjectName = updatedTask.ProjectName;
    task.Priority = updatedTask.Priority;
    task.EstimatedHours = updatedTask.EstimatedHours;
    task.IsCompleted = updatedTask.IsCompleted;

    return Results.Ok(task);
});

// 5. DELETE: Удалить задачу
app.MapDelete("/api/tasks/{id}", (int id) =>
{
    var task = ItTaskStore.Tasks.FirstOrDefault(x => x.Id == id);
    if (task == null) return Results.NotFound(new { Message = "Задача не найдена" });

    ItTaskStore.Tasks.Remove(task);
    return Results.NoContent();
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");

//var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
//app.Run($"http://0.0.0.0:{port}");