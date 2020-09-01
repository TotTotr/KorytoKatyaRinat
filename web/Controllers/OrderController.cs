using Logic.BindingModel;
using Logic.BusinessLogic;
using Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using web.Models;
using System.Threading.Tasks;
using Logic.ViewModel;
using Logic.Enums;
using Database;
using Database.Implements;

namespace web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderLogic _orderLogic;
        private readonly ICarLogic _carLogic;
        private readonly IDetailLogic _detailLogic;
        private readonly ReportLogic _reportLogic;

        public OrderController(IOrderLogic orderLogic, ICarLogic carLogic, IDetailLogic detailLogic, ReportLogic reportLogic)
        { 
            _orderLogic = orderLogic;
            _carLogic = carLogic;
            _detailLogic = detailLogic;
            _reportLogic = reportLogic;
        }

        public IActionResult Order()
        {
            ViewBag.Orders = _orderLogic.Read(new OrderBindingModel
            {
                ClientId = Program.Client.Id
            });
            return View();
        }
        [HttpPost]
        public IActionResult Order(ReportModel model)
        {
            var carList = new List<CarViewModel>();
            var orders = _orderLogic.Read(new OrderBindingModel
            {
                ClientId = Program.Client.Id,
                DateFrom = model.From,
                DateTo = model.To
            });
            var cars = _carLogic.Read(null);
            foreach (var order in orders)
            {
                foreach (var car in cars)
                {
                        carList.Add(car);
                }
            }
            ViewBag.Cars = carList;
            ViewBag.Orders = orders;
            string fileName = "pdfreport.pdf";
            if (model.SendMail)
            {
                _reportLogic.SaveCarsDetailsToPdfFile(fileName, Program.Client.Id, Program.Client.Mail);
            }
            return View();
        }

        public IActionResult CreateOrder()
        {
            ViewBag.OrderCars = _carLogic.Read(null);
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(CreateOrderModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.OrderCars = _carLogic.Read(null);
                return View(model);
            }

            if (model.OrderCars == null)
            {
                ViewBag.OrderCars = _carLogic.Read(null);
                ModelState.AddModelError("", "Выберите автомобиль");
                return View(model);
            }
            var orderCars = new List<OrderCarBindingModel>();
            int flag = 0;
            foreach (var car in model.OrderCars)
            {
                if (car.Value > 0)
                {
                    var cart = _carLogic.Read(new CarBindingModel { Id = car.Key }).FirstOrDefault();
                    foreach(var det in cart.CarDetails)
                    {
                        var detcount = _detailLogic.Read(new DetailBindingModel { Id = det.Key }).FirstOrDefault();
                        if((det.Value.Item2 * car.Value) > detcount.TotalAmount)
                        {
                            //вывести пользователю
                            ModelState.AddModelError("", "МАЛО ДЕТАЛЕЙ");
                            flag = 1;
                            int raznica = (det.Value.Item2 * car.Value) - detcount.TotalAmount;
                        }
                        else
                        {
                            _detailLogic.CreateOrUpdate(new DetailBindingModel
                            {
                                Id = detcount.Id,
                                DetailName = detcount.DetailName,
                                Price = detcount.Price,
                                TotalAmount = detcount.TotalAmount - (det.Value.Item2 * car.Value)
                            });
                        }
                    }
                    orderCars.Add(new OrderCarBindingModel
                    {
                        CarId = car.Key,
                        Count = car.Value
                    });
                }
            }
            if (flag == 1)
            {
                _orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    ClientId = Program.Client.Id,
                    DateCreate = DateTime.Now,
                    Status = OrderStatus.Ожидает_поставки_деталей,
                    Price = CalculateSum(orderCars),
                    OrderCars = orderCars
                });
            }
            else
            {
                _orderLogic.CreateOrUpdate(new OrderBindingModel
                {
                    ClientId = Program.Client.Id,
                    DateCreate = DateTime.Now,
                    Status = OrderStatus.Принят,
                    Price = CalculateSum(orderCars),
                    OrderCars = orderCars
                });
            }
            return RedirectToAction("Order");
        }

        private decimal CalculateSum(List<OrderCarBindingModel> orderCars)
        {
            decimal sum = 0;

            foreach (var car in orderCars)
            {
                var carData = _carLogic.Read(new CarBindingModel { Id = car.CarId }).FirstOrDefault();

                if (carData != null)
                {
                    for (int i = 0; i < car.Count; i++)
                        sum += carData.Price;
                }
            }
            return sum;
        }

        public IActionResult SendWordReport(int id)
        {
            var order = _orderLogic.Read(new OrderBindingModel { Id = id }).FirstOrDefault();
            string fileName = "Список автомобилей в заказе" + order.Id + ".docx";
            _reportLogic.SaveOrderToWordFile(fileName, order, Program.Client.Mail);
            return RedirectToAction("Order");
        }

        public IActionResult SendExcelReport(int id)
        {
            var order = _orderLogic.Read(new OrderBindingModel { Id = id }).FirstOrDefault();
            string fileName = "Список автомобилей в заказе" + order.Id + ".xlsx";
            _reportLogic.SaveOrderToExcelFile(fileName, order, Program.Client.Mail);
            return RedirectToAction("Order");
        }
    }
}
