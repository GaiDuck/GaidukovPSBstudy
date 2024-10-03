﻿using GaidukovPSBstudyBasket.Models;
using GaidukovPSBstudyCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using C = GaidukovPSBstudyCalculator.Constants; // пример ссылок на статические классы

namespace GaidukovPSBstudyBasket.Generator
{
    internal class OrderGenerator
    {
        ProductsGenerator generator = new ProductsGenerator();
        BasketConvertor basket = new BasketConvertor();

        private static Random random = new Random();

        List<ProductsModel> ProductList = new List<ProductsModel>();
        List<ProductsModel> OrderList = new List<ProductsModel>();
        List<ProductsModel> ShopAssortmentList = new List<ProductsModel>();
        List<ProductsModel> RelevantShopAssortmentList = new List<ProductsModel>();

        ProductsModel currentProduct;
        int OrderNumber { get; set; } = 1;

        bool exit;
        int sortingPattern = 1;
        int productsOnScreenNumber;

        string fileName;
        string jsonString;

        public static string path = @"C:\Users\alexg\OneDrive\Рабочий стол\GaidukovPSBstudyCalculator-08b48238f18b7f9e03efc951a4fc3d611d2aca50\GaidukovPSBstudyCalculator\GaidukovPSBstudyBasket\Orders\";

        ILogger Logger { get; set; }

        internal OrderGenerator() : this(new ConsoleLogger())
        {

        }

        public OrderGenerator(ILogger logger)
        {
            Logger = logger;
        }

        public void CreateUserOrder()
        {
            do
            {
                AddProductToOrder(basket.UsersBasket);

                if (basket.UsersBasket.Count > 0)
                {
                    Logger.SendMessage("\nКорзина:");
                    basket.SellSortedCategoryList(basket.GetSortedProductList(basket.UsersBasket, basket.GetSortingParametr()), basket.UsersBasket.Count());

                    Logger.SendMessage("\nСохранить заказ?" +
                   "\n1 - да" +
                   "\nEnter - нет");

                    if (Logger.ReadMessage() == "1")
                    {
                        SaveOrder(basket.UsersBasket);
                    }
                }
            }
            while (OneMoreOrder());
        }

        void AddProductToOrder(List<ProductsModel> Basket)
        {
            do
            {
                do
                {
                    Logger.SendMessage("\nВыберите интересующую вас категорию товаров:" +
                                       "\n1 - стиральные машины " +
                                       "\n2 - фены " +
                                       "\n3 - микроволновые печи\n");

                    ProductList = GetProductsCategory(Logger.ReadMessage());
                }
                while (ProductList == null);

                basket.SellSortedCategoryList(basket.GetSortedProductList(ProductList, basket.GetSortingParametr()), GetNumberOfProductsOnScreen());

                Logger.SendMessage("\nВведите артикул желаемого товара." +
                                   "\nИли нажмите Enter, если не один из товаров вам не нравится.");

                basket.GetBasket(ProductList, Basket, Logger.ReadMessage());

                Logger.SendMessage("\nЖелаете продолжть покупки?" +
                                   "\nДля продолжения нажмите Enter клавишу." +
                                   "\nДля завершения нажмите 1.\n");

                if (Logger.ReadMessage() == "1")
                {
                    //вот сюда воткнуть метод, возвращающий суммарный заказ. 
                    //только надо переписать метод таким образом, что он принимает не номер заказа, а список продуктов.
                    OrderCardsGenerator orderCard = new OrderCardsGenerator();
                    OrderCardModel currentOrder = orderCard.GetOrderCard(Basket);
                    Logger.SendMessage($"\nОбщие параметры текущего заказа:\n" +
                                       $"\nОбщая стоимость: {currentOrder.TotalCost}" +
                                       $"\nСредняя оценка: {currentOrder.AverageScore}" +
                                       $"\nОбщий вес: {currentOrder.TotalWeight}" +
                                       $"\nВремя доставки: {currentOrder.DeliveryDays}д.\n");

                    exit = true;
                }
                else
                    exit = false;
            }
            while (!exit);
        }

        List<ProductsModel> GetProductsCategory(string category)
        {
            List<ProductsModel> ProductrsCategory = new List<ProductsModel>();

            switch (category)
            {
                case "1":
                    fileName = "WashingMachines.json";
                    jsonString = File.ReadAllText(fileName);
                    ProductrsCategory = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);
                    break;

                case "2":
                    fileName = "Fans.json";
                    jsonString = File.ReadAllText(fileName);
                    ProductrsCategory = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);
                    break;

                case "3":
                    fileName = "Microwaves.json";
                    jsonString = File.ReadAllText(fileName);
                    ProductrsCategory = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);
                    break;

                default:
                    Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                    ProductrsCategory = null;
                    break;
            }

            return ProductrsCategory;
        }

        int GetNumberOfProductsOnScreen()
        {
            Logger.SendMessage("\nСколько товаров категории отобразить?\n");
            bool parced = int.TryParse(Logger.ReadMessage(), out int productsOnScreen);

            if (!parced || productsOnScreen < 1 || productsOnScreen > 10)
            {
                Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                Logger.SendMessage("Выбрано значение по умолчанию: 3.");
                productsOnScreenNumber = 3;
            }
            else
                productsOnScreenNumber = productsOnScreen;

            return productsOnScreenNumber;
        }

        public void CreateRandomOrder()
        {
            OrderList.Clear();

            int orderCount = GetNumberOfRandomOrders();

            for (int i = 0; i < orderCount; i++)
            {
                int orderProductsCount = random.Next(1, 6);
                OrderList.Clear();

                for (int j = 0; j < orderProductsCount; j++)
                {
                    ProductsModel product = generator.GetRandomProduct(generator.GetRandomType());
                    OrderList.Add(product);
                }

                Logger.SendMessage("Заказ успешно создан.");

                SaveOrder(OrderList);
            }
        }

        public void CreateRandomOrder(string mod)
        {
            OrderList.Clear();

            int orderCount = GetNumberOfRandomOrders();
            int sortingParametr = 1;

            switch (mod)
            {
                case "cheap":
                    sortingParametr = 2;
                    break;

                case "qualitative":
                    sortingParametr = 3;
                    break;

                case "fast delivery":
                    sortingParametr = 5;
                    break;
            }

            for (int i = 0; i < orderCount; i++)
            {
                int orderProductsCount = random.Next(1, 6);
                OrderList.Clear();

                for (int j = 0; j < orderProductsCount; j++)
                {
                    string category = random.Next(1, 4).ToString();
                    List<ProductsModel> Category = basket.GetSortedProductList(GetProductsCategory(category), sortingParametr);

                    OrderList.Add(Category[0]);
                }

                Logger.SendMessage("Заказ успешно создан.");

                SaveOrder(OrderList);
            }
        }

        public void GenerateOrderBySumm(double maxSumm)
        {
            OrderList.Clear();

            do
            {
                GenerateRelevantShopAssortmentBySumm(maxSumm);

                if (RelevantShopAssortmentList.Count == 0)
                {
                    break;
                }
                else
                {
                    currentProduct = RelevantShopAssortmentList[random.Next(0, RelevantShopAssortmentList.Count)];
                    maxSumm = maxSumm - currentProduct.Cost;
                    OrderList.Add(currentProduct);
                }
            }
            while (true);

            SaveOrder(OrderList);
        }

        public void GenerateOrderBySumm(double minSumm, double maxSumm)
        {
            OrderList.Clear();

            do
            {
                GenerateRelevantShopAssortmentBySumm(maxSumm);

                if (RelevantShopAssortmentList.Count == 0)
                {
                    Logger.SendMessage("Заданы некорректные рамки стоимости заказа.");
                    break;
                }
                else
                {
                    currentProduct = RelevantShopAssortmentList[random.Next(0, RelevantShopAssortmentList.Count)];
                    maxSumm = maxSumm - currentProduct.Cost;
                    minSumm = minSumm - currentProduct.Cost;
                    OrderList.Add(currentProduct);
                }
            }
            while (minSumm > 0);

            SaveOrder(OrderList);
        }

        public void GenerateOrderByCount(int maxCount)
        {
            OrderList.Clear();

            for (int i = 0; i < maxCount; i++)
            {
                int orderProductsCount = random.Next(1, 6);

                OrderList.Clear();

                for (int j = 0; j < orderProductsCount; j++)
                {
                    string category = random.Next(1, 4).ToString();
                    List<ProductsModel> Category = GetProductsCategory(category);

                    OrderList.Add(Category[random.Next(0, Category.Count())]);
                }

                Logger.SendMessage("Заказ успешно создан.");

                SaveOrder(OrderList);
            }
        }

        void GenerateRelevantShopAssortmentBySumm(double maxSumm)
        {
            RelevantShopAssortmentList.Clear();

            for (int i = 1; i < 4; i++)
            {
                RelevantShopAssortmentList.AddRange(GetProductsCategory(i.ToString()).Where(s => s.Cost <= maxSumm));
            }

            Logger.SendMessage($"\nНайденно удовлетворяющих требованиям продуктов: {RelevantShopAssortmentList.Count}\n");
        }

        int GetNumberOfRandomOrders()
        {
            int orderCount = 1;

            Logger.SendMessage("\nСколько заказов вы желаете создать?");
            bool parced = int.TryParse(Logger.ReadMessage(), out int input);

            if (parced)
                orderCount = input;
            else
                Logger.SendMessage($"Выбранно значение по умолчанию: {orderCount}");

            return orderCount;
        }

        public void SaveOrder(List<ProductsModel> UsersBasket)
        {

            while (File.Exists(path + "order_" + OrderNumber.ToString() + ".json"))
            {
                OrderNumber++;
            }

            generator.SerializeOrder(UsersBasket, OrderNumber);

            Logger.SendMessage($"Заказ Order_{OrderNumber} успешно сохранен.");
            UsersBasket.Clear();

            OrderNumber++;
        }

        public void SaveOrder(List<ProductsModel> UsersBasket, int orderNumber)
        {
            OrderNumber = orderNumber;

            while (File.Exists(path + "order_" + OrderNumber.ToString() + ".json"))
            {
                OrderNumber++;
            }

            generator.SerializeOrder(UsersBasket, OrderNumber);

            Logger.SendMessage($"Заказ Order_{OrderNumber} успешно сохранен.");
            UsersBasket.Clear();

            OrderNumber++;
        }

        public bool OneMoreOrder()
        {
            bool makeOneMoreOrder = true;

            Logger.SendMessage("\nХотите сделать еще один заказ?" +
                               "\n1 - да" +
                               "\nEnter - нет");

            if (Logger.ReadMessage() != "1")
            {
                makeOneMoreOrder = false;
            }

            return makeOneMoreOrder;
        }

        /// <summary>
        /// Метод создан для того, чтобы можно было протестировать метод OneMoreOrder().
        /// </summary>
        /// <param name="str"> текст, который обычно получается из консоли </param>
        /// <returns></returns>
        public bool OneMoreOrder(string str)
        {
            bool makeOneMoreOrder = true;

            Logger.SendMessage("\nХотите сделать еще один заказ?" +
                               "\n1 - да" +
                               "\nEnter - нет");

            if (Logger.ReadMessage(str) != "1")
            {
                makeOneMoreOrder = false;
            }

            return makeOneMoreOrder;
        }

        public void ReadOrder(List<int> OrderNumbers)
        {
            EnumerateOrders(OrderNumbers);

            do
            {
                Logger.SendMessage("Введите номер интересующего вас заказа:");

                bool parced = int.TryParse(Logger.ReadMessage(), out int orderNum);

                if (parced)
                {
                    fileName = $"{path}order_{orderNum}.json";
                    jsonString = File.ReadAllText(fileName);
                    List<ProductsModel> order = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);

                    basket.SellSortedCategoryList(order, order.Count);

                    Logger.SendMessage("\nХотите посмотреть еще 1 заказ?" +
                                       "\nДа - нажмите Enter" +
                                       "\nНет - нажмите 1\n");
                    switch (Logger.ReadMessage())
                    {
                        case "1":
                            exit = true;
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                    Logger.SendMessage("Такой заказ не найден.");
                }
            }
            while (!exit);
        }

        public void EditOrder(List<int> OrderNumbers)
        {
            Logger.SendMessage("Найдены следующие заказы:");
            EnumerateOrders(OrderNumbers);

            Logger.SendMessage("Введите номер заказа, который хотите изменить:");

            bool parced = int.TryParse(Logger.ReadMessage(), out int input);

            if (parced)
            {
                Logger.SendMessage("\nЧто сделать с заказом?" +
                                   "\n1 - добавить товар" +
                                   "\n2 - удалить товар," +
                                   "\n3 - удалить заказ.");

                switch (Logger.ReadMessage())
                {
                    case "1":
                        AddProductToOrder(input);
                        break;

                    case "2":
                        DeleteProductFromOrder(input);
                        break;

                    case "3":
                        DeleteOrder(input);
                        break;

                    default:
                        Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                        break;
                }
            }
            else
                Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
        }

        void AddProductToOrder(int orderNum)
        {
            bool exit = true;
            bool orderIsFound = false;
            List<ProductsModel> order = new List<ProductsModel>();

            do
            {
                if (File.Exists(path + "order_" + orderNum.ToString() + ".json"))
                {
                    fileName = $"{path}order_{orderNum}.json";
                    jsonString = File.ReadAllText(fileName);
                    order = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);

                    orderIsFound = true;
                    DeleteOrder(orderNum);
                }
                else
                {
                    Logger.SendMessage($"order_{orderNum.ToString()} не существует.");
                    Logger.SendMessage("\nПопробовать еще раз?" +
                                       "\n1 - да" +
                                       "\nEnter - нет");

                    if (Logger.ReadMessage() == "1")
                        exit = false;
                }
            }
            while (!exit);

            if (orderIsFound)
            {
                AddProductToOrder(order);
                generator.SerializeOrder(order, orderNum);
            }
        }

        void DeleteProductFromOrder(int orderNum)
        {
            bool exit = true;
            bool orderIsFound = false;
            List<ProductsModel> order = new List<ProductsModel>();

            do
            {
                if (File.Exists(path + "order_" + orderNum.ToString() + ".json"))
                {
                    fileName = $"{path}order_{orderNum}.json";
                    jsonString = File.ReadAllText(fileName);
                    order = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);

                    orderIsFound = true;
                    DeleteOrder(orderNum);
                }
                else
                {
                    Logger.SendMessage($"order_{orderNum.ToString()} не существует.");
                    Logger.SendMessage("\nПопробовать еще раз?" +
                                       "\n1 - да" +
                                       "\nEnter - нет");
                    if (Logger.ReadMessage() == "1")
                        exit = false;
                }
            }
            while (!exit);

            if (orderIsFound)
            {
                Logger.SendMessage("Введите артикул товара, который хотите удалить:");
                basket.SellSortedCategoryList(basket.GetSortedProductList(order, 1), order.Count);

                string removeProductArticle = Logger.ReadMessage();

                foreach (ProductsModel product in order)
                {
                    if (product.Article == removeProductArticle)
                    {
                        order.Remove(product);
                        break;
                    }
                }

                generator.SerializeOrder(order, orderNum);
            }
        }

        void DeleteOrder(int orderNum)
        {
            if (File.Exists(path + "order_" + orderNum.ToString() + ".json"))
            {
                Logger.SendMessage($"order_{orderNum.ToString()} удален.");
                File.Delete($"{path}order_{orderNum.ToString()}.json");
            }
            else
                Logger.SendMessage($"order_{orderNum.ToString()} не существует.");
        }

        void EnumerateOrders(List<int> OrderNumbers)
        {
            foreach (int j in OrderNumbers)
            {
                Logger.SendMessage($"Order_{j}");
            }
        }

        public List<int> SeachForOrders()
        {
            List<int> OrderNumbers = new List<int>();
            int fileQuantity = Directory.GetFiles(path).Length;

            int i = 0;

            while (fileQuantity > 0)
            {
                if (File.Exists($"{path}order_{i}.json"))
                {
                    OrderNumbers.Add(i);
                    fileQuantity--;
                }
                i++;
            }

            return OrderNumbers;
        }

        public List<int> SeachForOrders(string path)
        {
            List<int> OrderNumbers = new List<int>();
            int fileQuantity = Directory.GetFiles(path).Length;

            int i = 0;

            while (fileQuantity > 0)
            {
                if (File.Exists($"{path}order_{i}.json"))
                {
                    OrderNumbers.Add(i);
                    fileQuantity--;
                }
                i++;
            }

            return OrderNumbers;
        }

        public List<ProductsModel> DeserializeOrder(string orderNum)
        {
            fileName = $"{path}order_{orderNum}.json";
            jsonString = File.ReadAllText(fileName);
            List<ProductsModel> order = JsonSerializer.Deserialize<List<ProductsModel>>(jsonString);
            return order;
        }
    }
}