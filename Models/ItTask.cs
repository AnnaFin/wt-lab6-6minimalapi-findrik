namespace wt_lab6_6minimalapi_findrik.Models;

public class ItTask
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string Priority { get; set; } = "Medium";
    public double EstimatedHours { get; set; }
    public bool IsCompleted { get; set; }
}