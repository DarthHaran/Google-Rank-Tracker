using GRT.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GRT.Interfaces
{
    public interface IProjectRepository
    {
        Task<IEnumerable<Project>> GetProjectsAsync();
        Task<Project> GetProjectAsync(int id);
        Task AddProject(Project project);
        Task UpdateProject(Project project);
        Task DeleteProject(Project project);
    }
}
