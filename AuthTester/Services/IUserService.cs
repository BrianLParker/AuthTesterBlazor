using AuthTester.Entities;

namespace AuthTester.Services
{
    public interface IUserService
    {
        Task<User> UserInfo();
        Task<User> UserInfoWorkaround(string token);
    }
}
