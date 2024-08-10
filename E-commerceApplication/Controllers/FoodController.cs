using E_commerceApplication.Data;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_commerceApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        // GET: api/<FoodController>
        //private readonly FoodRepository _foodRepository;
        private readonly EcommerceDBRepositoryClass _foodRepository;

        private readonly PaymentService _paymentService;
        public FoodController(EcommerceDBRepositoryClass foodRepository, PaymentService paymentService)
        {
            _foodRepository = foodRepository;
            _paymentService = paymentService;
        }
        [HttpGet()]
        public ActionResult<IEnumerable<Food>> Get()
        {
            IEnumerable<Food> foods = _foodRepository.GetAll();
            if (foods == null || !foods.Any())  // Check if no foods are found
            {
                return NotFound();  // Return 404 Not Found response
            }
            return Ok(foods);  // Return 200 OK response with the foods
        }

        // GET api/<FoodController>/5
        [HttpGet("{id}")]
        public Food? Get(int id)
        {
            Food? food = _foodRepository.GetById(id);
            return food;
        }

        // POST api/<FoodController>
        [HttpPost]
        public Food? Post([FromBody] Food value)
        {
            Food food = _foodRepository.Add(value);
            return food;
        }

        // PUT api/<FoodController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Food? value)
        {
            _foodRepository.Update(id, value);
        }

        // DELETE api/<FoodController>/5
        [HttpDelete("{id}")]
        public Food? Delete(int id)
        {
            Food? food = _foodRepository.Delete(id);
            return food;
        }
        [HttpPost("checkout")]
        public ActionResult<Session> Checkout([FromBody] List<SelectedFood> selectedFoods)
        {
            Session session = _paymentService.CreateCheckoutSession(selectedFoods);
            //return session;
            return Ok(new { sessionId = session.Id, sessionUrl = session.Url });
        }

        [HttpPost("create-stripe-session")]
        public IActionResult CreateStripeSession([FromBody] CreateStripeSessionRequest request)
        {
            try
            {
                var session = _paymentService.CreateCheckoutSession(request.Items);
                return Ok(new { sessionId = session.Id, sessionUrl = session.Url });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    

        
    }

    public class CreateStripeSessionRequest
    {
        public List<SelectedFood> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
