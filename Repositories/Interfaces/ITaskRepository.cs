using ToDoListBk.Persistence.Models;

namespace ToDoListBk.Repositories.Interfaces;

public interface ITaskRepository
{
    Task SaveTask(TaskModel task);
    Task<TaskModel>GetTaskId(int id);
    Task deleteTask(int taskId);
    Task UpdateTask(TaskModel task);
    Task<List<TaskModel>> GetTasks(int userId);
}
