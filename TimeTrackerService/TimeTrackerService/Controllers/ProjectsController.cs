using Microsoft.AspNetCore.Mvc;
using TimeTrackerService.Models;
using TimeTrackerService.Services.Interfaces;

namespace TimeTrackerService.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : Controller
    {
        IProjectsService _projectsService;

        public ProjectsController(IProjectsService tasksService)
        {
            _projectsService = tasksService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<Project> projects = await _projectsService.GetAllProjects();
            if (projects.Count == 0)
            {
                Project Project = new Project("Project1");

                projects = new List<Project> {Project };
                projects.ForEach(x => _projectsService.AddTask(x));
                return Ok(await _projectsService.GetAllProjects());
            }
            return Ok(projects);
        }

        [HttpPost("add")]
        public async Task<ActionResult<Project>> AddProject(Project project)
        {
            return Ok(await _projectsService.AddTask(project));
        }
    }
}
