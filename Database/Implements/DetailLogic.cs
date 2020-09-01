using Database.Models;
using Logic.BindingModel;
using Logic.Interfaces;
using Logic.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Database.Implements
{
    public class DetailLogic : IDetailLogic
    {
        public void CreateOrUpdate(DetailBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Detail element = context.Details.FirstOrDefault(rec => rec.DetailName == model.DetailName && rec.Id != model.Id);

                if (element != null)
                {
                    throw new Exception("Уже есть деталь с таким названием");
                }

                if (model.Id.HasValue)
                {
                    element = context.Details.FirstOrDefault(rec => rec.Id == model.Id);

                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                }
                else
                {
                    element = new Detail();
                    context.Details.Add(element);
                }
                element.DetailName = model.DetailName;
                element.Price = model.Price;
                element.TotalAmount = model.TotalAmount;

                context.SaveChanges();
            }
        }

        public void Delete(DetailBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                Detail element = context.Details.FirstOrDefault(rec => rec.Id == model.Id);

                if (element != null)
                {
                    context.Details.Remove(element);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("Элемент не найден");
                }
            }
        }

        public List<DetailViewModel> Read(DetailBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Details
                .Where(rec => model == null || rec.Id == model.Id)
                .Select(rec => new DetailViewModel
                {
                    Id = rec.Id,
                    DetailName = rec.DetailName,
                    Price = rec.Price,
                    TotalAmount = rec.TotalAmount
                })
                .ToList();
            }
        }
    }
}
