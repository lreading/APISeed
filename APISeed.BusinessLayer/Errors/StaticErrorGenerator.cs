using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace APISeed.BusinessLayer.Errors
{
    /// <summary>
    /// Responsible for generating HTML error pages when the application
    /// is in a good state.
    /// </summary>
    public class StaticErrorGenerator
    {
        private readonly string _errorController;
        private readonly string _baseUrl;
        private readonly Type _globalAsaxType;
        private readonly IEnumerable<string> _actionNames;

        /// <summary>
        /// Creates a new HtmlGenerator
        /// </summary>
        /// <param name="errorController"></param>
        public StaticErrorGenerator(string errorController, Type globalAsaxType, string baseUrl)
        {
            _baseUrl = baseUrl;
            _errorController = errorController;
            _globalAsaxType = globalAsaxType;
            _actionNames = GetControllerActions();
        }

        /// <summary>
        /// Creates the static error pages
        /// </summary>
        /// <param name="errorPageFolderPhysicalPath"></param>
        public void GenerateStaticErrorPages(string errorPageFolderPhysicalPath)
        {
            if (!Directory.Exists(errorPageFolderPhysicalPath)) Directory.CreateDirectory(errorPageFolderPhysicalPath);
            var client = new RestClient(_baseUrl);

            foreach (var action in _actionNames)
            {
                // Get the HTML, and write it to file using the same name as the action method
                var request = new RestRequest(_errorController + "/" + action, Method.GET);
                var responseHtml = client.Execute(request).Content;
                var fileName = errorPageFolderPhysicalPath + "\\" + action + ".html";
                File.WriteAllText(fileName, responseHtml);
            }
        }

        /// <summary>
        /// Get all of the method names in the error controller using reflection
        /// </summary>
        /// <returns></returns>
        private IEnumerable<string> GetControllerActions()
        {
            // Adapted from AVH at StackOverflow: http://stackoverflow.com/a/30969888/3033053
            var assembly = Assembly.GetAssembly(_globalAsaxType);
            return assembly.GetTypes()
                .Where(type => typeof(System.Web.Mvc.Controller).IsAssignableFrom(type))
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.Public))
                .Where(m => !m.GetCustomAttributes(typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute), true).Any())
                .Where(x => x.DeclaringType.Name == _errorController + "Controller" && x.ReturnType.Name == "ActionResult")
                .Select(x => x.Name);
        }
    }
}
