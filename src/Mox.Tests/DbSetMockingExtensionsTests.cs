using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Xunit;
using Xunit.Should;

namespace Mox.Tests
{
    public class Entity { }
    
    public class DbSetMockingExtensionsTests
    {
        [Fact]
        public void ShouldMockDbSetData()
        {
            Mock.Of<DbSet<Entity>>().MockWithList(new List<Entity> { new Entity(), new Entity() }).ToList().Count.ShouldBe(2);
        }
    }
}
