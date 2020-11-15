using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using MoE.ECE.Web.Infrastructure.Authorisation;
using Newtonsoft.Json;

namespace MoE.ECE.CLI.Commands
{
    public class AuthorisationReportCommands
    {
        public Command AuthorisationReportCommand
        {
            get
            {
                Command? command = new("auth-report", "Generate authorisation report for API")
                {
                    new Option<string?>(new[] {"-rp", "--report-path"},
                        "The path to write the report to ")
                };

                command.Handler = CommandHandler.Create<string?>(GenerateAuthorisationReport);
                return command;
            }
        }

        private async Task<int> GenerateAuthorisationReport(string? reportPath)
        {
            Console.WriteLine("Generating Authorisation report...");

            AuthorisationReport? authReport = new();

            Console.WriteLine("All distinct permissions:");
            Console.WriteLine(string.Join(
                Environment.NewLine,
                authReport.PermissionsList
                    .Select(permission => $" - {permission}")));

            Console.WriteLine("Route permissions:");
            Console.WriteLine(string.Join(
                Environment.NewLine,
                authReport.AllApiRoutes
                    .Select(p =>
                        $" - {p.RouteInformation.RouteDescription} requires \"{p.MethodRequiredPermission}\"")));

            reportPath ??= "./auth-report.json";

            await File.WriteAllTextAsync(reportPath, JsonConvert.SerializeObject(authReport));

            Console.WriteLine($"Written authorisation report to {reportPath}");

            return await Task.FromResult(0);
        }

        public class AuthorisationReport
        {
            public AuthorisationReport()
            {
                IEnumerable<MethodAuthorisationInfo>? allApiRoutes = BuildMethodAuthorisationInfo();
                AllApiRoutes = allApiRoutes.ToList();
                PermissionsList = AllApiRoutes
                    .GroupBy(a => a.MethodRequiredPermission)
                    .Select(g => g.First().MethodRequiredPermission)
                    .ToList();
            }

            public List<string> PermissionsList { get; }

            public List<MethodAuthorisationInfo> AllApiRoutes { get; }

            private IEnumerable<MethodAuthorisationInfo> BuildMethodAuthorisationInfo()
                => typeof(Web.Program).Assembly.GetTypes()
                    .Where(t => !t.IsAbstract)
                    .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                    .SelectMany(c => c
                        .GetMethods(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance)
                        .Select(m => new MethodAuthorisationInfo(m, c)));
        }

        public class MethodAuthorisationInfo
        {
            private readonly MethodInfo _methodInfo;

            public MethodAuthorisationInfo(MethodInfo methodInfo, Type controller)
            {
                _methodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
                MethodRequiredPermission = methodInfo.GetCustomAttribute<RequirePermissionAttribute>()?.Permission
                                           ?? "NO PERMISSIONS!!!";

                RouteAttribute? controllerRouteAttribute =
                    controller.GetCustomAttributes(typeof(RouteAttribute)).FirstOrDefault() as RouteAttribute;
                RouteInformation = GetApiRouteFor(controllerRouteAttribute?.Template ?? "/");
                ControllerName = controller.Name;
                ControllerNamespace = controller.Namespace ?? "";
                MethodName = _methodInfo.Name;
                FullMethodDeclaration = $"{ControllerName}.{MethodName}({GetMethodParameters()})";
            }

            public string ControllerName { get; }

            public string ControllerNamespace { get; }

            public string MethodName { get; }

            public RouteInfo RouteInformation { get; }

            public string MethodRequiredPermission { get; }

            public string FullMethodDeclaration { get; }

            private string GetMethodParameters()
                => string.Join(",",
                    _methodInfo.GetParameters()
                        .OrderBy(p => p.Position)
                        .Select(p => p.Name));

            private RouteInfo GetApiRouteFor(string routePrefix)
                => new(
                    routePrefix,
                    _methodInfo.GetCustomAttributes(typeof(HttpMethodAttribute))
                        .FirstOrDefault() as HttpMethodAttribute,
                    _methodInfo.GetCustomAttributes(typeof(RouteAttribute)).FirstOrDefault() as RouteAttribute);

            public class RouteInfo
            {
                public RouteInfo(string routePrefix, HttpMethodAttribute? httpMethodAttribute,
                    RouteAttribute? routeAttribute)
                {
                    HttpVerbs = httpMethodAttribute?.HttpMethods?.ToList() ?? new List<string> {HttpMethods.Get};
                    Route = $"{routePrefix}/{routeAttribute?.Template ?? ""}";
                    RouteDescription = $"[{string.Join(", ", HttpVerbs)}] {Route}";
                }

                public string Route { get; }

                public string RouteDescription { get; }

                public List<string> HttpVerbs { get; }
            }
        }
    }
}