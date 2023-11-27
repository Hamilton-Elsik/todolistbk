using AutoMapper;
using System.Threading.Tasks;
using ToDoListBk.DTO;
using ToDoListBk.Persistence.Models;
using ToDoListBk.Repositories.Interfaces;
using ToDoListBk.Services.Interfaces;
using ToDoListBk.Utils;

namespace ToDoListBk.Services;

public class TaskService: ITaskService
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    private readonly ICustomerService _customerService;
    public TaskService(ITaskRepository taskRepository, IMapper mapper, ICustomerService customerService)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _customerService = customerService;
    }

    public async Task SaveTask(TaskInsertDTO task)
    {
        var date = DateTime.Now;
        var idUser = _customerService.GetUserId();
        var model = _mapper.Map<TaskModel>(task);
        model.UserId = idUser.FromHashId();
        model.CreatedAt= date;
        await _taskRepository.SaveTask(model);
    }

    public async Task<TaskDTO> GetTaskId(int id)
    {
        var task = await _taskRepository.GetTaskId(id);
        var model = _mapper.Map<TaskDTO>(task);
        return model;
    }

    public async Task deleteTask(int taskId)
    {
        await _taskRepository.deleteTask(taskId);
    }

    public async Task UpdateTask(TaskDTO task)
    {
        var date = DateTime.Now;
        var model = _mapper.Map<TaskModel>(task);
        model.UpdatedAt = date;
        await _taskRepository.UpdateTask(model);
    }
    public async Task<List<TaskDTO>> GetTasks()
    {
        var idUser = _customerService.GetUserId();
        var data = await _taskRepository.GetTasks(idUser.FromHashId());
        var model = _mapper.Map<List<TaskDTO>>(data);
        return model;
    }

}
