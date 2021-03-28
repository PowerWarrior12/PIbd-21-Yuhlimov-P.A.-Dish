using Microsoft.EntityFrameworkCore;


namespace DishProjectDatabaseImplement
{
    public class DishProjectDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=POWERWARRIOR-ПК\SQLEXPRESS01;Initial Catalog=DishProjectDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Component> Components { set; get; }
        public virtual DbSet<Dish> Dishes { set; get; }
        public virtual DbSet<DishComponent> DishComponents { set; get; }
        public virtual DbSet<Order> Orders { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
    }
}
