using BusinessLogic.Interfaces;
using Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;

namespace BusinessLogic.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly string _API_KEY = "sandbox-VhLjQwwqW7VcHiviaOTcQgvHTk0Lg19c";
        private readonly string _SECRET_KEY = "sandbox-3Op2Bsj0R5T7k4I4D6BPakDTGKGTHHYS";
        private readonly ICartService _cartService;
        private readonly IUserService _userService;

        public PaymentService(ICartService cartService, IUserService userService)
        {
            _cartService = cartService;
            _userService = userService;
        }

        public bool Pay()
        {
            Options options = new()
            {
                ApiKey = _API_KEY,
                SecretKey = _SECRET_KEY,
                BaseUrl = "https://sandbox-api.iyzipay.com"
            };
            var cart = _cartService.Get();
            int userId = _cartService.GetUserId();
            var user = _userService.GetById(userId);

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = "123456789";
            request.Price = Convert.ToString(cart.TotalPrice);
            request.PaidPrice = Convert.ToString(cart.TotalPrice + 50);
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = user.Name;
            paymentCard.CardNumber = "5528790000000008";
            paymentCard.ExpireMonth = "12";
            paymentCard.ExpireYear = "2030";
            paymentCard.Cvc = "123";
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = Convert.ToString(user.Id);
            buyer.Name = user.Name;
            buyer.Surname = "";
            buyer.GsmNumber = "+905350000000";
            buyer.Email = user.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = user.Address;
            buyer.Ip = "85.34.78.112";
            buyer.City = user.Address;
            buyer.Country = user.Address;
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = "Jane Doe";
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            shippingAddress.ZipCode = "34742";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = "Jane Doe";
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            billingAddress.ZipCode = "34742";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in cart.CartItems!)
            {
                var bItem = new BasketItem()
                {
                    Id = Convert.ToString(item.Id),
                    Name = item.ProductName,
                    Category1 = "Collectibles",
                    Category2 = "Accessories",
                    ItemType = item.GetType().ToString(),
                    Price = Convert.ToString(item.Price),
                };
                basketItems.Add(bItem);
            }
            //BasketItem firstBasketItem = new BasketItem();
            //firstBasketItem.Id = "BI101";
            //firstBasketItem.Name = "Binocular";
            //firstBasketItem.Category1 = "Collectibles";
            //firstBasketItem.Category2 = "Accessories";
            //firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            //firstBasketItem.Price = "0.3";
            //basketItems.Add(firstBasketItem);

            //BasketItem secondBasketItem = new BasketItem();
            //secondBasketItem.Id = "BI102";
            //secondBasketItem.Name = "Game code";
            //secondBasketItem.Category1 = "Game";
            //secondBasketItem.Category2 = "Online Game Items";
            //secondBasketItem.ItemType = BasketItemType.VIRTUAL.ToString();
            //secondBasketItem.Price = "0.5";
            //basketItems.Add(secondBasketItem);

            //BasketItem thirdBasketItem = new BasketItem();
            //thirdBasketItem.Id = "BI103";
            //thirdBasketItem.Name = "Usb";
            //thirdBasketItem.Category1 = "Electronics";
            //thirdBasketItem.Category2 = "Usb / Cable";
            //thirdBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            //thirdBasketItem.Price = "0.2";
            //basketItems.Add(thirdBasketItem);
            request.BasketItems = basketItems;

            var result = true;

            Payment payment = Payment.Create(request, options);

            if (payment == null)
            {
                result = false;
            }

            return result;
        }
    }
}
