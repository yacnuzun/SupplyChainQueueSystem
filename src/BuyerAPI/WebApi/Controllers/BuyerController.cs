using BuyerAPI.Domain.Entities;
using BuyerAPI.Dto_s;
using BuyerAPI.Infrastructure.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BuyerAPI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BuyerController : ControllerBase
    {
        private readonly IBuyerHelper _buyerHelper;
        private readonly IBus _publishEndpoint;

        public BuyerController(IBuyerHelper buyerHelper,
            IBus publishEndpoint)
        {
            _buyerHelper = buyerHelper;
            _publishEndpoint = publishEndpoint;
        }

        [Authorize(Roles = "Buyer")]
        [HttpPost("createabill")]
        public async Task<bool> CreateABill(CreateBillDTO dto)
        {
            var userId = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var response = await _buyerHelper.CreateABill(dto, HttpContext.Request.Headers.Authorization.ToString());

            await _publishEndpoint.Publish(response);

            return await Task.FromResult(true);
        }

        [Authorize(Roles = "Buyer")]
        [HttpGet("getbill")]
        public async Task<IActionResult> GetBills()
        {
            var x = ClaimTypes.NameIdentifier;

            //check to user claims
            var userRequest = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userRequest is null)
            {
                return Ok("Kullanýcý Bulunamadý");
            }

            var list = await _buyerHelper.GetBills(userRequest, HttpContext.Request.Headers.Authorization.ToString());

            if (!list.Success)
            {
                return Ok(list.Message);
            }

            return Ok(list.Data);
        }
    }
}
