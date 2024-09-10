using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class ProductFan : ProductGenerator
    {
        public bool TurboMod { get; set; }

        public ProductFan(string article, double cost, double score, double weight, int deliveryDays, string turboMod)
        {
            Article = article;
            ProductType = "Фен";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            SpecialFeature = turboMod;
        }

        public static ProductFan Dyson => new("Dyson", 8000, 3.9, 0.2, 7, "есть");
        public static ProductFan Veterok => new("Veterok", 1500, 4.3, 0.35, 0, "нет");
        public static ProductFan Tuvio => new("Tuvio", 2400, 4.6, 0.15, 2, "нет");

        //List<Product> fans = new List<Product>() { Dyson, Veterok, Tuvio };
    }
}
