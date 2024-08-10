using E_commerceApplication.Data;

namespace E_commerceApplication
{
    public class EcommerceDBRepositoryClass
    {
        public AppDbContext Context;

        public EcommerceDBRepositoryClass(AppDbContext context)
        {
            Context = context;
        }
        public List<Food> GetAll()
        {
            return Context.Foods.ToList();
        }

        public Food? GetById(int id)
        {
            Food? food = Context.Foods.FirstOrDefault(f => f.Id == id);
            return food;
        }

        public Food Add(Food food)
        {
            Context.Foods.Add(food);
            Context.SaveChanges();
            return food;
        }

        public Food? Update(int id, Food food)
        {
            var existingFood = Context.Foods.FirstOrDefault(f => f.Id == id);
            if (existingFood != null)
            {
                existingFood.Name = food.Name;
                existingFood.Quantity = food.Quantity;
                existingFood.Price = food.Price;
                Context.SaveChanges();
            }
            return existingFood;
        }

        public Food? Delete(int id)
        {
            var existingFood = Context.Foods.FirstOrDefault(f => f.Id == id);
            if (existingFood != null)
            {
                Context.Foods.Remove(existingFood);
                Context.SaveChanges();
            }
            return existingFood;
        }
    }
}
