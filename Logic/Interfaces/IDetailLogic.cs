using Logic.BindingModel;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interfaces
{
    public interface IDetailLogic
    {
        List<DetailViewModel> Read(DetailBindingModel model);

        void CreateOrUpdate(DetailBindingModel model);

        void Delete(DetailBindingModel model);
    }
}
