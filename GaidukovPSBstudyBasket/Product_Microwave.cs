using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class Product_Microwave : ProductsModel
    {
        public bool Defrosting { get; set; }

        public Product_Microwave(string article, double cost, double score, double weight, int deliveryDays, string defrosting)
        {
            Article = article;
            ProductType = "Микроволновая печь";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            SpecialFeature = defrosting;
        }
        
/*
        public override string GetTitle()  //не понял, зачем нужно и как использовать
        {
            return "Микроволновая печь";
        }
*/
        /*
                public static ProductMicrowave Vitek => new("Vitek", 5300, 2.7, 5.5, 0, "есть");
                public static ProductMicrowave Brizz => new("Brizz", 3200, 3.7, 3.4, 1, "есть");
                public static ProductMicrowave Liama => new("Liama", 4900, 4.4, 4.2, 5, "нет");   
        */
    }
}
