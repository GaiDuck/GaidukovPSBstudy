using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class ProductMicrowave : ProductGenerator
    {
        public bool Defrosting { get; set; }

        public ProductMicrowave(string article, double cost, double score, double weight, int deliveryDays, bool defrosting)
        {
            Article = article;
            ProductType = "Микроволновая печь";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            Defrosting = defrosting;
        }

        public static ProductMicrowave Vitek => new("Vitek", 5300, 2.7, 5.5, 0, true);
        public static ProductMicrowave Brizz => new("Brizz", 3200, 3.7, 3.4, 1, true);
        public static ProductMicrowave Liama => new("Liama", 4900, 4.4, 4.2, 5, false);

        //List<Product> microwaves = new List<Product>() { Vitek, Brizz, Liama };
        
    }
}
