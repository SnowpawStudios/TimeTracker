



using Microsoft.EntityFrameworkCore;

namespace TimeTracker.API.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _context;

        public ProjectRepository(DataContext context)
        {
            _context = context;
        }

        
        public async Task<List<Project>> CreateProject(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>?> DeleteProject(int id)
        {
            var dbProject = await _context.Projects.FindAsync(id);
            if(dbProject == null)
            {
                return null;
            }

            dbProject.IsDeleted = true;
            dbProject.DateDeleted = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }

        public async Task<List<Project>> GetAllProjects()
        {
            return await _context.Projects.Where(p => !p.IsDeleted).ToListAsync();
        }

       

        public async Task<Project?> GetProjectById(int id)
        {
            var project = await _context.Projects.Where(p=> !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id);

            return project;
        }

        public async Task<List<Project>> UpdateProject(int id, Project project)
        {
            var dbProject = await _context.Projects.FindAsync(id);
            if(dbProject == null)
            {
                throw new EntityNotFoundException($"Entity with ID {id} was not found");
            }
        
            dbProject.Description = project.Description;
            dbProject.Name = project.Name;
            dbProject.StartDate = project.StartDate;
            dbProject.EndDate = project.EndDate;
            
            dbProject.DateUpdated = DateTime.Now;

            await _context.SaveChangesAsync();

            return await GetAllProjects();
        }

       
    }
}
