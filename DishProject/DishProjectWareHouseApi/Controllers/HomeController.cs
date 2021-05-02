using DishProjectWareHouseApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using DishProjectBusinessLogic.ViewModels;
using DishProjectBusinessLogic.BindingModels;
using DishProjectDatabaseImplement;

namespace DishProjectWareHouseApi.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public IActionResult Index()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIEmployer.GetRequest<List<WareHouseViewModel>>("api/warehouse/GetWarehouses"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (password != Program.Password)
                {
                    throw new Exception("Неверный пароль");
                }
                Program.Enter = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public void Create(string name, string FIO)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(FIO))
            {
                APIEmployer.PostRequest("api/warehouse/CreateOrUpdateWareHouse", new WareHouseBindingModel
                {
                    FIO = FIO,
                    Name = name,
                    DateCreate = DateTime.Now,
                    StoreComponents = new Dictionary<int, (string, int)>()
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите имя и ответственного");
        }

        [HttpGet]
        public IActionResult Update(int warehouseId)
        {
            var warehouse = APIEmployer.GetRequest<WareHouseViewModel>($"api/warehouse/GetWarehouse?warehouseId={warehouseId}");
            ViewBag.StoreComponents = warehouse.StoreComponents.Values;
            ViewBag.Name = warehouse.Name;
            ViewBag.FIO = warehouse.FIO;
            return View();
        }

        [HttpPost]
        public void Update(int warehouseId, string name, string FIO)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(FIO))
            {
                var warehouse = APIEmployer.GetRequest<WareHouseViewModel>($"api/warehouse/GetWarehouse?warehouseId={warehouseId}");
                if (warehouse == null)
                {
                    return;
                }
                APIEmployer.PostRequest("api/warehouse/CreateOrUpdateWareHouse", new WareHouseBindingModel
                {
                    FIO = FIO,
                    Name = name,
                    DateCreate = DateTime.Now,
                    StoreComponents = warehouse.StoreComponents,
                    Id = warehouse.Id
                });
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.WareHouse = APIEmployer.GetRequest<List<WareHouseViewModel>>("api/warehouse/GetWarehouses");
            return View();
        }

        [HttpPost]
        public void Delete(int warehouseId)
        {
            APIEmployer.PostRequest("api/warehouse/DeleteWarehouse", new WareHouseBindingModel
            {
                Id = warehouseId
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult AddComponent()
        {
            if (Program.Enter == null)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.WareHouse = APIEmployer.GetRequest<List<WareHouseViewModel>>("api/warehouse/GetWarehouses");
            ViewBag.Component = APIEmployer.GetRequest<List<ComponentViewModel>>("api/warehouse/GetComponents");
            return View();
        }

        [HttpPost]
        public void AddComponent(int warehouseId, int componentId, int count)
        {
            APIEmployer.PostRequest("api/warehouse/AddNewComponent", new AddComponentBindingModel
            {
                WareHouseId = warehouseId,
                ComponentId = componentId,
                Count = count
            });
            Response.Redirect("Index");
        }
    }
}
