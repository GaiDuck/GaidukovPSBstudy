using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class BasketConvertor
    {
        ProductGenerator prod = new ProductGenerator(); // если статика - убрать вообще, если динамика - прокидывать через конструктор
        ConsoleLogger logger = new ConsoleLogger();//то же, что с остальными логгерами

        List<string> category = new List<string>();
        public List<ProductGenerator> UsersBasket = new List<ProductGenerator>();

        /// <summary>
        /// метод принимает список продуктов одной категории и параметр, покотому сортируется товар, затем сортирует товары в соответствии с параметром. 
        /// </summary>
        /// <param name="Produckts"></param>
        public List<ProductGenerator> GetSortedProductList(List<ProductGenerator> Produckts, int sortingPattern)
        {
            List <ProductGenerator> product = new List <ProductGenerator>();
            
            switch (sortingPattern)
            {
                case 1:
                    product = Produckts.OrderBy(s => s.Article).ToList();
                    break;

                case 2:
                    product = Produckts.OrderBy(s => s.Cost).ToList();
                    break;

                case 3:
                    product = Produckts.OrderByDescending(s => s.Score).ToList();
                    break;

                case 4:
                    product = Produckts.OrderBy(s => s.Weight).ToList();
                    break;

                case 5:
                    product = Produckts.OrderBy(s => s.DeliveryDays).ToList();
                    break;
            }
            return product;
        }

        /// <summary>
        /// Метод выписывает список товаров с характеристиками из категории в соответствии с отсортированным списком артикулов. 
        /// </summary>
        public void SellSortedCategoryList(List<ProductGenerator> product, int productsOnScreenNumber)
        {
            for (int i = 0; i < productsOnScreenNumber; i++)
            {
                logger.SendMessage($"\nАртикул: {product[i].Article} \nТип товара: {product[i].ProductType} \nЦена: {Math.Round(product[i].Cost, 2)} \nОценка: {Math.Round(product[i].Score, 2)} \nВес: {Math.Round(product[i].Weight, 1)} " +
                                   $"\nДней до доставки: {product[i].DeliveryDays} \n{prod.GetSpecialFeatureByType(product[i].ProductType)}: {product[i].SpecialFeature}");
            }
        }

        /// <summary>
        /// Метод принимает в себя список категории и артикул требуемого товара, после чего добавляет требуемый товар в коллекцию UserBasket.
        /// </summary>
        /// <param name="Produckts"></param>
        /// <param name="userInput"></param>
        public void GetBasket(List<ProductGenerator> Produckts, List<ProductGenerator> Basket, string userInput)
        {
            int BasketCount = Basket.Count;

            foreach (ProductGenerator product in Produckts)
            {
                if (product.Article == userInput)
                {
                    Basket.Add(product);
                    logger.SendMessage("\nТовар успешно добавлен в корзину.\n");
                }
            }

            if (Basket.Count <= BasketCount)
            {
                logger.SendMessage("\nТакого товара нет.\n");
            }
        }

        /// <summary>
        /// Метод предлагает пользователю выбрать параметр, по которому будет происходить сортировка и возвращает его номер. 
        /// </summary>
        public int GetSortingParametr()
        {
            logger.SendMessage("\nОпределите параметр сортировки:\n" +
                               "1 - сортировка по названию,\n" +
                               "2 - сортировка по стоимости,\n" +
                               "3 - сортировка по оценке,\n" +
                               "4 - сортировка по весу,\n" +
                               "5 - сортировка по времени доставки.\n");

            bool parced = int.TryParse(logger.ReadMessage(), out int sortingPatternNumber);

            if (!parced)
            {
                logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                logger.SendMessage("Выбрано значение по умолчанию: сортировка по названию.");
                sortingPatternNumber = 1;
            }
            return sortingPatternNumber;
        }
    }
}
