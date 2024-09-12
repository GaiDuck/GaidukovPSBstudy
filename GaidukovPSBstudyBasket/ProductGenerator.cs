using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


        List <string> articles = new List <string> ();

        public ProductGenerator GenerateProduct(type Type)
        {
            ProductGenerator generatedProduct = new ProductGenerator ();

            generatedProduct.Article = GenerateArticle();
            generatedProduct.ProductType = ChooseType(Type);
            generatedProduct.Cost = GenerateCost(Type);
            generatedProduct.Score = GenerateScore();
            generatedProduct.Weight = GenerateWeight(Type);
            generatedProduct.DeliveryDays = GenerateDeliveryDays(); 
            generatedProduct.SpecialFeature = GenerateSpecialFeature();

            return generatedProduct;
        }
        
        /// <summary>
        /// Метод создает случайный артикул, состоящий из 3х букв и 4х цифр, затем проверяет артикул на уникальность, 
        /// записывает сгенеренный артикул в список артикулов и возвращает его.
        /// </summary>
        /// <returns></returns>
        string GenerateArticle()
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

                if (!articles.Contains(s))
                {
                    articles.Add(s);
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
        type GenerateType()
        {
            int t = random.Next(1, 4);
            return t switch
            {
                1 => type.washingMachine,
                2 => type.fan,
                3 => type.microwave
            };
        }

        string ChooseType(type Type)
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
        double GenerateCost(type Type)
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
        double GenerateScore()
        {
            return random.Next(0, 5) + random.NextDouble();
        }

        /// <summary>
        /// Метод возвращает случайный вес товара в зависимости от типа товара.
        /// </summary>
        /// <param name="Type"></param>
        /// <returns></returns>
        double GenerateWeight(type Type)
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
        int GenerateDeliveryDays()
        {
            return random.Next(0, 10);
        }

        /// <summary>
        /// Метод возвращает выбранное случайным образом "yes" или "no".
        /// </summary>
        /// <returns></returns>
        string GenerateSpecialFeature()
        {
            if (random.Next(0, 2) == 0)
                return "yes";
            else 
                return "no";
        }

        public string SpecialFeatureByType(string ProductType)
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
    }
}
