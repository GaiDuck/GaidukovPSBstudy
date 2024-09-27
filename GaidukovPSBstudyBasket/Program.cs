using GaidukovPSBstudyBasket;
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
/*
        ProductFan fan = new ProductFan("Серп и молот", 150, 3.8, 15.1, 25, "есть, ещё какой");
        var pg = (ProductGenerator)fan;
        pg.GetRandomProduct(type.fan);
*/

        ConsoleLogger logger = new ConsoleLogger(); //убрать дубли классов Logger, ConsoleLogger и привести всё к одному классу и интерфейсу ILogger
        ProductsGenerator generator = new ProductsGenerator();
        OrderGenerator order = new OrderGenerator();
        OrderCardsGenerator orderCards = new OrderCardsGenerator();

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
                               "\n6 - Вывести часть заказов (LINQ).");


            switch (logger.ReadMessage())
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

                    switch (logger.ReadMessage())
                    {
                        case "1":
                            mod = "cheap";
                            break;
                        case "2":
                            mod = "qualitative";
;                            break;

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
                    orderCards.GetOrderCardsList();
                    logger.SendMessage("\nПо какому критерию вывести заказы?" +
                                       "\n1 - дешевле заданной стоимости," +
                                       "\n2 - дороже заданной стоимости," +
                                       "\n3 - отсортированные по весу," +
                                       "\n4 - по времени доставки.");
/*                    switch (logger.ReadMessage())
                    {
                        case "1":
                            logger.SendMessage("Введите максимальную стоимость: ");
                            orderCards.GetOrdersChiapperThan(double.Parse(logger.ReadMessage));
                            break;

                        case "2":
                            logger.SendMessage("Введите минимальную стоимость: ");
                            orderCards.GetOrdersMoreExpensiveThan(double.Parse(logger.ReadMessage));
                            break;

                        case "3":
                            orderCards.GetOrdersSortedByWeight();
                            break;

                        case "4":
                            logger.SendMessage("Введите максимальное время доставки: ");
                            orderCards.GetOrdersByDeliveringDate(int.Parse(logger.ReadMessage));
                            break;

                        default:
                            break;
                    }*/
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
