using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GaidukovPSBstudyBasket.Models;

namespace GaidukovPSBstudyBasket.Products
{
    internal class Product_WashingMachine : ProductsModel
    {
        public Product_WashingMachine(string article, double cost, double score, double weight, int deliveryDays, string dryer)
        {
            Article = article;
            ProductType = "Стиральная машина";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            SpecialFeature = dryer;
        }
    }
}