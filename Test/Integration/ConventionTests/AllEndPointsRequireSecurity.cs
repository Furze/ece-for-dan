using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoE.ECE.Web.Infrastructure.Authorisation;
using Shouldly;
using Xunit;
using Xunit.Abstractions;
using System;
using MoE.ECE.Web.Controllers;

namespace MoE.ECE.Integration.Tests.ConventionTests
{
    public class AllEndPointsRequireSecurity
    {
        private readonly ITestOutputHelper _output;

        public AllEndPointsRequireSecurity(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void AllEndPointsRequireDeclarativeSecurity()
        {
            var unsecuredControllers = new List<string>();

            foreach (var controller in GetControllers())
            {
                var attributes = controller.CustomAttributes.Select(ca => ca.AttributeType).ToList();

                if (!attributes.Contains(typeof(AuthorizeAttribute)))
                {
                    unsecuredControllers.Add(controller.Name + " does not have an Authorize Attribute. ");
                }

                // Find all action methods which do not have a RequirePermission
                unsecuredControllers.AddRange(
                    from actionMethod in controller.GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance) 
                    where actionMethod.GetCustomAttribute<RequirePermissionAttribute>() == null 
                    select $"{controller.Name}.{actionMethod.Name}() method does not have an RequirePermission Attribute. ");
            }
            
            foreach (var msg in unsecuredControllers)
            {
                _output.WriteLine(msg);
            }
            
            unsecuredControllers.Count.ShouldBe(0, "A controller does not have an Authorize Attribute. ");
        }

        /// <summary>
        /// Gets all the controllers from the assembly that contains the system.time controller
        /// </summary>
        private static IEnumerable<Type> GetControllers()
        {
            return from t in typeof(Rs7Controller).Assembly.GetTypes()
                where t.IsAbstract == false
                where typeof(ControllerBase).IsAssignableFrom(t)
                select t;
        }
    }
}