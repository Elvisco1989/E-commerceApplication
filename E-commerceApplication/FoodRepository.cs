namespace E_commerceApplication
{
    public class FoodRepository
    {
        private List<Food> _foods = new List<Food>() {
            new Food { Id = 1, Name = "Achu", Quantity = 1, Price = 35, Image = "img/Food1.png" },
            new Food { Id = 2, Name = "Ndole", Quantity = 1, Price = 189,Image = "img/Food2.png" },
            new Food { Id = 3, Name = "Beans", Quantity = 1, Price = 100,Image = "img/Food3.png" },
            new Food { Id = 4, Name = "Puffpuff", Quantity = 1, Price = 75,Image = "img/Food4.png" },
            new Food { Id = 5, Name = "Noodles", Quantity = 1, Price = 50, Image = "img/Food5.png" }
        };

        public List<Food> GetAll()
        {
            return _foods;
        }

        public Food? GetById(int id)
        {
            Food? food = _foods.FirstOrDefault(f => f.Id == id);
            return food;
        }

        public Food Add(Food food)
        {
            _foods.Add(food);
            return food;
        }
        public Food? Update( int id, Food food)
        {
            var existingFood = _foods.FirstOrDefault(f => f.Id == id);
            if (existingFood != null)
            {
                existingFood.Name = food.Name;
                existingFood.Quantity = food.Quantity;
                existingFood.Price = food.Price;
            }
            return existingFood;
        }
        public Food? Delete(int id)
        {
            var existingFood = _foods.FirstOrDefault(f => f.Id == id);
            if (existingFood != null)
            {
                _foods.Remove(existingFood);
               
            }
            return existingFood;
        }

    }
}
