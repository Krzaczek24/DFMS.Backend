using AutoMapper;
using Core.Database.Services;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Workspace
{
    public interface IWorkspaceService
    {
        Task<int> CreateWorkspace(string creatorLogin, string name, bool isPublic);
        Task<bool> UpdateWorkspace(int id, string updaterLogin, string? name = null, bool? isPublic = null);
        Task<bool> RemoveWorkspace(int id);
    }

    public class WorkspaceService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IWorkspaceService
    {
        public Task<int> CreateWorkspace(string creatorLogin, string name, bool isPublic)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateWorkspace(int id, string updaterLogin, string? name = null, bool? isPublic = null)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> RemoveWorkspace(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
