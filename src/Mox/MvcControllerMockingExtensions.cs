using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Mox
{
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
}