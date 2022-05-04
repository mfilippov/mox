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
        
        [Fact]
        public void ShouldMockDbSetAddMethod()
        {
            var entities = new List<Entity> { new Entity(), new Entity() };
            var dbSet = entities.MockDbSet();
            dbSet.Add(new Entity());
            entities.Count.ShouldBe(3);
        }
        
        [Fact]
        public void ShouldMockDbSetRemoveMethod()
        {
            var entity1 = new Entity();
            var entity2 = new Entity();
            var entities = new List<Entity> { entity1, entity2 };
            var dbSet = entities.MockDbSet();
            dbSet.Remove(entity2);
            entities.Count.ShouldBe(1);
        }
    }
}
