using GaidukovPSBstudyBasket;
using GaidukovPSBstudyBasket.DataBase;
using GaidukovPSBstudyBasket.Generator;
using GaidukovPSBstudyBasket.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace GaidukovPSBstudyTests
{
    [TestFixture]
    public class Tests
    {
        ProductsGenerator product = new ProductsGenerator();
        OrderGenerator order = new OrderGenerator();
        BasketConvertor basket = new BasketConvertor();

        static string testPath = @"C:\Users\alexg\OneDrive\Рабочий стол\GaidukovPSBstudyCalculator-08b48238f18b7f9e03efc951a4fc3d611d2aca50\GaidukovPSBstudyCalculator\GaidukovPSBstudyBasket\TestOrders\";
        bool testPassed = true;

        List <int> numbers = new List<int> { 1, 2, 3 };
        List <ProductsModel> products = new List<ProductsModel> ();
        List <ProductsModel> sortedProducts = new List<ProductsModel> ();

        [SetUp]
        public void Setup()
        {
            product.GetShopAssortment();

            products.Clear();

            for (int i = 0; i < 3; i++)
            {
                products.Add(product.GetRandomProduct(product.GetRandomType()));
            }
        }

        //ProductGeneratorTests

        [TestCase ("Стиральная машина", "Сушилка")]
        [TestCase ("Фен", "Турборежим")]
        [TestCase ("Микроволновая печь", "Режим разморозки")]
        [TestCase ("Миксер", "#####")]
        public void GetSpecialFeatureByTypeTest(string productType, string expectedResult)
        {
            Assert.That(product.GetSpecialFeatureByType(productType), Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetWashingMachinesAssortmentTest()
        {
            Assert.That(ProductsDataBase.WashingMachines.Count, Is.EqualTo(ProductsGenerator.NumberOfGeneratedProducts));
        }

        [Test]
        public void GetFansAssortmentTest()
        {
            Assert.That(ProductsDataBase.Fans.Count, Is.EqualTo(ProductsGenerator.NumberOfGeneratedProducts));
        }

        [Test]
        public void GetMicrowavesAssortmentTest()
        {
            Assert.That(ProductsDataBase.Microwaves.Count, Is.EqualTo(ProductsGenerator.NumberOfGeneratedProducts));
        }

        //OrderGeneratorTests

        [TestCase ("1", true)]
        [TestCase ("2", false)]
        [TestCase ("5", false)]
        [TestCase ("Hello", false)]
        [TestCase ("", false)]
        public void OneMoreOrderTest(string str, bool makeOneMoreOrder)
        {
            Assert.That(order.OneMoreOrderForTest(str), Is.EqualTo(makeOneMoreOrder));
        }

        [Test]
        public void SeachForOrdersTest()
        {
            Assert.That(order.SeachForOrdersForTest(testPath), Is.EqualTo(numbers));
        }

        //BasketConverter
        [TestCase ("1", 1)]
        [TestCase ("2", 2)]
        [TestCase ("3", 3)]
        [TestCase ("4", 4)]
        [TestCase ("5", 5)]
        [TestCase ("6", 1)]
        [TestCase ("10", 1)]
        [TestCase ("Hello", 1)]
        [TestCase ("", 1)]
        public void GetSortingParametrTest(string str, int expectedResult)
        {
            Assert.That(basket.GetSortingParametrForTest(str), Is.EqualTo(expectedResult));
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(10)]
        [TestCase(50)]
        public void GetSortedProductListTest(int sortingPattern)
        {
            sortedProducts.Clear();
            testPassed = true;
            
            sortedProducts.AddRange(basket.GetSortedProductList(products, sortingPattern));

            switch (sortingPattern)
            {
                case 1:
                    for (int i = 0; i < sortedProducts.Count - 1; i++)
                    {
                        if (string.Compare(sortedProducts[i].Article, sortedProducts[i + 1].Article) > 0)
                            testPassed = false;
                    }
                break;

                case 2:
                    for (int i = 0; i < sortedProducts.Count - 1; i++)
                    {
                        if (sortedProducts[i].Cost > sortedProducts[i + 1].Cost)
                            testPassed = false;
                    }
                break;

                case 3:
                    for (int i = 0; i < sortedProducts.Count - 1; i++)
                    {
                        if (sortedProducts[i].Score < sortedProducts[i + 1].Score)
                            testPassed = false;
                    }
                break;

                case 4:
                    for (int i = 0; i < sortedProducts.Count - 1; i++)
                    {
                        if (sortedProducts[i].Weight > sortedProducts[i + 1].Weight)
                            testPassed = false;
                    }
                break;

                case 5:
                    for (int i = 0; i < sortedProducts.Count - 1; i++)
                    {
                        if (sortedProducts[i].DeliveryDays > sortedProducts[i + 1].DeliveryDays)
                            testPassed = false;
                    }
                break;

                default:
                    for (int i = 0; i < sortedProducts.Count - 1; i++)
                    {
                        if (string.Compare(sortedProducts[i].Article, sortedProducts[i + 1].Article) > 0)
                            testPassed = false;
                    }
                    break;       
            }

            if (testPassed) 
                Assert.Pass();
            else 
                Assert.Fail();
        }


    }
} 