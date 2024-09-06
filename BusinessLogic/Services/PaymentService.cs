using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Interfaces;
using Common.DTOs;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;

namespace BusinessLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _API_KEY = "sandbox-VhLjQwwqW7VcHiviaOTcQgvHTk0Lg19c";
        private readonly string _SECRET_KEY = "sandbox-3Op2Bsj0R5T7k4I4D6BPakDTGKGTHHYS";
        private readonly ICartService _cartService;
        private readonly IUserService _userService;
        private readonly HttpClient _httpClient;

        public PaymentService(ICartService cartService, IUserService userService, HttpClient httpClient)
        {
            _cartService = cartService;
            _userService = userService;
            _httpClient = httpClient;

        }      

        public async Task<bool> IsPayCompleted(CreditCardDTO cardInfo)
        {
            var cart = _cartService.Get();
            int userId = _cartService.GetUserId();
            var user = _userService.GetById(userId);

            // Create a request object
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(10000).ToString();
            request.Price = cart.TotalPrice.ToString();
            request.PaidPrice = cart.TotalPrice.ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B" + new Random().Next(10000).ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            //payment card for request object
            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = cardInfo.CardHolderName;
            paymentCard.CardNumber = cardInfo.CardNumber;
            paymentCard.ExpireMonth = cardInfo.ExpireMonth;
            paymentCard.ExpireYear = cardInfo.ExpireYear;
            paymentCard.Cvc = cardInfo.Cvc;
            paymentCard.RegisterCard = cardInfo.RegisterCard;
            request.PaymentCard = paymentCard;

            //buyer for request object
            Buyer buyer = new Buyer();
            buyer.Id = user.Id.ToString();
            buyer.Name = user.Name;
            buyer.Surname = "";
            buyer.GsmNumber = "";
            buyer.Email = user.Email;
            buyer.IdentityNumber = user.Id.ToString();
            buyer.LastLoginDate = DateTime.UtcNow.ToString();
            buyer.RegistrationDate = DateTime.UtcNow.ToString();
            buyer.RegistrationAddress = user.Address;
            buyer.Ip = new Random().Next(100000).ToString();
            buyer.City = user.Address;
            buyer.Country = user.Address;
            buyer.ZipCode = new Random().Next(10000).ToString();
            request.Buyer = buyer;

            //shipping address
            Address shippingAddress = new Address();
            shippingAddress.ContactName = user.Name;
            shippingAddress.City = user.Address;
            shippingAddress.Country = user.Address;
            shippingAddress.Description = user.Address;
            shippingAddress.ZipCode = new Random().Next(10000).ToString();
            request.ShippingAddress = shippingAddress;

            //billing address
            Address billingAddress = new Address();
            billingAddress.ContactName = user.Name;
            billingAddress.City = user.Address;
            billingAddress.Country = user.Address;
            billingAddress.Description = user.Address;
            billingAddress.ZipCode = new Random().Next(10000).ToString();
            request.BillingAddress = billingAddress;

            //basket items
            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var cartItem in cart.CartItems!)
            {
                var basketItem = new BasketItem()
                {
                    Id = cartItem.Id.ToString(),
                    Name = cartItem.ProductName,
                    Category1 = "product",
                    Category2 = "",
                    ItemType = BasketItemType.PHYSICAL.ToString(),
                    Price = cartItem.Price.ToString()
                };
                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems;   

            var response = await _httpClient.PostAsJsonAsync("http://192.168.2.5/payments/pay", request);

            if (response.IsSuccessStatusCode)
            {
                var payment = await response.Content.ReadFromJsonAsync<Payment>();
                if (payment!.Status == "success")
                {
                    return true;
                }
            }

            return false;
        }
    
    }
}
