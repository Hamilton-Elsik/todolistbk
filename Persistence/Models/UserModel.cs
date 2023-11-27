using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ToDoListBk.Persistence.Models;

public class UserModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? FirstName { get; set; }

    [Column(TypeName = "varchar(50)")]
    public string? LastName { get; set; }
    [Required]
    [Column(TypeName = "varchar(50)")]
    public string? Email { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string? PhoneNumber { get; set; }
    public bool IsActive { get; set; }
    [Column(TypeName = "varchar(50)")]
    public string? Password { get; set; }
    public DateTime? CreatedAt { get; set; }
    internal object Include(Func<object, object> value)
    {
        throw new NotImplementedException();
    }
}
