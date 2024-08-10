using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace E_commerceApplication
{
    public class PaymentService
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentService(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
        }
        public Session CreateCheckoutSession(List<SelectedFood>selectedFoods)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = selectedFoods.Select(selectedFood => new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(selectedFood.Price * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = selectedFood.Name,
                        },
                    },
                    Quantity = selectedFood.Quantity,
                }).ToList(),
               
                Mode = "payment",
                //SuccessUrl = "https://example.com/success",
                SuccessUrl = "https://example.com/cancel",
                CancelUrl = "https://example.com/cancel",
            };
            var service = new SessionService();
            return service.Create(options);
        }
    }
    
}
