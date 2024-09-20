using GaidukovPSBstudyBasket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class ProductGenerator
    {
        Random random = new Random();

        public string Article { get; set; }
        public string ProductType { get; set; }
        public double Cost { get; set; }
        public double Score { get; set; }
        public double Weight { get; set; }
        public int DeliveryDays { get; set; }
        public string SpecialFeature { get; set; }
        public static int NumberOfGeneratedProducts { get; set; } = 10;

        List<ProductGenerator> WashingMachines = new List<ProductGenerator>();
        List<ProductGenerator> Fans = new List<ProductGenerator>();
        List<ProductGenerator> Microwaves = new List<ProductGenerator>();

        List <string> UsedArticles = new List <string> ();

        public ProductGenerator GetRandomProduct(type Type)
        {
            ProductGenerator generatedProduct = new ProductGenerator ();

            generatedProduct.Article = GetRandomArticle();
            generatedProduct.ProductType = GetRandomTitleByType(Type);
            generatedProduct.Cost = GetRandomCostByType(Type);
            generatedProduct.Score = GetRandomScore();
            generatedProduct.Weight = GetRandomWeight(Type);
            generatedProduct.DeliveryDays = GetRandomDeliveryDays(); 
            generatedProduct.SpecialFeature = GetRandomSpecialFeature();

            return generatedProduct;
        }
        
        /// <summary>
        /// Метод создает случайный артикул, состоящий из 3х букв и 4х цифр, затем проверяет артикул на уникальность, 
        /// записывает сгенеренный артикул в список артикулов и возвращает его.
        /// </summary>
        /// <returns></returns>
        string GetRandomArticle()
        {
            bool stop = false;
            string s;
            char[] chars = new char[7];
            int j = 4;

            do
            {
                for (int i = 0; i < chars.Length - j; i++)
                {
                    chars[i] = Convert.ToChar(random.Next(65, 91));
                }

                for (int i = chars.Length - j; i < chars.Length; i++)
                {
                    chars[i] = Convert.ToChar(random.Next(48, 58));
                }

                s = new string(chars);

                if (!UsedArticles.Contains(s))
                {
                    UsedArticles.Add(s);
                    stop = true;
                }
            } 
            while (!stop);

            return s;
        }

        /// <summary>
        /// Метод возвращает случайный тип товара.
        /// </summary>
        /// <returns></returns>
        public type GetRandomType()
        {
            int t = random.Next(1, 4);
            return t switch
            {
                1 => type.washingMachine,
                2 => type.fan,
                3 => type.microwave
            };
        }

        string GetRandomTitleByType(type Type)
        {
            return Type switch
            {
                type.washingMachine => "Стиральная машина",
                type.fan => "Фен",
                type.microwave => "Микроволновая печь"
            };
        }

        /// <summary>
        /// Метод возвращает случайную стоимость товара, в зависимости от типа.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        double GetRandomCostByType(type Type)
        {
            return Type switch
            {
                type.washingMachine => (1 + random.NextDouble())*10000,
                type.fan => (1 + random.NextDouble())*500,
                type.microwave => (1 + random.NextDouble())*3000
            };
        }

        /// <summary>
        /// Метод возвращает случайную оценку товара.
        /// </summary>
        /// <returns></returns>
        double GetRandomScore()
        {
            return random.Next(0, 5) + random.NextDouble();
        }

        /// <summary>
        /// Метод возвращает случайный вес товара в зависимости от типа товара.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        double GetRandomWeight(type Type)
        {
            return Type switch
            {
                type.washingMachine => random.Next(30, 61) ,
                type.fan => 0.1 + random.NextDouble(),
                type.microwave => random.Next(3, 5) + random.NextDouble()
            };
        }

        /// <summary>
        /// Метод возвращает случайное число дней до доставки товара.
        /// </summary>
        /// <returns></returns>
        int GetRandomDeliveryDays()
        {
            return random.Next(0, 10);
        }

        /// <summary>
        /// Метод возвращает выбранное случайным образом "yes" или "no".
        /// </summary>
        /// <returns></returns>
        string GetRandomSpecialFeature()
        {
            if (random.Next(0, 2) == 0)
                return "yes";
            else 
                return "no";
        }

        /// <summary>
        /// Метод принимает тип продукта и возвращает название специальной функции заданного типа продукта. 
        /// </summary>
        /// <param name="ProductType"></param>
        /// <returns></returns>
        public string GetSpecialFeatureByType(string ProductType)
        {
            string feature = null; 

            switch(ProductType)
            {
                case "Стиральная машина":
                    feature = "Сушилка";
                    break;

                case "Фен":
                    feature = "Турборежим";
                    break; 

                case "Микроволновая печь":
                    feature = "Режим разморозки";
                    break; 

                default: 
                    feature = "Какая-то ерунда";
                    break;
            };

            return feature;
        }

        /// <summary>
        /// Метод генерирует по 10 случайных продуктов каждой категории и записывает их в списки по категориям.
        /// </summary>
        public void GetShopAssortment()
        {
            WashingMachines.Clear();
            Fans.Clear();
            Microwaves.Clear();

            for (int i = 0; i<NumberOfGeneratedProducts; i++)
            {
                WashingMachines.Add(GetRandomProduct(type.washingMachine));
                Fans.Add(GetRandomProduct(type.fan));
                Microwaves.Add(GetRandomProduct(type.microwave));
            }
        }

        public void SerializeShopAssortment()
        {
            SerializeWashingMachines();
            SerializeFans();
            SerializeMicrowaves();
        }

        void SerializeWashingMachines()
        {
            string fileName = "WashingMachines.json";
            string jsonString = JsonSerializer.Serialize(WashingMachines);
            File.WriteAllText(fileName, jsonString);            
        }

        void SerializeFans()
        {
            string fileName = "Fans.json";
            string jsonString = JsonSerializer.Serialize(Fans);
            File.WriteAllText(fileName, jsonString);            
        }

        void SerializeMicrowaves()
        {
            string fileName = "Microwaves.json";
            string jsonString = JsonSerializer.Serialize(Microwaves);
            File.WriteAllText(fileName, jsonString);            
        }

        public void SerializeOrder(List<ProductGenerator> Basket, int orderNumber)
        {
            string fileName = OrderGenerator.path + "order_" + orderNumber.ToString() + ".json";
            string jsonString = JsonSerializer.Serialize(Basket);
            File.WriteAllText(fileName, jsonString);            
        }
    }
}