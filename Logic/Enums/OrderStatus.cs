﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Logic.Enums
{
    public enum OrderStatus
    {
        Принят = 0,

        Выполняется = 1,

        Готов = 2,

        Оплачен = 3,

        Ожидает_поставки_деталей = 4
    }
}
