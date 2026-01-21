using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SupplierAPI.Domain.Entities;
using SupplierAPI.Infrastructure.Repositories.Interfaces;
using System.Security.Claims;

namespace SupplierAPI.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierHelper _suplierHelper;
        private readonly IBus _bus;

        public SupplierController(ISupplierHelper suplierHelper, IBus bus)
        {
            _suplierHelper = suplierHelper;
            _bus = bus;
        }

        [Authorize(Roles = "Supplier")]
        [HttpPost("paymentrequest")]
        public async Task<bool> EarlypPaymentRequest(string invoice)
        {

            //check to user claims
            var userRequest = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userRequest is null)
            {
                return false;
            }

            var request = await _suplierHelper.CreateAEarlyTask(invoice, HttpContext.Request.Headers.Authorization.ToString());

            if (!request.Success)
            {
                return false;
            }

            _bus?.Publish(request?.Data);

            return true;
        }

        [Authorize(Roles = "Supplier")]
        [HttpGet("listingBills")]
        public async Task<IActionResult> ListingBills()
        {
            
            //check to user claims
            var userRequest = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userRequest))
            {
                return Ok("UserId alaný boþ");
            }

            var request = await _suplierHelper.GetBillswithSupplier(userRequest, HttpContext.Request.Headers.Authorization.ToString());

            if (request == null)
            {
                return Ok(request?.Message);
            }

            return Ok(request.Data);
        }
    }
}
