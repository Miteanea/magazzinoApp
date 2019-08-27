using magazzinoApp.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace magazzinoApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // open excel

            FileInfo sourceExcel = new FileInfo($"{Directory.GetCurrentDirectory()}\\magazzino.xlsx");
            List<Shelf> shelves = new List<Shelf>();

            using (ExcelPackage package = new ExcelPackage(sourceExcel))
            {
                ExcelWorkbook wb = package.Workbook;

                List<MappingData> creationMetadata = GetCreationMetadata(wb);
                shelves = CreateShelves(creationMetadata, wb);
            }

            StringBuilder result = new StringBuilder();
            foreach (Shelf shelf in shelves)
            {
                result.Append(shelf.GenerateHTML());
            }

            string htmlShelves = result.ToString();

            string mainTemplate = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\magazzinoTemplate.html");

            string finalDocument = mainTemplate.Replace("%dataPlaceholder%", htmlShelves);

            File.WriteAllText($"{Directory.GetCurrentDirectory()}\\final.html", finalDocument);
            // read worksheets

            // create boxes for every shelf
        }

        private static List<Shelf> CreateShelves(List<MappingData> creationMetadata, ExcelWorkbook wb)
        {
            Dictionary<string, Shelf> shelves = CreateShelfDict(creationMetadata);
            string boxTemplateHTML = ReadTemplateFromFile();
            foreach (MappingData mappingData in creationMetadata)
            {
                Box box = GetBoxFromMetadata(mappingData, wb, boxTemplateHTML);

                if (box != null)
                {
                    shelves[mappingData.ShelfName].Boxes.Add(box);
                }
            }

            return shelves.Values.ToList();
        }

        private static Box GetBoxFromMetadata(MappingData mappingData, ExcelWorkbook wb, string templateHTML)
        {
            string rangeAddress = $"{mappingData.StartOfRangeBox}:{mappingData.EndOfRangeBox}";
            ExcelRange boxData = wb.Worksheets[mappingData.SourceWorkSheet].Cells[rangeAddress];

            string prodCodes = ((object[,])boxData.Value)[0, 0]?.ToString();
            string colors = ((object[,])boxData.Value)[1, 0]?.ToString();
            string sizes = ((object[,])boxData.Value)[2, 0]?.ToString();

            if (string.IsNullOrWhiteSpace(prodCodes) &&
                string.IsNullOrWhiteSpace(colors) &&
                string.IsNullOrWhiteSpace(sizes))
            {
                return null;
            }

            return new Box(
                    productCodes: prodCodes,
                    colors: colors,
                    sizes: sizes,
                    templateHTML: templateHTML,
                    location: GetLocationString(mappingData)
                );
        }

        private static string GetLocationString(MappingData mappingData)
        {
            var builder = new StringBuilder();
            builder.Append(mappingData.ShelfName);
            builder.Append("-r");
            builder.Append(mappingData.RowBox + 1);
            builder.Append("-c");
            builder.Append(mappingData.ColBox);

            return builder.ToString();
        }

        private static Dictionary<string, Shelf> CreateShelfDict(List<MappingData> creationMetadata)
        {
            Dictionary<string, Shelf> shelves = new Dictionary<string, Shelf>();

            List<string> shelfNames = creationMetadata.GroupBy(x => x.ShelfName).Select(x => x.Key).ToList();

            foreach (string shelfName in shelfNames)
            {
                shelves[shelfName] = new Shelf(shelfName);
            }

            return shelves;
        }

        private static List<MappingData> GetCreationMetadata(ExcelWorkbook wb)
        {
            ExcelWorksheet map = wb.Worksheets["map"];
            List<MappingData> creationMetadata = new List<MappingData>();

            int startRow = 2;
            string iterationControlValue = map.Cells[startRow, 1].Value?.ToString();
            while (!string.IsNullOrWhiteSpace(iterationControlValue))
            {
                // fix

                ExcelRange dataRange = map.Cells;

                creationMetadata.Add(
                    new MappingData(
                        sourceWorkSheet: dataRange[startRow, 1].Value.ToString(),
                        shelfName: dataRange[startRow, 2].Value.ToString(),
                        rowBox: int.Parse(dataRange[startRow, 3].Value.ToString()),
                        colBox: int.Parse(dataRange[startRow, 4].Value.ToString()),
                        startOfRangeBox: dataRange[startRow, 5].Value.ToString(),
                        endOfRangeBox: dataRange[startRow, 6].Value.ToString()
                    ));

                startRow++;
                iterationControlValue = map.Cells[startRow, 1].Value?.ToString();
            }
            return creationMetadata;
        }

        private static string ReadTemplateFromFile()
        {
            string currentDir = Directory.GetCurrentDirectory();
            string text = File.ReadAllText($"{currentDir}\\boxHtmlTemplate.html");
            return text;
        }
    }
}