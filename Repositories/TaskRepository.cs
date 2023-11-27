using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using ToDoListBk.Persistence.Context;
using ToDoListBk.Persistence.Models;
using ToDoListBk.Repositories.Interfaces;

namespace ToDoListBk.Repositories;

public class TaskRepository:ITaskRepository
{
    private readonly AplicationDbContext _context;
    public TaskRepository(AplicationDbContext context)
    {
        _context = context;
    }

    public async Task SaveTask(TaskModel task)
    {
        _context.Add(task);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskModel> GetTaskId(int id)
    {
        var task = await _context.Task.Where(x => x.TaskId == id)
                                                                .FirstOrDefaultAsync();
        return task;
    }

    public async Task<List<TaskModel>> GetTasks(int userId)
    {
        var listTask= await _context.Task.Where(x => x.UserId == userId).Select(o => new TaskModel
        {
            TaskId = o.TaskId,
            Name = o.Name,
            Completed = o.Completed,
            Description = o.Description,
            UserId = o.UserId,
            IsActive = o.IsActive,
            CreatedAt = o.CreatedAt,
            UpdatedAt = o.UpdatedAt,
        }).ToListAsync();
        return listTask;
    }

    public async Task deleteTask(int TaskId)
    {
        var enDelete = _context.Task.Find(TaskId);
        if (enDelete != null)
        {
            _context.Task.Remove(enDelete);
            await _context.SaveChangesAsync();
        }

    }

    public async Task UpdateTask(TaskModel task)
    {
        var auxtask = _context.Task.Where(x => x.TaskId == task.TaskId
                                                      )
                                                        .ToList();

        auxtask[0].Name = task.Name;
        auxtask[0].Description = task.Description;
        auxtask[0].Completed = task.Completed;
        auxtask[0].UserId = task.UserId;
        auxtask[0].CreatedAt = task.CreatedAt;
        auxtask[0].UpdatedAt = task.UpdatedAt;


        await _context.SaveChangesAsync();
    }
}
