using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void GetSortedCategoryList(List<ProductGenerator> Produckts)
        {
            foreach (ProductGenerator product in Produckts)
            {
                category.Add(product.Article); //Как сделать сортировку не только по артиклу?
            }

            category.Sort();
        }

        /// <summary>
        /// Метод выписывает список товаров с характеристиками из категории в соответствии с отсортированным списком артикулов. 
        /// </summary>
        public void SellSortedCategoryList(List<ProductGenerator> Produckts)
        {
            for (int i = 0; i < category.Count; i++)
            {
                foreach (ProductGenerator product in Produckts)
                {
                    if (product.Article == category[i])
                    {
                        logger.SendMessage($"\nАртикул: {product.Article} \nТип товара: {product.ProductType} \nЦена: {product.Cost} \nОценка: {product.Score} \nВес: {product.Weight} " +
                                           $"\nДней до доставки: {product.DeliveryDays} \n{prod.SpecialFeatureByType(product.ProductType)}: {product.SpecialFeature}");
                    }
                }
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
