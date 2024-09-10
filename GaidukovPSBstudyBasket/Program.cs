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
        ShopBasketGenerator gen = new ShopBasketGenerator();

        List<ProductGenerator> product = null;
        bool exit = false;

        List<ProductGenerator> washingMachines = new List<ProductGenerator>() { ProductWashingMachine.Bosh, ProductWashingMachine.Ural, ProductWashingMachine.Toshiba };
        List<ProductGenerator> microwaves = new List<ProductGenerator>() { ProductMicrowave.Vitek, ProductMicrowave.Brizz, ProductMicrowave.Liama };
        List<ProductGenerator> fans = new List<ProductGenerator>() { ProductFan.Dyson, ProductFan.Veterok, ProductFan.Tuvio };

        logger.SendMessage(LogMessage.GreetingMassegeForShopMessage);
        do
        { 
            do
            {
                logger.SendMessage(LogMessage.ShopCategoryMessage);
                bool parced = char.TryParse(logger.ReadMessage(), out char categoryNumber);

                if (parced)
                {
                    switch (categoryNumber)
                    {
                        case '1':
                            product = washingMachines;
                            exit = true;
                            break;

                        case '2':
                            product = fans;
                            exit = true;
                            break;

                        case '3':
                            product = microwaves;
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

            gen.GetSortedCategoryList(product);
            gen.SellSortedCategoryList(product);

            logger.SendMessage("\nВведите артикул желаемого товара.\n");

            gen.GetUsersBasket(product, logger.ReadMessage());

            logger.SendMessage("\nЖелаете продолжть покупки?" +
                               "\nДля продолжения нажмите Enter клавишу." +
                               "\nДля завершения нажмите 1.\n");
            
            if(logger.ReadMessage() == "1")
                exit = true;
        }
        while (!exit);

        logger.SendMessage("\nКорзина:");
        gen.WriteUsersBasket();
    }
}