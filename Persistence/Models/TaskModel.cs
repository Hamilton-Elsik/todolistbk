using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoListBk.Persistence.Models;

public class TaskModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int TaskId { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? Name { get; set; }

    [Column(TypeName = "varchar(250)")]
    public string? Description { get; set; }

    public bool Completed { get; set; }
    public int UserId { get; set; }
    public bool IsActive { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    internal object Include(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
