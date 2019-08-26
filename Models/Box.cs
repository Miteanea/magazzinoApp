namespace magazzinoApp.Models
{
    internal class Box
    {
        public Box(
            string Id,
            string productCodes,
            string colors,
            string sizes,
            string location)
        {
            this.Id = Id;
            this.productCodes = productCodes;
            this.colors = colors;
            this.sizes = sizes;
            this.location = location;
        }
        public string Id { get; }

        public string productCodes { get; }
        public string colors { get; }
        public string sizes { get; }
        public string location { get; }


        public string GenerateHTML()
        {
            // read the template from the text file
            // replace the placeholders with data

            return
             "<div class=\"main-container\"" +
             "   <div class=\"inner-container\" id=\"productCode\">" +
            $"      {productCodes}" +
             "   </div>                                        " +
             "   <div class=\"inner-container\" id=\"colors\">     " +
            $"       {colors}" +
             "   </div>                                        " +
             "   <div class=\"inner-container\" id=\"sizeNumbers\">" +
            $"         {sizes}" +
             "   </div>                                        " +
             "   <div class=\"inner-container\" id=\"boxLocation\">" +
            $"         {location}" +
             "   </div>                                        " +
             "</div>";

        }
    }
}