using OrderManagement.Application.DTO.ApiUsers;

namespace OrderManagement.Application.Repositories
{
    public interface ITokenRepository
    {
        Task<string> GetToken(ApiUserModel model);
    }
}