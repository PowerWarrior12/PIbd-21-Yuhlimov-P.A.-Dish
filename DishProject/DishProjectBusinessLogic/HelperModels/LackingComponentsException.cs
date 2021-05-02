using System;
using System.Collections.Generic;
using System.Text;

namespace DishProjectBusinessLogic.HelperModels
{
    public class LackingComponentsException : Exception
    {
        public LackingComponentsException() : base("Не достаточно компонентов на складе")
        {
        }
    }
}
