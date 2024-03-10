using Microsoft.AspNetCore.Mvc;
using TimeTracker.Shared.Models.Project;


namespace TimeTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _timeEntryService;
        public ProjectController(IProjectService timeEntryService) 
        {
            _timeEntryService = timeEntryService;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<ProjectResponse>>> GetAllProjects()
        {
           return Ok(await _timeEntryService.GetAllProjects());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponse>> GetAllProjectById(int id)
        {
            var result = await _timeEntryService.GetProjectById(id);
            if(result == null)
            {
                return NotFound("Time entry with given ID not found!");
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<ProjectResponse>>>CreateProject(ProjectCreateRequest timeEntry)
        {
            return Ok(await _timeEntryService.CreateProject(timeEntry));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<ProjectResponse>>>UpdateProject(int id, ProjectUpdateRequest timeEntry)
        {
            var result = await _timeEntryService.UpdateProject(id, timeEntry);
            if(result is null)
            {
                return NotFound("Project with given ID was not found.");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<ProjectResponse>>> DeleteProject(int id) 
        {
            var result = await _timeEntryService.DeleteProject(id);
            if (result is null)
            {
                return NotFound("Project with given ID was not found.");
            }
            return Ok(result);
        }

    }
}
