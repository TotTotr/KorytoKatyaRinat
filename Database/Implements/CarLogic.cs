using Database.Models;
using Logic.BindingModel;
using Logic.Interfaces;
using Logic.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Implements
{
    public class CarLogic : ICarLogic
    {
        public void CreateOrUpdate(CarBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Car element = context.Cars.FirstOrDefault(rec => rec.CarName == model.CarName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть автомобиль с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Car();
                            context.Cars.Add(element);
                        }
                        element.CarName = model.CarName;
                        element.Price = model.Price;
                        element.FullPrice = model.FullPrice;
                        element.Year = model.Year;

                        context.SaveChanges();

                        if (model.Id.HasValue)
                        {
                            var CarDetails = context.CarDetails.Where(rec
                           => rec.CarId == model.Id.Value).ToList();
                            context.CarDetails.RemoveRange(CarDetails.Where(rec =>
                            !model.CarDetails.ContainsKey(rec.DetailId)).ToList());
                            context.SaveChanges();
                            foreach (var updateDetail in CarDetails)
                            {
                                updateDetail.Count =
                               model.CarDetails[updateDetail.DetailId].Item2;

                                model.CarDetails.Remove(updateDetail.DetailId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.CarDetails)
                        {
                            context.CarDetails.Add(new CarDetail
                            {
                                CarId = element.Id,
                                DetailId = pc.Key,
                                Count = pc.Value.Item2,
                                Sum = pc.Value.Item3
                            });
                            context.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Delete(CarBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.CarDetails.RemoveRange(context.CarDetails.Where(rec =>
                        rec.CarId == model.Id));
                        Car element = context.Cars.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Cars.Remove(element);
                            context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Элемент не найден");
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public List<CarViewModel> Read(CarBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Cars
                .Where(rec => model == null || rec.Id == model.Id)
                .ToList()
                .Select(rec => new CarViewModel
                {
                    Id = rec.Id,
                    CarName = rec.CarName,
                    Price = rec.Price,
                    FullPrice = rec.FullPrice,
                    Year = rec.Year,
                    CarDetails = context.CarDetails
                        .Include(recPC => recPC.Detail)
                        .Where(recPC => recPC.CarId == rec.Id)
                        .ToDictionary(recPC => recPC.DetailId, recPC =>
                             (recPC.Detail?.DetailName, recPC.Count, recPC.Sum))
                })
                .ToList();
            }
        }
    }
}
