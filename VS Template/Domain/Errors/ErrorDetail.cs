using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace $safeprojectname$.Errors
{
    public class ErrorDetail
    {
        public string Host { get; set; }
        public string Type { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Detail { get; set; }
        public string User { get; set; }
        public DateTime Time { get; set; }
        public int? StatusCode { get; set; }
        public Dictionary<string, string> ServerVariables { get; set; }
        public Dictionary<string, string> Cookies { get; set; }

        public ErrorDetail FromXML(string allXml)
        {
            ServerVariables = new Dictionary<string, string>();
            Cookies = new Dictionary<string, string>();

            // Load the XML
            var xdoc = XDocument.Parse(allXml);
            var errorNode = xdoc.Root;

            var SV_And_Cookie_Info = errorNode.Elements();
            var serverVars = SV_And_Cookie_Info.FirstOrDefault().Elements();
            var cookies = SV_And_Cookie_Info.LastOrDefault().Elements();

            Host = errorNode.Attribute("host").Value;
            Type = errorNode.Attribute("type").Value;
            Message = errorNode.Attribute("message").Value;
            Source = errorNode.Attribute("source").Value;
            Detail = errorNode.Attribute("detail").Value;
            Time = DateTime.Parse(errorNode.Attribute("time").Value, null, DateTimeStyles.RoundtripKind);
            if (errorNode.Attribute("statusCode") != null)
            {
                StatusCode = int.Parse(errorNode.Attribute("statusCode").Value);
            } // end if


            foreach (var variable in serverVars)
            {
                var key = variable.Attribute("name").Value;
                var Value = variable.Element("value").Attribute("string").Value;
                ServerVariables.Add(key, Value);
            } // end loop

            foreach (var cookie in cookies)
            {
                var key = cookie.Attribute("name").Value;
                var Value = cookie.Element("value").Attribute("string").Value;
                Cookies.Add(key, Value);
            } // end loop
            
            return this;
        }
    }
}