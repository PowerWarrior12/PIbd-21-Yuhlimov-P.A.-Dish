using DishProjectBusinessLogic.BusinessLogics;
using DishProjectBusinessLogic.Interfaces;
using DishProjectListImplement;
using DishProjectListImplement.Models;
using System;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;


namespace DishProjectView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IComponentStorage, ComponentStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IOrderStorage, OrderStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IDishStorage, DishStorage>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWareHouseStorage, WareHouseStorage>(new
            HierarchicalLifetimeManager());
            currentContainer.RegisterType<ComponentLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<OrderLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<DishLogic>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<WareHouseLogic>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
