using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class OrderCardsGenerator
    {
        public string Article { get; set; }
        public double TotalCost { get; set; }
        public double AverageScore { get; set; }
        public double TotalWeight { get; set; }
        public int DeliveryDays { get; set; }

        OrderGenerator OG = new OrderGenerator();

        List<OrderCardsGenerator> orderCardsList = new List<OrderCardsGenerator>();

        string fileName;
        string jsonString;

        public void GetOrdersChiapperThan(double cost)
        {
            
        }

        public void GetOrderCardsList()
        {
            orderCardsList.Clear();

            foreach (int num in OG.SeachForOrders())
            {
                orderCardsList.Add(GetOrderCard(num));
            }
        }

        OrderCardsGenerator GetOrderCard(int num)
        {
            OrderCardsGenerator OrderCard = new OrderCardsGenerator();

            List<ProductGenerator> order = OG.DeserializeOrder(num.ToString());

            OrderCard.Article = $"order_{num}";
            OrderCard.TotalCost = GetTotalCost(order);
            OrderCard.AverageScore = GetAverageScore(order);
            OrderCard.TotalWeight = GetTotalWeight(order);
            OrderCard.DeliveryDays = GetDeliveryDays(order);

            return OrderCard;
        }

        double GetTotalCost(List<ProductGenerator> order) 
        {
            double totalCost = 0;

            foreach (ProductGenerator product in order)
            {
                totalCost += product.Cost;
            }

            return totalCost;
        }

        double GetAverageScore(List<ProductGenerator> order)
        {
            double score = 0;
            int i = 0;

            foreach (ProductGenerator product in order)
            {
                score += product.Score;
                i++;
            }

            score = score / i;

            return score;
        }

        double GetTotalWeight(List<ProductGenerator> order)
        {
            double totalCosWeight = 0;

            foreach (ProductGenerator product in order)
            {
                totalCosWeight += product.Weight;
            }

            return totalCosWeight;
        }

        int GetDeliveryDays(List<ProductGenerator> order)
        {
            int deliveryDays = 0;

            foreach (ProductGenerator product in order)
            {
                if (product.DeliveryDays > deliveryDays) 
                    deliveryDays = product.DeliveryDays;
            }

            return deliveryDays;
        }
    }
}


/*
        generatedProduct.Article = GetRandomArticle();
        generatedProduct.ProductType = GetRandomTitleByType(Type);
        generatedProduct.Cost = GetRandomCostByType(Type);
        generatedProduct.Score = GetRandomScore();
        generatedProduct.Weight = GetRandomWeight(Type);
        generatedProduct.DeliveryDays = GetRandomDeliveryDays();
        generatedProduct.SpecialFeature = GetRandomSpecialFeature();

        return generatedProduct;
*/