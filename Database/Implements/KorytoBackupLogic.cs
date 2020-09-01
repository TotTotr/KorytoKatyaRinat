using Logic.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Database.Implements
{
    public class KorytoBackupLogic : KorytoBackupBusinessLogic
    {
        protected override Assembly GetAssembly()
        {
            return typeof(KorytoBackupLogic).Assembly;
        }
        protected override List<PropertyInfo> GetFullList()
        {
            using (var context = new KorytoDatabase())
            {
                Type type = context.GetType();
                return type.GetProperties().Where(x =>
                x.PropertyType.FullName.StartsWith("Microsoft.EntityFrameworkCore.DbSet")).ToList();
            }
        }
        protected override List<T> GetList<T>()
        {
            using (var context = new KorytoDatabase())
            {
                return context.Set<T>().ToList();
            }
        }
    }
}
