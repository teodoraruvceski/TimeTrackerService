﻿using Microsoft.AspNetCore.Mvc;
using TimeTrackerService.Models;
using TimeTrackerService.Services.Interfaces;
using Task = TimeTrackerService.Models.Task;

namespace TimeTrackerService.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : Controller
    {
        ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Task> tasks = await _tasksService.GetAllTasks();
            if(tasks.Count == 0)
            {
                Task task1 = new Task(0, "Task1", "Project1");
                Task task2 = new Task(0, "Task2",  "Project2");
                Task task3 = new Task(0, "Task3", "Project1");
                Task task4 = 
                    new Task(0, 
                            "my task", 
                            new DateTime(2023,9,24,9,40,20),
                            new DateTime(2023, 9, 24, 9, 45, 20),
                            350,
                            "Project1");
                tasks = new List<Models.Task> { task1, task2, task3, task4 };
                tasks.ForEach(x => _tasksService.AddTask(x));
                return Ok(await _tasksService.GetAllTasks());
            }
            return Ok(tasks);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Task>> AddTask(Task task)
        {
            return Ok(await _tasksService.AddTask(task));
        }

        [HttpGet("generate-report")]
        public async Task<ActionResult> GenerateReport()
        {
            await _tasksService.GenerateReport();
            return Ok();
        }
    }
}
