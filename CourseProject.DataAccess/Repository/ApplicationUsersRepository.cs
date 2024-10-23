using CourseProject.Domain.Abstractions;
using CourseProject.Domain.Models;

namespace CourseProject.DataAccess.Repository;

public class ApplicationUsersRepository : IEntitiesRepository<ApplicationUser>
{
    public async Task<List<ApplicationUser>> Get()
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Create(ApplicationUser formTemplate)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Update(ApplicationUser formTemplate)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> Delete(Guid id)
    {
        throw new NotImplementedException();
    }
}