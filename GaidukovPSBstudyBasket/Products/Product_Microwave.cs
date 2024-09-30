using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GaidukovPSBstudyBasket.Models;

namespace GaidukovPSBstudyBasket.Products
{
    internal class Product_Microwave : ProductsModel
    {
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
    }
}
