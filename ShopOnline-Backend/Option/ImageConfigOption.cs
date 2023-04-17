using System.Collections.Generic;

namespace ShopOnline_Backend.Option
{
    public class ImageConfigOption
    {
        public const string ImageConfig = "ImageConfig";

        public string ImagePath { get; set; } = null!;

        public List<string> ImageTypes { get; set; } = null!;
    }
}
