using GaidukovPSBstudyBasket;

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
        ShopBasketGenerator basket = new ShopBasketGenerator();
        ProductGenerator generator = new ProductGenerator();

        List<ProductGenerator> product = new List<ProductGenerator>();

        bool exit = false;
        int sortingPattern = 1;

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
                    parced = int.TryParse(logger.ReadMessage(), out int productsOnScreenNumber);

                    if (!parced)
                    {
                        logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                        logger.SendMessage("Выбрано значение по умолчанию: 3.");
                        productsOnScreenNumber = 3;
                    }

                    logger.SendMessage("\nОпределите параметр сортировки:\n" +
                                           "1 - сортировка по названию,\n" +
                                           "2 - сортировка по стоимости,\n" +
                                           "3 - сортировка по оценке,\n" +
                                           "4 - сортировка по весу,\n" +
                                           "5 - сортировка по времени доставки.\n");
                    parced = int.TryParse(logger.ReadMessage(), out int sortingPatternNumber);

                    if (!parced)
                    {
                        logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                        logger.SendMessage("Выбрано значение по умолчанию: сортировка по названию.");
                        sortingPatternNumber = 1;
                    }

                    sortingPattern = sortingPatternNumber;

                    switch (categoryNumber)
                    {
                        case '1':
                            for (int i = 0; i < productsOnScreenNumber; i++)
                            {
                                product.Add(generator.GenerateProduct(type.washingMachine));
                            }
                            exit = true;
                            break;

                        case '2':
                            for (int i = 0; i < productsOnScreenNumber; i++)
                            {
                                product.Add(generator.GenerateProduct(type.fan));
                            }
                            exit = true;
                            break;

                        case '3':
                            for (int i = 0; i < productsOnScreenNumber; i++)
                            {
                                product.Add(generator.GenerateProduct(type.microwave));
                            }
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

            basket.SellSortedCategoryList(basket.GetSortedProductList(product, sortingPattern));

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
        basket.WriteUsersBasket();
    }
}