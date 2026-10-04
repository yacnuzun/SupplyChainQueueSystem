using AccountApi.Domain.Enums;
using Shared.Abstract;

namespace AccountApi.Dto_s
{
    public class UserClaimDto:IDTO { 
        public int UserId { get; set;}
        public UserRoles Role { get; set;} 
    }
}
