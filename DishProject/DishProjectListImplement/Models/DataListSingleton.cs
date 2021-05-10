using DishProjectBusinessLogic.ViewModels;
using DishProjectListImplement.Models;
using System.Collections.Generic;


namespace DishProjectListImplement
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Dish> Dishes { get; set; }
        public List<Client> Clients { get; set; }
        public List<WareHouse> WareHouses { get; set; }
        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Dishes = new List<Dish>();
            Clients = new List<Client>();
            WareHouses = new List<WareHouse>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }

    }
}
