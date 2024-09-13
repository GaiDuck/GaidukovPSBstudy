using GaidukovPSBstudyBasket;
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
        BasketConvertor basket = new BasketConvertor();
        ProductGenerator generator = new ProductGenerator();

        List<ProductGenerator> product = new List<ProductGenerator>();
        List<ProductGenerator> usersBasket = new List<ProductGenerator>();

        bool exit = false;
        int sortingPattern = 1;
        int productsOnScreenNumber = 3;

        string fileName;
        string jsonString;

        generator.GetShopAssortment();
        generator.SerializeShopAssortment();

        logger.SendMessage(LogMessage.GreetingMassegeForShopMessage);
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

                    sortingPattern = basket.GetSortingParametr();

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
            while(!exit);

            exit = false;

            basket.SellSortedCategoryList(basket.GetSortedProductList(product, sortingPattern), productsOnScreenNumber);

            logger.SendMessage("\nВведите артикул желаемого товара.\n");

            basket.GetUsersBasket(product, logger.ReadMessage());

            logger.SendMessage("\nЖелаете продолжть покупки?" +
                               "\nДля продолжения нажмите Enter клавишу." +
                               "\nДля завершения нажмите 1.\n");
            
            if(logger.ReadMessage() == "1")
                exit = true;
        }
        while (!exit);

        logger.SendMessage("\nКорзина:");
        basket.SellSortedCategoryList(basket.GetSortedProductList(basket.UsersBasket, basket.GetSortingParametr()), basket.UsersBasket.Count());
    }
}