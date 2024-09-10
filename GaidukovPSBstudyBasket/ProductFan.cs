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

        public ProductFan(string article, double cost, double score, double weight, int deliveryDays, bool turboMod)
        {
            Article = article;
            ProductType = "Фен";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            TurboMod = turboMod;
        }

        public static ProductFan Dyson => new("Dyson", 8000, 3.9, 0.2, 7, true);
        public static ProductFan Veterok => new("Veterok", 1500, 4.3, 0.35, 0, false);
        public static ProductFan Tuvio => new("Tuvio", 2400, 4.6, 0.15, 2, false);

        //List<Product> fans = new List<Product>() { Dyson, Veterok, Tuvio };
    }
}
