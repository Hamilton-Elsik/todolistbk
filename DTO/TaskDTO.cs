using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoListBk.DTO;

public class TaskDTO
{
    public string TaskId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public bool Completed { get; set; }
    public string UserId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
