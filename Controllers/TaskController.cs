using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListBk.DTO;
using ToDoListBk.Services.Interfaces;
using ToDoListBk.Utils;

namespace ToDoListBk.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Post([FromBody] TaskInsertDTO task)
    {
        try
        {

            await _taskService.SaveTask(task);
            return Ok(new { message = "Tarea registrada con Exito!" });

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTareaId(string id)
    {

        try
        {

            var GetTask= await _taskService.GetTaskId(id.FromHashId());
            return Ok(GetTask);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpDelete("{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Delete(string id)
    {
        try
        {
            var GetTask= await _taskService.GetTaskId(id.FromHashId());
            if (GetTask == null)
            {
                return BadRequest(new { message = "No se encontro ningun registro" });
            }
            await _taskService.deleteTask(id.FromHashId());
            return Ok(new { message = "El registro fue eliminado con exito" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPut()]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> UpdateTask([FromBody] TaskDTO task)
    {
        try
        {
            var GetTask = await _taskService.GetTaskId(task.TaskId.FromHashId());
            if (GetTask == null)
            {
                return BadRequest(new { message = "No se encontro ningun registro" });
            }
            await _taskService.UpdateTask(task);
            return Ok(new { message = "El registro fue actualizado con exito!" });
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        try
        {
            var listTask= await _taskService.GetTasks();
            return Ok(listTask);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
