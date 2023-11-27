using ToDoListBk.DTO;

namespace ToDoListBk.Services.Interfaces;

public interface ITaskService
{
    Task SaveTask(TaskInsertDTO task);
    Task<TaskDTO> GetTaskId(int id);
    Task deleteTask(int taskId);
    Task UpdateTask(TaskDTO task);
    Task<List<TaskDTO>> GetTasks();
}
