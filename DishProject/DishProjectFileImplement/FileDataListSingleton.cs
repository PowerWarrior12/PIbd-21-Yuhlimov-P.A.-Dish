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
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string DishFileName = "Dish.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string WareHouseFileName = "WareHouse.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Dish> Dishes { get; set; }
        public List<WareHouse> WareHouses { get; set; }
        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Dishes = LoadDishes();
            Clients = LoadClients();
            WareHouses = LoadDWareHouses();
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
            SaveWareHouses();
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
                var xElements = xDocument.Root.Elements("Dish").ToList();
                foreach (var elem in xElements)
                {
                    var dishComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("DishComponents").Elements("DishComponent").ToList())
                    {
                        dishComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Dish
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        DishName = elem.Element("DishName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        DishComponents = dishComp
                    });
                }
            }
            return list;
        }
        private List<WareHouse> LoadDWareHouses()
        {
            var list = new List<WareHouse>();
            if (File.Exists(WareHouseFileName))
            {
                XDocument xDocument = XDocument.Load(WareHouseFileName);
                var xElements = xDocument.Root.Elements("WareHouse").ToList();
                foreach (var elem in xElements)
                {
                    var warehouseComp = new Dictionary<int, int>();
                    foreach (var component in
                   elem.Element("StoreComponents").Elements("StoreComponent").ToList())
                    {
                        warehouseComp.Add(Convert.ToInt32(component.Element("Key").Value),
                       Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new WareHouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        Name = elem.Element("Name").Value,
                        FIO = elem.Element("FIO").Value,
                        StoreComponents = warehouseComp
                    });
                    if (elem.Element("DateCreate").Value != "")
                        list.Last().DateCreate = DateTime.ParseExact(elem.Element("DateCreate").Value, "d.M.yyyy H:m:s", null);
                }
            }
            return list;
        }
        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                XDocument xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Email = elem.Element("Email").Value,
                        Password = elem.Element("Password").Value,
                    });
                }
            }
            return list;
        }
        private void SaveWareHouses()
        {
            if (WareHouses != null)
            {
                var xElement = new XElement("WareHouses");
                foreach (var wareHouse in WareHouses)
                {
                    var compElement = new XElement("StoreComponents");
                    foreach (var component in wareHouse.StoreComponents)
                    {
                        compElement.Add(new XElement("StoreComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("WareHouse",
                     new XAttribute("Id", wareHouse.Id),
                     new XElement("Name", wareHouse.Name),
                     new XElement("FIO", wareHouse.FIO),
                     new XElement("DateCreate", wareHouse.DateCreate.ToString()),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(WareHouseFileName);
            }
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
                var xElement = new XElement("Dishes");
                foreach (var dish in Dishes)
                {
                    var compElement = new XElement("DishComponents");
                    foreach (var component in dish.DishComponents)
                    {
                        compElement.Add(new XElement("DishComponent",
                        new XElement("Key", component.Key),
                        new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Dish",
                     new XAttribute("Id", dish.Id),
                     new XElement("DishName", dish.DishName),
                     new XElement("Price", dish.Price),
                     compElement));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(DishFileName);
            }
        }
        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                    new XAttribute("Id", client.Id),
                    new XElement("ClientFIO", client.ClientFIO),
                    new XElement("Email", client.Email),
                    new XElement("Password", client.Password)
                    ));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }
    }
}
