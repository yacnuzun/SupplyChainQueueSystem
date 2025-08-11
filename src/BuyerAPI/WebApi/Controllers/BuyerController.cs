using BuyerAPI.Domain.Entities;
using BuyerAPI.Dto_s;
using BuyerAPI.Infrastructure.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BuyerAPI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BuyerController : ControllerBase
    {
        readonly IBuyerHelper _buyerHelper;
        readonly IBus _publishEndpoint;
        readonly string role = nameof(Buyer);

        public BuyerController(IBuyerHelper buyerHelper,
            IBus publishEndpoint)
        {
            _buyerHelper = buyerHelper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("createabill")]
        [Authorize]
        public async Task<bool> CreateABill(CreateBillDTO dto)
        {
            //check to user claims
            var userRequest = await _buyerHelper.CheckUser(HttpContext.Request.Headers.Authorization.ToString());

            if (!userRequest.Success)
            {
                return false;
            }

            var response = await _buyerHelper.CreateABill(dto, HttpContext.Request.Headers.Authorization.ToString());

            await _publishEndpoint.Publish(response);

            return await Task.FromResult(true);
        }

        [HttpGet("getbill")]
        [Authorize]
        public async Task<IActionResult> GetBills()
        {
            //check to user claims
            var userRequest = await _buyerHelper.CheckUser(HttpContext.Request.Headers.Authorization.ToString());

            if (!userRequest.Success)
            {
                return Ok(userRequest.Message);
            }

            var list = await _buyerHelper.GetBills(userRequest.Data.NameIdentifier, HttpContext.Request.Headers.Authorization.ToString());

            if (!list.Success)
            {
                return Ok(list.Message);
            }

            return Ok(list.Data);
        }
    }
}
