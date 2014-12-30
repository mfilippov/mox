using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data.Entity.Core.Metadata.Edm;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
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
            return Url.Action("GetActionUrl", "Demo", null, Request.Url.Scheme);
        }
    }

    public static class MvcControllerMockingExtensions
    {
        public static T SetGetContextWithUrl<T>(this T controller, string url, Action<RouteCollection> setupRoutes) where T : Controller
        {
            var httpContext = Mock.Of<HttpContextBase>();
            Mock.Get(httpContext).Setup(o => o.Request.Url).Returns(new Uri(url));
            Mock.Get(httpContext).Setup(o => o.Response.ApplyAppPathModifier(It.IsAny<string>())).Returns<string>(r => r);

            controller.ControllerContext = new ControllerContext(httpContext, new RouteData(), controller);
            
            var routes = new RouteCollection();
            setupRoutes(routes);
            
            controller.Url = new UrlHelper(new RequestContext(httpContext, new RouteData()), routes);

            return controller;
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