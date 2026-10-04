using Shared.Helpers.ResponseModels.GenericResultModels;
using IResult = Shared.Helpers.ResponseModels.GenericResultModels.IResult;
using Shared.Persistance.Interfaces;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Repositories.Interfaces;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;

namespace AccountApi.Application.Services.Implementations
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userClaimRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserOperationClaim> _logger;

        public UserOperationClaimManager(IUserOperationClaimRepository userClaimRepository, IUnitOfWork unitOfWork, ILogger<UserOperationClaim> logger)
        {
            _userClaimRepository = userClaimRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IResult> AddAsync(UserClaimDto userOperationClaim)
        {
            try
            {
                var userOperationClaimEntity = new UserOperationClaim { OperationClaimId = ((int)userOperationClaim.Role), UserId = userOperationClaim.UserId };
                await _userClaimRepository.AddAsync(userOperationClaimEntity);
                var result = await _unitOfWork.CommitAsync();
                if (result < 0)
                {
                    return new ErrorResult();
                }
                return new SuccesResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                throw;
            }

        }

        public async Task<IResult> AddWithoutCommitAsync(UserOperationClaim userOperationClaim)
        {
            try
            {
                await _userClaimRepository.AddAsync(userOperationClaim);

                return new SuccesResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
                throw;
            }

        }

    }
}
