using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateUserAsync(AppUser user)
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<MemberDto>> GetAllMembersAsync()
        {
            return await _context.Users
                .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetAllUsersAsync()
        {
            return await _context.Users
             .Include(x => x.Photos)
            .ToListAsync();
        }

        public async Task<MemberDto?> GetMemberDtoByIdAsync(int id)
        {
           return await _context.Users
              .Where(x => x.Id == id)
              .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync();
        }

        public async Task<MemberDto?> GetMemberDtoByUserName(string username)
        {
            return await _context.Users
              .Where(x => x.UserName == username)
              .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
              .SingleOrDefaultAsync();  
        }

        public async Task<AppUser?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<AppUser?> GetUserByUserName(string username)
        {
            return await _context.Users
              .Include(x => x.Photos)
             .SingleOrDefaultAsync(u => u.UserName == username);
        }

        public void UpdateUser(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}
