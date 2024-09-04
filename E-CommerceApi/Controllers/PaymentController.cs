using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using Iyzipay;
using BusinessLogic.Interfaces;
using BusinessLogic.DTOs;

namespace E_CommerceApi.Controllers
{
    [Route("[Controller]s")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("pay")]
        public IActionResult Pay()
        {
            var result = _paymentService.Pay();
            if (result == false)
                throw new ArgumentException("Payment failed!");

            return Ok(result);
        }
    }
}
