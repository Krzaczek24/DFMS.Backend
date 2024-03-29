﻿using DFMS.WebApi.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DFMS.WebApi.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ApiRouteAttribute : RouteAttribute
    {
        public ApiRouteAttribute() : base(ControllerGroup.Api)
        {

        }

        public ApiRouteAttribute(string template) : base($"{ControllerGroup.Api}/{template}")
        {

        }
    }
}
