using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oShopSolution.Application.Option
{
    public class ImageConfigOption
    {
        public const string ImageConfig = "ImageConfig";

        public string ImagePath { get; set; }

        public List<string> ImageTypes { get; set; }
    }
}
