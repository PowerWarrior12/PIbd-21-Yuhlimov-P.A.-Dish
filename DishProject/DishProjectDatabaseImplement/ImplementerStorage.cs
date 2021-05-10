using System;
using System.Collections.Generic;
using System.Linq;
using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.Interfaces;
using DishProjectBusinessLogic.ViewModels;
using DishProjectDatabaseImplement;

namespace DishProjectDatabaseImplement
{
    public class ImplementerStorage : IImplementerStorage
    {
        public void Delete(ImplementerBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                Implementer element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element != null)
                {
                    context.Implementers.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Работник не найден");
                }
            }
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                var employee = context.Implementers
                .FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO ||
                rec.Id == model.Id);
                return employee != null ?
                new ImplementerViewModel
                {
                    Id = employee.Id,
                    ImplementerFIO = employee.ImplementerFIO,
                    WorkingTime = employee.WorkingTime,
                    PauseTime = employee.PauseTime,
                } :
                null;
            }
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using (var context = new DishProjectDatabase())
            {
                return context.Implementers
                .Where(rec => rec.ImplementerFIO.Contains(model.ImplementerFIO))
                .Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                })
                .ToList();
            }
        }

        public List<ImplementerViewModel> GetFullList()
        {
            using (var context = new DishProjectDatabase())
            {
                return context.Implementers.Select(rec => new ImplementerViewModel
                {
                    Id = rec.Id,
                    ImplementerFIO = rec.ImplementerFIO,
                    WorkingTime = rec.WorkingTime,
                    PauseTime = rec.PauseTime,
                })
                .ToList();
            }
        }

        public void Insert(ImplementerBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                context.Implementers.Add(CreateModel(model, new Implementer(), context));
                context.SaveChanges();
            }
        }

        public void Update(ImplementerBindingModel model)
        {
            using (var context = new DishProjectDatabase())
            {
                var element = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Работник не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
            }
        }
        private Implementer CreateModel(ImplementerBindingModel model, Implementer employee, DishProjectDatabase database)
        {
            employee.ImplementerFIO = model.ImplementerFIO;
            employee.WorkingTime = model.WorkingTime;
            employee.PauseTime = model.PauseTime;
            return employee;
        }
    }
}
