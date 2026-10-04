using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClaimController : ControllerBase
    {
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly IValidator<ClaimDto> _validator;
        private readonly IValidator<UserClaimDto> _validatorUserClaim;
        public ClaimController(IOperationClaimService operationClaimService,
            IValidator<ClaimDto> validator,
            IValidator<UserClaimDto> validatorUserClaim,
            IUserOperationClaimService userOperationClaimService)
        {
            _operationClaimService = operationClaimService;
            _validator = validator;
            _validatorUserClaim = validatorUserClaim;
            _userOperationClaimService = userOperationClaimService;
        }

        [Authorize(Roles = UserRolesConst.Admin)]
        [HttpPost("addoperationclaim")]
        public async Task<IActionResult> AddOperationClaim(ClaimDto operationClaim)
        {
            var isValidate = await _validator.ValidateAsync(operationClaim);
            if (!isValidate.IsValid)
            {
                return BadRequest(isValidate.Errors);
            }
            
            var result = await _operationClaimService.Add(operationClaim);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }

        [Authorize(Roles = UserRolesConst.Admin)]
        [HttpPost("adduserclaim")]
        public async Task<IActionResult> AddUserClaim(UserClaimDto operationClaim)
        {
            var isValidate = await _validatorUserClaim.ValidateAsync(operationClaim);
            if (!isValidate.IsValid)
            {
                return BadRequest(isValidate.Errors);
            }

            var result = await _userOperationClaimService.AddAsync(operationClaim);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result);
        }
    }
}
