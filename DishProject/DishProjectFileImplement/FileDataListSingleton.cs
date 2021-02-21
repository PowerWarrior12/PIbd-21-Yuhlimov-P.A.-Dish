using DishProjectBusinessLogic.Enums;
using DishProjectFileImplement;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace DishProjectFileImplement
{
    class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "F:\\Users\\PowerWarrior\\Desktop\\Component.xml";
        private readonly string OrderFileName = "F:\\Users\\PowerWarrior\\Desktop\\Order.xml";
        private readonly string DishFileName = "F:\\Users\\PowerWarrior\\Desktop\\Product.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Dish> Dishes { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Dishes = LoadDishes();
        }
        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }
        ~FileDataListSingleton()
        {
            SaveComponents();
            SaveOrders();
            SaveDishes();
        }
        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                XDocument xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }
        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                XDocument xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishId = Convert.ToInt32(elem.Element("DishId").Value),
                        DishName = elem.Element("DishName").Value,
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                    });
                    if (elem.Element("DateCreate").Value != "")
                        list.Last().DateCreate = DateTime.ParseExact(elem.Element("DateCreate").Value, "d.M.yyyy H:m:s", null);
                    if (elem.Element("DateImplement").Value != "")
                        list.Last().DateImplement = DateTime.ParseExact(elem.Element("DateImplement").Value, "d.M.yyyy H:m:s", null);
                }
            }
            return list;
        }
        private List<Dish> LoadDishes()
        {
            var list = new List<Dish>();
            if (File.Exists(DishFileName))
            {
                XDocument xDocument = XDocument.Load(DishFileName);
                var xElements = xDocument.Root.Elements("Product").ToList();
                foreach (var elem in xElements)
                {
                    var prodComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("ProductComponents").Elements("ProductComponent").ToList())
                    {
                        prodComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Dish
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishName = elem.Element("ProductName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        DishComponents = prodComp
                    });
                }
            }
            return list;
        }
        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                    new XAttribute("Id", component.Id),
                    new XElement("ComponentName", component.ComponentName)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }
        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var component in Orders)
                {
                    xElement.Add(new XElement("Order",
                    new XAttribute("Id", component.Id),
                    new XElement("DishId", component.DishId),
                    new XElement("DishName", component.DishName),
                    new XElement("Count", component.Count),
                    new XElement("Sum", component.Sum),
                    new XElement("Status", component.Status),
                    new XElement("DateCreate", component.DateCreate.ToString()),
                    new XElement("DateImplement", component.DateImplement.ToString())));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
        private void SaveDishes()
        {
            if (Dishes != null)
            {
                var xElement = new XElement("Products");
                foreach (var dish in Dishes)
                {
                    var compElement = new XElement("ProductComponents");
                    foreach (var component in dish.DishComponents)
                    {
                        compElement.Add(new XElement("ProductComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Product",
                     new XAttribute("Id", dish.Id),
                     new XElement("ProductName", dish.DishName),
                     new XElement("Price", dish.Price),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(DishFileName);
            }
        }
    }
}
