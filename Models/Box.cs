using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace magazzinoApp.Models
{
    internal class Box
    {
        public Box(string productCodes, string sizes, string location, string colors = "")
        {
            this.productCodes = productCodes;
            this.colors = colors;
            this.sizes = sizes;
            this.location = location;
            this.templateHTML = ReadTemplateFromFile();
        }

        public string productCodes { get; }
        public string colors { get; }
        public string sizes { get; }
        public string location { get; }

        private string templateHTML { get; set; }

        public string GenerateHTML()
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

        private string ReadTemplateFromFile()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string text = File.ReadAllText($"{currentDir}\\htmlTemplate.txt");
            return text;
        }
    }
}