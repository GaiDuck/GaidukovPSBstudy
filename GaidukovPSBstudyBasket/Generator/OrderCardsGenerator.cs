using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GaidukovPSBstudyBasket.DataBase;
using GaidukovPSBstudyBasket.Models;

namespace GaidukovPSBstudyBasket.Generator
{
    internal class OrderCardsGenerator
    {
        OrderGenerator order = new OrderGenerator();

        string fileName;
        string jsonString;

        public void GetOrderCardsList()
        {
            OrdersDataBase.orderCardsList.Clear();

            List<int> OrderNumbers = order.SeachForOrders();

            foreach (int num in OrderNumbers)
            {
                OrdersDataBase.orderCardsList.Add(GetOrderCard(num));
            }
        }

        OrderCardModel GetOrderCard(int num)
        {
            OrderCardModel OrderCard = new OrderCardModel();

            List<ProductsModel> tempOrder = order.DeserializeOrder(num.ToString());

            OrderCard.Article = $"order_{num}";
            OrderCard.TotalCost = GetTotalCost(tempOrder);
            OrderCard.AverageScore = GetAverageScore(tempOrder);
            OrderCard.TotalWeight = GetTotalWeight(tempOrder);
            OrderCard.DeliveryDays = GetDeliveryDays(tempOrder);

            return OrderCard;
        }

        public OrderCardModel GetOrderCard(List<ProductsModel> tempOrder)
        {
            OrderCardModel OrderCard = new OrderCardModel();

            OrderCard.TotalCost = GetTotalCost(tempOrder);
            OrderCard.AverageScore = GetAverageScore(tempOrder);
            OrderCard.TotalWeight = GetTotalWeight(tempOrder);
            OrderCard.DeliveryDays = GetDeliveryDays(tempOrder);

            return OrderCard;
        }

        double GetTotalCost(List<ProductsModel> order)
        {
            double totalCost = 0;

            foreach (ProductsModel product in order)
            {
                totalCost += product.Cost;
            }

            return totalCost;
        }

        double GetAverageScore(List<ProductsModel> order)
        {
            double score = 0;
            int i = 0;

            foreach (ProductsModel product in order)
            {
                score += product.Score;
                i++;
            }

            score = score / i;

            return score;
        }

        double GetTotalWeight(List<ProductsModel> order)
        {
            double totalCosWeight = 0;

            foreach (ProductsModel product in order)
            {
                totalCosWeight += product.Weight;
            }

            return totalCosWeight;
        }

        int GetDeliveryDays(List<ProductsModel> order)
        {
            int deliveryDays = 0;

            foreach (ProductsModel product in order)
            {
                if (product.DeliveryDays > deliveryDays)
                    deliveryDays = product.DeliveryDays;
            }

            return deliveryDays;
        }

        public List<OrderCardModel> GetOrdersChiapperThan(double cost)
        {
            OrdersDataBase.relevantOrderCardsList.Clear();
            OrdersDataBase.relevantOrderCardsList.AddRange(OrdersDataBase.orderCardsList.Where(order => order.TotalCost < cost));
            return OrdersDataBase.relevantOrderCardsList;
        }

        public List<OrderCardModel> GetOrdersMoreExpensiveThan(double cost)
        {
            OrdersDataBase.relevantOrderCardsList.Clear();
            OrdersDataBase.relevantOrderCardsList.AddRange(OrdersDataBase.orderCardsList.Where(order => order.TotalCost > cost));
            return OrdersDataBase.relevantOrderCardsList;
        }

        public List<OrderCardModel> GetOrdersSortedByWeight()
        {
            OrdersDataBase.relevantOrderCardsList.Clear();
            OrdersDataBase.relevantOrderCardsList.AddRange(OrdersDataBase.orderCardsList.OrderBy(order => order.TotalWeight));
            return OrdersDataBase.relevantOrderCardsList;
        }

        public List<OrderCardModel> GetOrdersByDeliveringDate(int deliveryDays)
        {
            OrdersDataBase.relevantOrderCardsList.Clear();
            OrdersDataBase.relevantOrderCardsList.AddRange(OrdersDataBase.orderCardsList.Where(order => order.DeliveryDays <= deliveryDays).OrderBy(order => order.DeliveryDays));
            return OrdersDataBase.relevantOrderCardsList;
        }

    }
}