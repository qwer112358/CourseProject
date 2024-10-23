using CourseProjectItems.Models;
using System.Threading.Tasks;

namespace CourseProjectItems.Interfaces
{
	public interface ILikesRepository : IGenericRepository<Like>
	{
		Task<bool> IsLikedByUser(int itemId, string userId);
	}
}
