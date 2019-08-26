using System.Collections.Generic;

namespace magazzinoApp.Models
{
    internal class Shelf
    {
        public string Id { get; set; }
        public List<Box> Boxes { get; set; }

        public string GenerateHTML()
        {
            var htmlString = "<page>";

            for (int i = 1; i <= Boxes.Count; i++)
            {
                if (i == 1)
                {
                    htmlString += Boxes[i];
                }
                if (i % 2 == 0 ||
                    (i == Boxes.Count && Boxes.Count % 2 != 0))
                {
                    htmlString += Boxes[i] + "</page>";
                }
                if (i % 2 != 0)
                {
                    htmlString += Boxes[i];
                }
            }

            return htmlString;
        }
    }
}