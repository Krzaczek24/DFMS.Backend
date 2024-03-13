using AutoMapper;
using Core.Database.Services;
using System.Threading.Tasks;

#nullable enable
namespace DFMS.Database.Services.Workspace
{
    public interface IWorkspaceService
    {
        Task<int> CreateWorkspace(string creatorLogin, string name, bool isPublic);
        Task<bool> UpdateWorkspace(int id, string updaterLogin, string? name = null, bool? isPublic = null, bool? isActive = null);
        Task<bool> RemoveWorkspace(int id);
    }

    public class WorkspaceService(DfmsDbContext database, IMapper mapper)
        : DbService<DfmsDbContext>(database, mapper), IWorkspaceService
    {
        public async Task<int> CreateWorkspace(string creatorLogin, string name, bool isPublic)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateWorkspace(int id, string updaterLogin, string? name = null, bool? isPublic = null, bool? isActive = null)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> RemoveWorkspace(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}
