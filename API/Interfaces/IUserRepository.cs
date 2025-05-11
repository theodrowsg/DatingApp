using API.DTOs;
using API.Entities;

namespace API.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser?> GetUserByIdAsync(int id);
        Task<AppUser?> GetUserByUserName(string username);
        Task<MemberDto?> GetMemberDtoByIdAsync(int id);
        Task<MemberDto?> GetMemberDtoByUserName(string username);
        Task<IEnumerable<MemberDto>> GetAllMembersAsync();
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(AppUser user);
        void UpdateUser(AppUser user);

    }
}