using System;
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

        List<ProductGenerator> product = new List<ProductGenerator>();
        List<ProductGenerator> usersBasket = new List<ProductGenerator>();
        

        Random random = new Random();

        int OrderNumber { get; set; } = 1;

        bool exit = false;
        int sortingPattern = 1;
        int productsOnScreenNumber = 3;

        string fileName;
        string jsonString;

        //Это, конечно, костыльное решение, но оно необходимо, чтобы проверять, если ли в папке файл заказа с определенным номером. 
        //Я хочу использовать это для того, чтобы не приходилось угадывать, какие файлы уже созданы, а читать из списка существующих, и чтобы вновь создаваемые файлы не перезаписывали старые. 
        public static string path = @"C:\Users\alexg\OneDrive\Рабочий стол\GaidukovPSBstudyCalculator-08b48238f18b7f9e03efc951a4fc3d611d2aca50\GaidukovPSBstudyCalculator\GaidukovPSBstudyBasket\Orders\";

        public void CreateUserOrder()
        {
            do
            {

                do
                {
                    do
                    {
                        logger.SendMessage(LogMessage.ShopCategoryMessage);
                        bool parced = char.TryParse(logger.ReadMessage(), out char categoryNumber);

                        if (parced)
                        {
                            logger.SendMessage("\nСколько товаров категории отобразить?\n");
                            parced = int.TryParse(logger.ReadMessage(), out int productsOnScreen);

                            if (!parced)
                            {
                                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                                logger.SendMessage("Выбрано значение по умолчанию: 3.");
                            }
                            else
                                productsOnScreenNumber = productsOnScreen;

                            product.Clear();

                            switch (categoryNumber)
                            {
                                case '1':

                                    fileName = "WashingMachines.json";
                                    jsonString = File.ReadAllText(fileName);
                                    product = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);

                                    exit = true;
                                    break;

                                case '2':

                                    fileName = "Fans.json";
                                    jsonString = File.ReadAllText(fileName);
                                    product = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);

                                    exit = true;
                                    break;

                                case '3':

                                    fileName = "Fans.json";
                                    jsonString = File.ReadAllText(fileName);
                                    product = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);

                                    exit = true;
                                    break;

                                default:
                                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                                    break;
                            }
                        }
                        else
                            logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                    }
                    while (!exit);

                    exit = false;

                    basket.SellSortedCategoryList(basket.GetSortedProductList(product, basket.GetSortingParametr()), productsOnScreenNumber);

                    logger.SendMessage("\nВведите артикул желаемого товара.\n");

                    basket.GetUsersBasket(product, logger.ReadMessage());

                    logger.SendMessage("\nЖелаете продолжть покупки?" +
                                       "\nДля продолжения нажмите Enter клавишу." +
                                       "\nДля завершения нажмите 1.\n");

                    if (logger.ReadMessage() == "1")
                        exit = true;
                }
                while (!exit);

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

        public void CreateRandomOrder()
        {
            List<ProductGenerator> Order = new List<ProductGenerator>();
            int orderCount = 1;

            logger.SendMessage("\nСколько заказов вы желаете создать?");
            bool parced = int.TryParse(logger.ReadMessage(), out int input);

            if (parced)
                orderCount = input;
            else
                logger.SendMessage($"Выбранно значение по умолчанию: {orderCount}");

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

        public void SaveOrder(List<ProductGenerator> UsersBasket)
        {
            
            while (File.Exists(path + "order_" + OrderNumber.ToString() + ".json"))
            {
                logger.SendMessage("Заказ с таким номером уже существует");
                OrderNumber++;
            }

            generator.SerializeOrder(UsersBasket, OrderNumber);

            logger.SendMessage($"Заказ Order_{OrderNumber} успешно сохранен.");

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
            logger.SendMessage("Найдены следующие заказы:");

            foreach (int j in OrderNumbers)
            {
                logger.SendMessage($"Order_{j}");
            }

            do
            {
                logger.SendMessage("Введите номер интересующего вас заказа:");

                fileName = $"{path}order_{logger.ReadMessage()}.json";
                jsonString = File.ReadAllText(fileName);
                List<ProductGenerator> order = JsonSerializer.Deserialize<List<ProductGenerator>>(jsonString);

                basket.SellSortedCategoryList(order, order.Count);
                
                logger.SendMessage("\nХотите посмотреть еще 1 заказ?" +
                                   "\nДа - нажмите Enter" +
                                   "\nНет - нажмите 1.\n");

                if (logger.ReadMessage() == "1")
                    exit = true;
            }
            while (!exit);
        }

        public void EditOrder(List<int> OrderNumbers)
        {
            logger.SendMessage("Найдены следующие заказы:");

            foreach (int j in OrderNumbers)
            {
                logger.SendMessage($"Order_{j}");
            }

            //Придумать, что впринципе должен делать метод редактирования заказа. 
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
