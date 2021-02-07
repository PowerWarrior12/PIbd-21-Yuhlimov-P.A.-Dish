using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.ViewModels;
using System.Collections.Generic;


namespace DishProjectBusinessLogic.Interfaces
{
    public interface IDishStorage
    {
        List<DishViewModel> GetFullList();
        List<DishViewModel> GetFilteredList(DishBindingModel model);
        DishViewModel GetElement(DishBindingModel model);
        void Insert(DishBindingModel model);
        void Update(DishBindingModel model);
        void Delete(DishBindingModel model);
    }
}
