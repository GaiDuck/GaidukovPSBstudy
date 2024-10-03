using GaidukovPSBstudyBasket.Generator;
using GaidukovPSBstudyBasket.Models;
using GaidukovPSBstudyCalculator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class BasketConvertor
    {
        ProductsGenerator generator = new ProductsGenerator();

        List<string> category = new List<string>();
        public List<ProductsModel> UsersBasket = new List<ProductsModel>();

        ILogger Logger { get; set; }

        internal BasketConvertor() : this(new ConsoleLogger())
        {

        }

        public BasketConvertor(ILogger logger)
        {
            Logger = logger;
        }


        /// <summary>
        /// метод принимает список продуктов одной категории и параметр, покотому сортируется товар, затем сортирует товары в соответствии с параметром. 
        /// </summary>
        /// <param name="Produckts"></param>
        public List<ProductsModel> GetSortedProductList(List<ProductsModel> Produckts, int sortingPattern) //сделать sortingParametr enum-ом 
        {
            List <ProductsModel> product = new List <ProductsModel>();
            
            switch (sortingPattern)
            {
                case 1:
                    product = BubbleSorting(Produckts);
                    //product = Produckts.OrderBy(s => s.Article).ToList();
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

                default:
                    product = Produckts.OrderBy(s => s.Article).ToList();
                    break;

            }
            return product;
        }

        List<ProductsModel> BubbleSorting(List<ProductsModel> Produckts)
        {
            for (int i = 0; i < Produckts.Count; i++)
            {
                for (int j = 0; j < Produckts.Count; j++)
                {
                    if (string.Compare(Produckts[i].Article, Produckts[j].Article) < 0)
                    {
                        var temp = Produckts[j];
                        Produckts[j] = Produckts[i];
                        Produckts[i] = temp;
                    }
                }
            }
            return Produckts;
        }

        /// <summary>
        /// Метод выписывает список товаров с характеристиками из категории в соответствии с отсортированным списком артикулов. 
        /// </summary>
        public void SellSortedCategoryList(List<ProductsModel> product, int productsOnScreenNumber)
        {
            for (int i = 0; i < productsOnScreenNumber; i++)
            {
                Logger.SendMessage($"\nАртикул: {product[i].Article} \nТип товара: {product[i].ProductType} \nЦена: {Math.Round(product[i].Cost, 2)} \nОценка: {Math.Round(product[i].Score, 2)} \nВес: {Math.Round(product[i].Weight, 1)} " +
                                   $"\nДней до доставки: {product[i].DeliveryDays} \n{generator.GetSpecialFeatureByType(product[i].ProductType)}: {product[i].SpecialFeature}");
            }
        }

        /// <summary>
        /// Метод принимает в себя список категории и артикул требуемого товара, после чего добавляет требуемый товар в коллекцию UserBasket.
        /// </summary>
        /// <param name="Produckts"></param>
        /// <param name="userInput"></param>
        public void GetBasket(List<ProductsModel> Produckts, List<ProductsModel> Basket, string userInput)
        {
            if (userInput != "")
            {
                int BasketCount = Basket.Count;

                foreach (ProductsModel product in Produckts)
                {
                    if (product.Article == userInput)
                    {
                        Basket.Add(product);
                        Logger.SendMessage("\nТовар успешно добавлен в корзину.\n");
                    }
                }

                if (Basket.Count <= BasketCount)
                {
                    Logger.SendMessage("\nТакого товара нет.\n");
                }
            }
        }

        /// <summary>
        /// Метод предлагает пользователю выбрать параметр, по которому будет происходить сортировка и возвращает его номер. 
        /// </summary>
        public int GetSortingParametr()
        {
            Logger.SendMessage("\nОпределите параметр сортировки:\n" +
                               "1 - сортировка по названию,\n" +
                               "2 - сортировка по стоимости,\n" +
                               "3 - сортировка по оценке,\n" +
                               "4 - сортировка по весу,\n" +
                               "5 - сортировка по времени доставки.\n");

            bool parced = int.TryParse(Logger.ReadMessage(), out int sortingPatternNumber);

            if (!parced || sortingPatternNumber > 5)
            {
                Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                Logger.SendMessage("Выбрано значение по умолчанию: сортировка по названию.");
                sortingPatternNumber = 1;
            }
            return sortingPatternNumber;
        }
        
        public int GetSortingParametr(string str)
        {
            Logger.SendMessage("\nОпределите параметр сортировки:\n" +
                               "1 - сортировка по названию,\n" +
                               "2 - сортировка по стоимости,\n" +
                               "3 - сортировка по оценке,\n" +
                               "4 - сортировка по весу,\n" +
                               "5 - сортировка по времени доставки.\n");

            bool parced = int.TryParse(Logger.ReadMessage(str), out int sortingPatternNumber);

            if (!parced || sortingPatternNumber > 5)
            {
/*
                Logger.SendMessage(LogMessage.EnterIncorrectDataMessage);
                Logger.SendMessage("Выбрано значение по умолчанию: сортировка по названию.");
*/
                sortingPatternNumber = 1;
            }
            return sortingPatternNumber;
        }
    }
}