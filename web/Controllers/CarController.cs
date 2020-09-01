using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarLogic _car;
        public CarController(ICarLogic car)
        {
            _car = car;
        }
        public IActionResult Car()
        {
            ViewBag.Cars = _car.Read(null);
            return View();
        }
    }
}
