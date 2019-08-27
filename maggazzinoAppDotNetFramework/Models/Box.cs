using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maggazzinoAppDotNetFramework.Models
{
   
    internal class Box
    {
        public Box(string productCodes, string sizes, string location, string templateHTML, string colors = "")
        {
            this.productCodes = productCodes;
            this.colors = colors;
            this.sizes = sizes;
            this.location = location;
            this.templateHTML = GenerateHTML(templateHTML);
        }

        public string productCodes { get; }
        public string colors { get; }
        public string sizes { get; }
        public string location { get; }

        public string templateHTML { get; }

        public string GenerateHTML(string templateHTML)
        {
            List<string> propertyNames = this.GetType().GetProperties().Select(x => x.Name).ToList();

            string template = templateHTML;
            foreach (string propName in propertyNames)
            {
                template = template.Replace($"%{propName}%", (string)GetPropValue(this, propName));
            }

            return template;
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}

