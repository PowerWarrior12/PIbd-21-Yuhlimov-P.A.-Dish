using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.Interfaces
{
    public interface IWareHouseStorage
    {
        List<WareHouseViewModel> GetFullList();
        List<WareHouseViewModel> GetFilteredList(WareHouseBindingModel model);
        WareHouseViewModel GetElement(WareHouseBindingModel model);
        void Insert(WareHouseBindingModel model);
        void Update(WareHouseBindingModel model);
        void Delete(WareHouseBindingModel model);
        void ChangeComponents(ChangeComponentBindingModel model);
    }
}
