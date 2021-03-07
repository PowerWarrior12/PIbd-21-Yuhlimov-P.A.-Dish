using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DishProjectDatabaseImplement
{
    public class OrderStorage : IOrderStorage
    {
        public void Delete(OrderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public List<OrderViewModel> GetFullList()
        {
            throw new NotImplementedException();
        }

        public void Insert(OrderBindingModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderBindingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
