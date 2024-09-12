using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class ShopBasketGenerator
    {
        ProductGenerator prod = new ProductGenerator();
        ConsoleLogger logger = new ConsoleLogger();

        List<string> category = new List<string>();
        List<ProductGenerator> UsersBasket = new List<ProductGenerator>();

        /// <summary>
        /// метод принимает список продуктов одной категории, записывает артиклы всех продуктов и сортирует список артикулов. 
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
                    product = Produckts.OrderBy(s => s.Score).ToList();
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
        public void SellSortedCategoryList(List<ProductGenerator> product)
        {
            foreach (ProductGenerator p in product)
            {
                logger.SendMessage($"\nАртикул: {p.Article} \nТип товара: {p.ProductType} \nЦена: {Math.Round(p.Cost, 2)} \nОценка: {Math.Round(p.Score, 2)} \nВес: {Math.Round(p.Weight, 1)} " +
                                   $"\nДней до доставки: {p.DeliveryDays} \n{prod.SpecialFeatureByType(p.ProductType)}: {p.SpecialFeature}");
            }
        }

        /// <summary>
        /// Метод принимает в себя список категории и артикул требуемого товара, после чего добавляет требуемый товар в коллекцию UserBasket.
        /// </summary>
        /// <param name="Produckts"></param>
        /// <param name="userInput"></param>
        public void GetUsersBasket(List<ProductGenerator> Produckts, string userInput)
        {
            int userBasketCount = UsersBasket.Count();

            foreach (ProductGenerator product in Produckts)
            {
                if (product.Article == userInput)
                {
                    UsersBasket.Add(product);
                    logger.SendMessage("\nТовар успешно добавлен в корзину.\n");
                }
            }

            if (UsersBasket.Count - userBasketCount <= 0)
            {
                logger.SendMessage("\nТакого товара нет.\n");
            }
        }

        /// <summary>
        /// Метод выписывает в консоль все товары, взятые пользователм в корзину. 
        /// </summary>
        public void WriteUsersBasket()
        {
            foreach (ProductGenerator product in UsersBasket)
            {
                    logger.SendMessage($"Артикул: {product.Article} \nТип товара: {product.ProductType} \nЦена: {product.Cost} \nОценка: {product.Score} \nВес: {product.Weight} " +
                                       $"\nДней до доставки: {product.DeliveryDays} \n{prod.SpecialFeatureByType(product.ProductType)}: {product.SpecialFeature}");
            }
        }
    }
}
