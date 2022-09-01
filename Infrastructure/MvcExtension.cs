﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace SUMBER.Infrastructure
{
    public static class MvcExtension
    {
        public static string ActiveClass(this IHtmlHelper htmlHelper, string controllers = null, string actions = null, string cssClass = "font-weight-bold")
        {
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

            var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
            var acceptedActions = (actions ?? currentAction ?? "").Split(',');

            return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
                ? cssClass
                : "";
        }

        public static string ActiveSideBarClass(this IHtmlHelper htmlHelper, string controllers = null, string actions = null, string cssClass = "ac-success text-white rounded shadow")
        {
            var currentController = htmlHelper?.ViewContext.RouteData.Values["controller"] as string;
            var currentAction = htmlHelper?.ViewContext.RouteData.Values["action"] as string;

            var acceptedControllers = (controllers ?? currentController ?? "").Split(',');
            var acceptedActions = (actions ?? currentAction ?? "").Split(',');

            return acceptedControllers.Contains(currentController) && acceptedActions.Contains(currentAction)
                ? cssClass
                : "";
        }
    }
}
