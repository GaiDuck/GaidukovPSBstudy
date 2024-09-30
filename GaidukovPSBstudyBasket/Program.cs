using GaidukovPSBstudyBasket.Generator;
using GaidukovPSBstudyBasket.Models;
using GaidukovPSBstudyCalculator;
using System.Reflection.Emit;
using System.Text.Json;


enum type
{
    washingMachine,
    microwave,
    fan
}

internal class Program
{
    private static void Main(string[] args)
    {
        ConsoleLogger logger = new ConsoleLogger(); 
        ProductsGenerator generator = new ProductsGenerator();
        OrderGenerator order = new OrderGenerator();
        OrderCardsGenerator orderCards = new OrderCardsGenerator();

        List <OrderCardModel> OutputOrdersList = new List <OrderCardModel>();

        bool exit;

        generator.GetShopAssortment();
        generator.SerializeShopAssortment();

        do
        {
            logger.SendMessage("Добро пожаловать в наш магазин бытовой техники!\n" +
                               "У нас вы найдете самые лучшие товары: посудомоечные машины, фены и микроволновые печи.\n");
                               
            logger.SendMessage("\nВыберите режим:" +
                               "\n1 - Создать новый заказ вручную;" +
                               "\n2 - Создать случайно сгенерированный заказ;" +
                               "\n3 - Создать случайно сгенерированнный по пожеланиям клиента заказ" +
                               "\n4 - Прочитать заказ;" +
                               "\n5 - Редактировать заказ," +
                               "\n6 - Вывести часть заказов (LINQ).\n");


            switch (logger.ReadDigitsOnly())
            {
                case "1":
                    order.CreateUserOrder(); //Заказ, создаваемый пользователем вручную.
                    break;

                case "2":
                    order.CreateRandomOrder();
                    break;

                case "3":
                    string mod = null;
                    logger.SendMessage("\nКакие товары выбирать?" +
                                       "\n1 - Самые дешевые;" +
                                       "\n2 - С самой высокой оценкой;" +
                                       "\n3 - С самой быстрой доставкой.");

                    switch (logger.ReadDigitsOnly())
                    {
                        case "1":
                            mod = "cheap";
                            break;
                        case "2":
                            mod = "qualitative";
                            ; break;

                        case "3":
                            mod = "fast delivery";
                            break;

                        default:
                            logger.SendMessage("Выбран режим по-умолчанию: создание случайного заказа.");
                            break;
                    }

                    if (mod == null)
                        order.CreateRandomOrder();
                    else
                        order.CreateRandomOrder(mod);
                    break;

                case "4":
                    order.ReadOrder(order.SeachForOrders());
                    break;

                case "5":
                    order.EditOrder(order.SeachForOrders());
                    break;

                case "6":
                    do
                    {
                        exit = false;

                        orderCards.GetOrderCardsList();

                        OutputOrdersList.Clear();

                        logger.SendMessage("\nПо какому критерию вывести заказы?" +
                                           "\n1 - дешевле заданной стоимости," +
                                           "\n2 - дороже заданной стоимости," +
                                           "\n3 - отсортированные по весу," +
                                           "\n4 - по времени доставки.");

                        switch (logger.ReadDigitsOnly())
                        {
                            case "1":
                                logger.SendMessage("Введите максимальную стоимость: ");
                                OutputOrdersList.AddRange(
                                    orderCards.GetOrdersChiapperThan(double.Parse(logger.ReadDigitsOnly())));
                                exit = true;
                                break;

                            case "2":
                                logger.SendMessage("Введите минимальную стоимость: ");
                                OutputOrdersList.AddRange(
                                    orderCards.GetOrdersMoreExpensiveThan(double.Parse(logger.ReadDigitsOnly())));
                                exit = true;
                                break;

                            case "3":
                                OutputOrdersList.AddRange(
                                    orderCards.GetOrdersSortedByWeight());
                                exit = true;
                                break;

                            case "4":
                                logger.SendMessage("Введите максимальное время доставки: ");
                                OutputOrdersList.AddRange(
                                    orderCards.GetOrdersByDeliveringDate(int.Parse(logger.ReadDigitsOnly())));
                                exit = true;
                                break;

                            default:
                                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                                logger.SendMessage("\nЖелаете попробовать повторно?" +
                                                   "\n1 - да" +
                                                   "\nEnter - нет\n");

                                if(logger.ReadMessage() != "1")
                                    exit = true;
                                break;
                            }

                    } 
                    while (!exit);
                    
                    foreach (var orderCard in OutputOrdersList)
                    {
                        logger.SendMessage($"{orderCard.Article}");
                    }

                    break;

                default:
                    logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                    break;
            }

            logger.SendMessage("\nЗавершить работу?" +
                               "\nДа - нажмите 1" +
                               "\nНет - нажмите Enter\n");
            
            if (logger.ReadMessage() == "1")
                exit = true;
            else
                exit = false;
        }
        while (!exit);
    }
}
