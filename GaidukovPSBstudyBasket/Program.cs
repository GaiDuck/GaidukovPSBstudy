﻿using GaidukovPSBstudyBasket;
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
        ProductGenerator generator = new ProductGenerator();
        MainFunctions main = new MainFunctions();

        bool exit;

        generator.GetShopAssortment();
        generator.SerializeShopAssortment();

        do
        {
            logger.SendMessage(LogMessage.GreetingMassegeForShopMessage);
            logger.SendMessage("\nВыберите режим:" +
                               "\n1 - Создать новый заказ вручную;" +
                               "\n2 - Создать случайно сгенерированный заказ;" +
                               "\n3 - Создать случайно сгенерированнный по пожеланиям клиента заказ" +
                               "\n4 - Прочитать заказ;" +
                               "\n5 - Редактировать заказ.");


            switch (logger.ReadMessage())
            {
                case "1":
                    main.CreateUserOrder(); //Заказ, создаваемый пользователем вручную.
                    break;

                case "2":
                    main.CreateRandomOrder();
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
                        main.CreateRandomOrder();
                    else
                        main.CreateRandomOrder(mod);
                    break;

                case "4":
                    main.ReadOrder(main.SeachForOrders());
                    break;

                case "5":
                    main.EditOrder(main.SeachForOrders());
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
