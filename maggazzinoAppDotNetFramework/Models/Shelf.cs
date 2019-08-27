using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace maggazzinoAppDotNetFramework.Models
{
    internal class Shelf
    {
        public Shelf(string shelfName)
        {
            Name = shelfName;
            Boxes = new List<Box>();
        }

        public string Name { get; set; }
        public List<Box> Boxes { get; set; }

        public string GenerateHTML()
        {
            StringBuilder htmlString = new StringBuilder();
            string openTag = "\n<div class=\"page\">\n";
            string closeTag = "\n</div>\n";

            for (int i = 1; i <= Boxes.Count; i++)
            {
                if (i % 2 == 0)
                {
                    htmlString.Append('\n').Append(Boxes[i - 1].templateHTML).Append(closeTag);
                }
                if (
                    (i == Boxes.Count && Boxes.Count % 2 != 0) &&
                    (i % 2 != 0)
                    )
                {
                    htmlString.Append(openTag).Append(Boxes[i - 1].templateHTML).Append(closeTag);
                }
                else if (i % 2 != 0 || i == 1)
                {
                    htmlString.Append(openTag).Append(Boxes[i - 1].templateHTML).Append("<br>");
                }
            }

            return htmlString.ToString();
        }
    }
}
