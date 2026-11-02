using wt_lab6_6minimalapi_findrik.Models;

namespace wt_lab6_6minimalapi_findrik.Data;

public static class ItTaskStore
{
    public static List<ItTask> Tasks { get; } = new()
    {
        new ItTask { Id = 1, Title = "Разработка API", ProjectName = "CRM System", Priority = "High", EstimatedHours = 40, IsCompleted = false },
        new ItTask { Id = 2, Title = "Дизайн интерфейса", ProjectName = "CRM System", Priority = "Medium", EstimatedHours = 20, IsCompleted = true },
        new ItTask { Id = 3, Title = "Тестирование", ProjectName = "Mobile Banking", Priority = "High", EstimatedHours = 30, IsCompleted = false },
        new ItTask { Id = 4, Title = "Настройка CI/CD", ProjectName = "Mobile Banking", Priority = "Low", EstimatedHours = 15, IsCompleted = true },
        new ItTask { Id = 5, Title = "Code Review", ProjectName = "CRM System", Priority = "Medium", EstimatedHours = 10, IsCompleted = false }
    };
}