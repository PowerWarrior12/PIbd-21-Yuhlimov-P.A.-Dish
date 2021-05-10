using Microsoft.AspNetCore.Mvc;
using DishProjectBusinessLogic.BusinessLogics;
using DishProjectBusinessLogic.ViewModels;
using DishProjectBusinessLogic.BindingModels;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WareHouseController : ControllerBase
    {
        private readonly WareHouseLogic _wareHouse;
        private readonly ComponentLogic _component;
        public WareHouseController(WareHouseLogic wareHouse, ComponentLogic component)
        {
            _wareHouse = wareHouse;
            _component = component;
        }
        [HttpGet]
        public List<WareHouseViewModel> GetWareHouses() => _wareHouse.Read(null)?.ToList();
        [HttpGet]
        public WareHouseViewModel GetWareHouse(int wareHouseId) => _wareHouse.Read(new
       WareHouseBindingModel
        { Id = wareHouseId })?[0];
        [HttpPost]
        public void CreateOrUpdateWareHouse(WareHouseBindingModel model) => _wareHouse.CreateOrUpdate(model);
        [HttpPost]
        public void DeleteWarehouse(WareHouseBindingModel model) => _wareHouse.Delete(model);
        [HttpPost]
        public void AddNewComponent(AddComponentBindingModel model) => _wareHouse.AddNewComponent(model);
        [HttpGet]
        public List<ComponentViewModel> GetComponents() => _component.Read(null);

    }
}
