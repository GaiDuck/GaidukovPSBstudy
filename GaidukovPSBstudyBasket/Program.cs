using GaidukovPSBstudyBasket;
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

        generator.GetShopAssortment();
        generator.SerializeShopAssortment();

        logger.SendMessage(LogMessage.GreetingMassegeForShopMessage);
        logger.SendMessage("\nВыберите режим:" +
                           "\n1 - Создать новый заказ вручную;" +
                           "\n2 - Создать случайно сгенерированный заказ;" +
                           "\n3 - Прочитать заказ.");


        switch (logger.ReadMessage())
        {
            case "1":
                main.CreateUserOrder(); //Заказ, создаваемый пользователем вручную.
                break;
            case "2":
                main.CreateRandomOrder();
                break;
            case "3":
                main.ReadOrder(main.SeachForOrders());
                break;
            case "4":
                main.EditOrder(main.SeachForOrders());
                break;
            default:
                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                break;
        }
    }
}
