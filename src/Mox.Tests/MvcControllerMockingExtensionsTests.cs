using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;
using Xunit;
using Xunit.Should;

namespace Mox.Tests
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Film", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

    public class DemoController : Controller
    {
        public string GetTestString()
        {
            return "test";
        }

        public string GetActionUrl()
        {
            Debug.Assert(Request.Url != null, "Request.Url != null");
            return Url.Action("GetActionUrl", "Demo", null, Request.Url.Scheme);
        }
    }

    public class MvcControllerMockingExtensionsTests
    {
        [Fact]
        public void ShouldInvokeSimpleMethodWithContext()
        {
            new DemoController().SetGetContextWithUrl("http://test.ru/test", RouteConfig.RegisterRoutes).GetTestString().ShouldBe("test");
        }

        [Fact]
        public void ShouldReturnCorrectActionUrlWithContext()
        {
            new DemoController().SetGetContextWithUrl("http://test.ru/test", RouteConfig.RegisterRoutes).GetActionUrl().ShouldBe("http://test.ru/Demo/GetActionUrl");
        }
    }
}