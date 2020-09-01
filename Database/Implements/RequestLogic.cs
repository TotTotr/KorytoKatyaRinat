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
    public class RequestLogic : IRequestLogic
    {
        public void CreateOrUpdate(RequestBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        Request element = context.Requests.FirstOrDefault(rec => rec.RequestName == model.RequestName && rec.Id != model.Id);
                        if (element != null)
                        {
                            throw new Exception("Уже есть заявка с таким названием");
                        }
                        if (model.Id.HasValue)
                        {
                            element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                            if (element == null)
                            {
                                throw new Exception("Элемент не найден");
                            }
                        }
                        else
                        {
                            element = new Request();
                            context.Requests.Add(element);
                        }
                        element.RequestName = model.RequestName;
                        element.DateCreate = model.DateCreate;

                        context.SaveChanges();

                        if (model.Id.HasValue)
                        {
                            var DetailRequests = context.DetailRequests.Where(rec
                           => rec.RequestId == model.Id.Value).ToList();
                            context.DetailRequests.RemoveRange(DetailRequests.Where(rec =>
                            !model.DetailRequests.ContainsKey(rec.DetailId)).ToList());
                            context.SaveChanges();
                            foreach (var updateDetail in DetailRequests)
                            {
                                updateDetail.Count = model.DetailRequests[updateDetail.DetailId].Item2;

                                model.DetailRequests.Remove(updateDetail.DetailId);
                            }
                            context.SaveChanges();
                        }
                        foreach (var pc in model.DetailRequests)
                        {
                            context.DetailRequests.Add(new DetailRequest
                            {
                                RequestId = element.Id,
                                DetailId = pc.Key,
                                Count = pc.Value.Item2
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

        public void Delete(RequestBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.DetailRequests.RemoveRange(context.DetailRequests.Where(rec => rec.RequestId == model.Id));
                        Request element = context.Requests.FirstOrDefault(rec => rec.Id == model.Id);
                        if (element != null)
                        {
                            context.Requests.Remove(element);
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

        public List<RequestViewModel> Read(RequestBindingModel model)
        {
            using (var context = new KorytoDatabase())
            {
                return context.Requests
                 .Where(rec => model == null || rec.Id == model.Id
                  || (model.DateFrom.HasValue && model.DateTo.HasValue && rec.DateCreate >= model.DateFrom && rec.DateCreate <= model.DateTo))
                 .ToList()
                 .Select(rec => new RequestViewModel
                 {
                     Id = rec.Id,
                     RequestName = rec.RequestName,
                     DateCreate = rec.DateCreate,
                     DetailRequests = context.DetailRequests
                        .Include(recPC => recPC.Detail)
                        .Where(recPC => recPC.RequestId == rec.Id)
                        .ToDictionary(recPC => recPC.DetailId, recPC =>
                             (recPC.Detail?.DetailName, recPC.Count))
                 })
                 .ToList();
            }
        }
    }
}
