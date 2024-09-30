using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GaidukovPSBstudyBasket.Models;

namespace GaidukovPSBstudyBasket.Products
{
    internal class Product_Fan : ProductsModel
    {
        public Product_Fan(string article, double cost, double score, double weight, int deliveryDays, string turboMod)
        {
            Article = article;
            ProductType = "Фен";
            Cost = cost;
            Score = score;
            Weight = weight;
            DeliveryDays = deliveryDays;
            SpecialFeature = turboMod;
        }
    }
}
