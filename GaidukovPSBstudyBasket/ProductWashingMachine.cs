using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class ProductWashingMachine : ProductGenerator
    {
        public ProductWashingMachine(string article, double cost, double score, double weight, int deliveryDays, string dryer)
        {
            Article = article;
            ProductType = "Стиральная машина";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            SpecialFeature = dryer;
        }

        public static ProductWashingMachine Bosh => new ProductWashingMachine("Bosh", 20000, 4.9, 55, 1, "есть");
        public static ProductWashingMachine Ural => new("Ural", 14000, 3.8, 45, 3, "нет");
        public static ProductWashingMachine Toshiba => new("Toshiba", 17500, 4.2, 48, 0, "есть");

    }
}
