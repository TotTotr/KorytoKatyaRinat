using Logic.BindingModel;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interfaces
{
    public interface IRequestLogic
    {
        List<RequestViewModel> Read(RequestBindingModel model);

        void CreateOrUpdate(RequestBindingModel model);

        void Delete(RequestBindingModel model);
    }
}
