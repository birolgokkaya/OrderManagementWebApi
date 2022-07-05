using OrderManagement.Domain.Entities.Base;

namespace OrderManagement.Domain.Entities
{
    public class ApiUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}