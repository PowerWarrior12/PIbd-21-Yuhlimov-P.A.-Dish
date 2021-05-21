using DishProjectBusinessLogic.BindingModels;
using DishProjectBusinessLogic.ViewModels;
using System.Collections.Generic;

namespace DishProjectBusinessLogic.Interfaces
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();
        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);
        void Insert(MessageInfoBindingModel model);
        int Count(MessageInfoBindingModel model);
        List<MessageInfoViewModel> GetMessagesForPage(MessageInfoBindingModel model);
    }
}
