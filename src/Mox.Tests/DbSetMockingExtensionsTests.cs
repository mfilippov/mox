using System.Collections.Generic;
using System.Linq;
using Shouldly;
using Xunit;

namespace Mox.Tests
{
    public class Entity { }
    
    public class DbSetMockingExtensionsTests
    {
        [Fact]
        public void ShouldMockDbSetDataForIQueryable()
        {
            new List<Entity> { new Entity(), new Entity() }.MockDbSet().Count().ShouldBe(2);
        }
        
        [Fact]
        public void ShouldMockDbSetDataForIEnumerable()
        {
            new List<Entity> { new Entity(), new Entity() }.MockDbSet().ToList().Count.ShouldBe(2);
        }
    }
}
