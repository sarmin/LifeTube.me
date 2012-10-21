using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Web.Mvc;

namespace LifeTube.Controllers
{
    public class ButtonAttribute : ActionMethodSelectorAttribute
    {
        public string Action { get; set; }

        /// <summary>
        /// Determines whether the action method selection is valid for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="methodInfo">Information about the action method.</param>
        /// <returns>true if the action method selection is valid for the specified controller context; otherwise, false.</returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            if (controllerContext == null)
            {
                throw new ArgumentNullException("controllerContext");
            }

            var req = controllerContext.RequestContext.HttpContext.Request;

            return req.Form.AllKeys.Contains(this.Action);

        }

    }
}