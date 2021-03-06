﻿using Logic.BindingModel;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Interfaces
{
    public interface IOrderLogic
    {
        List<OrderViewModel> Read(OrderBindingModel model);

        void CreateOrUpdate(OrderBindingModel model);

        void Delete(OrderBindingModel model);
    }
}
