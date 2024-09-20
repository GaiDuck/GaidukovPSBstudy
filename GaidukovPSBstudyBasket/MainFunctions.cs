﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class MainFunctions
    {
        ConsoleLogger logger = new ConsoleLogger();
        ProductGenerator generator = new ProductGenerator();
        BasketConvertor basket = new BasketConvertor();
        Random random = new Random();

        List<ProductGenerator> Product = new List<ProductGenerator>();

        int OrderNumber { get; set; } = 1;

        bool exit;
        int sortingPattern = 1;
        int productsOnScreenNumber;

        string fileName;
        string jsonString;

        //Это, конечно, костыльное решение, но оно необходимо, чтобы проверять, если ли в папке файл заказа с определенным номером. 
        //Я хочу использовать это для того, чтобы не приходилось угадывать, какие файлы уже созданы, а читать из списка существующих, и чтобы вновь создаваемые файлы не перезаписывали старые. 
        public static string path = @"C:\Users\alexg\OneDrive\Рабочий стол\GaidukovPSBstudyCalculator-08b48238f18b7f9e03efc951a4fc3d611d2aca50\GaidukovPSBstudyCalculator\GaidukovPSBstudyBasket\Orders\";

        public void CreateUserOrder()
        {
            do
            {
                AddProductToBasket(basket.UsersBasket);

                logger.SendMessage("\nКорзина:");
                basket.SellSortedCategoryList(basket.GetSortedProductList(basket.UsersBasket, basket.GetSortingParametr()), basket.UsersBasket.Count());

                logger.SendMessage("\nСохранить заказ?" +
                   "\n1 - да" +
                   "\nEnter - нет");

                if (logger.ReadMessage() == "1")
                {
                    SaveOrder(basket.UsersBasket);
                }
            }
            while (OneMoreOrder());
        }

        void AddProductToBasket(List<ProductGenerator> Basket)
        {
            do
            {
                do 
                {
                    logger.SendMessage(LogMessage.ShopCategoryMessage);
                    Product = GetProductsCategory(logger.ReadMessage());
                }
                while (Product == null);

                productsOnScreenNumber = GetNumberOfProductsOnScreen();
                basket.SellSortedCategoryList(basket.GetSortedProductList(Product, basket.GetSortingParametr()), productsOnScreenNumber);

                logger.SendMessage("\nВведите артикул желаемого товара.\n");

                basket.GetBasket(Product, Basket, logger.ReadMessage());

                logger.SendMessage("\nЖелаете продолжть покупки?" +
                                   "\nДля продолжения нажмите Enter клавишу." +
                                   "\nДля завершения нажмите 1.\n");

                if (logger.ReadMessage() == "1")
                    exit = true;
                else
                    exit = false;
            }
            while (!exit);
        }

        List<ProductGenerator> GetProductsCategory(string category)
        {
            List<ProductGenerator> ProductrsCategory = new List<ProductGenerator>();

            switch (category)
            {
                case "1":
                    fileName = "WashingMachines.json";
                    jsonString = File.ReadAllText(fileName);
                    ProductrsCategory = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);
                    break;

                case "2":
                    fileName = "Fans.json";
                    jsonString = File.ReadAllText(fileName);
                    ProductrsCategory = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);
                    break;

                case "3":
                    fileName = "Fans.json";
                    jsonString = File.ReadAllText(fileName);
                    ProductrsCategory = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);
                    break;

                default:
                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                    break;
            }

            return ProductrsCategory;
        }

        int GetNumberOfProductsOnScreen()
        {
            logger.SendMessage("\nСколько товаров категории отобразить?\n");
            bool parced = int.TryParse(logger.ReadMessage(), out int productsOnScreen);

            if (!parced || productsOnScreen > 10)
            {
                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                logger.SendMessage("Выбрано значение по умолчанию: 3.");
            }
            else
                productsOnScreenNumber = productsOnScreen;

            return productsOnScreenNumber;
        }

        public void CreateRandomOrder()
        {
            List<ProductGenerator> Order = new List<ProductGenerator>();

            int orderCount = GetNumberOfRandomOrders();

            for (int i = 0; i < orderCount; i++)
            {
                int orderProductsCount = random.Next(1, 6);
                Order.Clear();

                for (int j = 0; j < orderProductsCount; j++)
                {
                    ProductGenerator product = generator.GetRandomProduct(generator.GetRandomType());
                    Order.Add(product);
                }

                logger.SendMessage("Заказ успешно создан.");

                SaveOrder(Order);
            }
        }

        public void CreateRandomOrder(string mod)
        {
            List<ProductGenerator> Order = new List<ProductGenerator>();

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
                Order.Clear();

                for (int j = 0; j < orderProductsCount; j++)
                {
                    string category = random.Next(1, 4).ToString();
                    List<ProductGenerator> Category = basket.GetSortedProductList(GetProductsCategory(category), sortingParametr);

                    Order.Add(Category[0]);
                }

                logger.SendMessage("Заказ успешно создан.");

                SaveOrder(Order);
            }
        }



        int GetNumberOfRandomOrders()
        {
            int orderCount = 1;

            logger.SendMessage("\nСколько заказов вы желаете создать?");
            bool parced = int.TryParse(logger.ReadMessage(), out int input);

            if (parced)
                orderCount = input;
            else
                logger.SendMessage($"Выбранно значение по умолчанию: {orderCount}");

            return orderCount;
        }

        public void SaveOrder(List<ProductGenerator> UsersBasket)
        {

            while (File.Exists(path + "order_" + OrderNumber.ToString() + ".json"))
            {
                //logger.SendMessage("Заказ с таким номером уже существует");
                OrderNumber++;
            }

            generator.SerializeOrder(UsersBasket, OrderNumber);

            logger.SendMessage($"Заказ Order_{OrderNumber} успешно сохранен.");
            UsersBasket.Clear();

            OrderNumber++;
        }

        public bool OneMoreOrder()
        {
            bool makeOneMoreOrder = true;

            logger.SendMessage("\nХотите сделать еще один заказ?" +
                               "\n1 - да" +
                               "\nEnter - нет");

            if (logger.ReadMessage() != "1")
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
                logger.SendMessage("Введите номер интересующего вас заказа:");

                bool parced = int.TryParse(logger.ReadMessage(), out int orderNum);

                if (parced)
                {
                    fileName = $"{path}order_{orderNum}.json";
                    jsonString = File.ReadAllText(fileName);
                    List<ProductGenerator> order = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);

                    basket.SellSortedCategoryList(order, order.Count);

                    logger.SendMessage("\nХотите посмотреть еще 1 заказ?" +
                                       "\nДа - нажмите Enter" +
                                       "\nНет - нажмите 1\n");
                    switch (logger.ReadMessage())
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
                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                    logger.SendMessage("Такой заказ не найден.");
                }
            }
            while (!exit);
        }

        public void EditOrder(List<int> OrderNumbers)
        {
            logger.SendMessage("Найдены следующие заказы:");
            EnumerateOrders(OrderNumbers);

            logger.SendMessage("Введите номер заказа, который хотите изменить:");

            bool parced = int.TryParse(logger.ReadMessage(), out int input);

            if (parced)
            {
                logger.SendMessage("\nЧто сделать с заказом?" +
                                   "\n1 - добавить товар" +
                                   "\n2 - удалить товар," +
                                   "\n3 - удалить заказ.");

                switch (logger.ReadMessage())
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
                        logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                        break;
                }
            }
            else
                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
        }

        void AddProductToOrder(int orderNum)
        {
            bool exit = true;
            List<ProductGenerator> order = new List<ProductGenerator>();

            do
            {
                if (File.Exists(path + "order_" + orderNum.ToString() + ".json"))
                {
                    fileName = $"{path}order_{orderNum}.json";
                    jsonString = File.ReadAllText(fileName);
                    order = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);
                    DeleteOrder(orderNum);
                }
                else
                {
                    logger.SendMessage($"order_{orderNum.ToString()} не существует.");
                    logger.SendMessage("\nПопробовать еще раз?" +
                                       "\n1 - да" +
                                       "\nEnter - нет");
                    if (logger.ReadMessage() == "1")
                        exit = false;
                }
            }
            while (!exit);

            AddProductToBasket(order);
            SaveOrder(order);
        }

        void DeleteProductFromOrder(int orderNum)
        {
            bool exit = true;
            List<ProductGenerator> order = new List<ProductGenerator>();

            do
            {
                if (File.Exists(path + "order_" + orderNum.ToString() + ".json"))
                {
                    fileName = $"{path}order_{orderNum}.json";
                    jsonString = File.ReadAllText(fileName);
                    order = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);
                    DeleteOrder(orderNum);
                }
                else
                {
                    logger.SendMessage($"order_{orderNum.ToString()} не существует.");
                    logger.SendMessage("\nПопробовать еще раз?" +
                                       "\n1 - да" +
                                       "\nEnter - нет");
                    if (logger.ReadMessage() == "1")
                        exit = false;
                }
            }
            while (!exit);

            logger.SendMessage("Введите артикул товара, который хотите удалить:");
            basket.SellSortedCategoryList(basket.GetSortedProductList(order, 1), order.Count);
            
            string removeProductArticle = logger.ReadMessage();
            
            foreach (ProductGenerator product in order)
            {
                if (product.Article == removeProductArticle)
                {
                    order.Remove(product);
                    break;
                }
            }
            SaveOrder(order);
        }

        void DeleteOrder(int orderNum)
        {
            if (File.Exists(path + "order_" + orderNum.ToString() + ".json"))
            {
                logger.SendMessage($"order_{orderNum.ToString()} удален.");
                File.Delete($"{path}order_{orderNum.ToString()}.json");
            }
            else
                logger.SendMessage($"order_{orderNum.ToString()} не существует.");
        }

        void EnumerateOrders(List<int> OrderNumbers)
        {
            foreach (int j in OrderNumbers)
            {
                logger.SendMessage($"Order_{j}");
            }
        }

        public List<int> SeachForOrders()
        {
            List<int> OrderNumbers = new List<int>();

            for (int i = 0; i < 100; i++)
            {
                if (File.Exists(path + "order_" + i.ToString() + ".json"))
                {
                    OrderNumbers.Add(i);
                }
            }

            return OrderNumbers;
        }
    }
}
