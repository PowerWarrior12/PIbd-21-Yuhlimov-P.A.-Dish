using System;
using System.Collections.Generic;
using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;

namespace DishProjectListImplement.Models
{
    public class ImplementerStorage : IImplementerStorage
    {
        private readonly DataListSingleton source;

        public ImplementerStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        public void Delete(ImplementerBindingModel model)
        {
            for (int i = 0; i < source.Implementers.Count; ++i)
            {
                if (source.Implementers[i].Id == model.Id.Value)
                {
                    source.Implementers.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var employee in source.Implementers)
            {
                if (employee.Id == model.Id)
                {
                    return CreateModel(employee);
                }
            }
            return null;
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var employee in source.Implementers)
            {
                if (employee.ImplementerFIO.Contains(model.ImplementerFIO))
                {
                    result.Add(CreateModel(employee));
                }
            }
            if (result.Count > 0)
            {
                return result;
            }
            return null;
        }

        public List<ImplementerViewModel> GetFullList()
        {
            List<ImplementerViewModel> result = new List<ImplementerViewModel>();
            foreach (var employee in source.Implementers)
            {
                result.Add(CreateModel(employee));
            }
            return result;
        }

        public void Insert(ImplementerBindingModel model)
        {
            Implementer tempEmployee = new Implementer { Id = 1 };
            foreach (var employee in source.Implementers)
            {
                if (employee.Id >= tempEmployee.Id)
                {
                    tempEmployee.Id = employee.Id + 1;
                }
            }
            source.Implementers.Add(CreateModel(model, tempEmployee));
        }

        public void Update(ImplementerBindingModel model)
        {
            Implementer tempEmployee = null;
            foreach (var employee in source.Implementers)
            {
                if (employee.Id == model.Id)
                {
                    tempEmployee = employee;
                }
            }
            if (tempEmployee == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempEmployee);
        }
        private Implementer CreateModel(ImplementerBindingModel model, Implementer employee)
        {
            employee.ImplementerFIO = model.ImplementerFIO;
            employee.PauseTime = model.PauseTime;
            employee.WorkingTime = model.WorkingTime;
            return employee;
        }

        private ImplementerViewModel CreateModel(Implementer employee)
        {
            return new ImplementerViewModel
            {
                Id = employee.Id,
                ImplementerFIO = employee.ImplementerFIO,
                PauseTime = employee.PauseTime,
                WorkingTime = employee.WorkingTime
            };
        }
    }
}
