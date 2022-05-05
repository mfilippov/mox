using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;

namespace Mox
{
    public static class DbSetMockingExtensions
    {
        public static DbSet<T> MockDbSet<T>(this IList<T> data, List<string> supportedIncludes = null) where T : class
        {
            var queryable = data.AsQueryable();
            var mockDbSet = new Mock<DbSet<T>>();
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockDbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());
            mockDbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>(data.Add);
            mockDbSet.Setup(d => d.Remove(It.IsAny<T>())).Callback<T>(t => data.Remove(t));
            if (supportedIncludes == null)
            {
                return mockDbSet.Object;
            }

            foreach (var include in supportedIncludes)
            {
                mockDbSet.Setup(d => d.Include(include)).Returns(mockDbSet.Object);
            }
            return mockDbSet.Object;
        }
    }
}
