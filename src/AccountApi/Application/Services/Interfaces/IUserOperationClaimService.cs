using AccountApi.Domain.Entities;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;

namespace AccountApi.Application.Services.Interfaces
{
    public interface IUserOperationClaimService
    {
        Task<IResult> AddAsync(UserClaimDto userOperationClaim);
        Task<IResult> AddWithoutCommitAsync(UserOperationClaim userOperationClaim);
    }
}
