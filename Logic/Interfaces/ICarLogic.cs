using Logic.BindingModel;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interfaces
{
    public interface ICarLogic
    {
        List<CarViewModel> Read(CarBindingModel model);

        void CreateOrUpdate(CarBindingModel model);

        void Delete(CarBindingModel model);
    }
}
