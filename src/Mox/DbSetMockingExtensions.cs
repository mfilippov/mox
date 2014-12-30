using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace Mox
{
    public static class DbSetMockingExtensions
    {
        public static DbSet<T> MockWithList<T>(this DbSet<T> dbSet, IList<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            Mock.Get(dbSet).As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            Mock.Get(dbSet).As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            Mock.Get(dbSet).As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            Mock.Get(dbSet).As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet;
        }
    }
}
