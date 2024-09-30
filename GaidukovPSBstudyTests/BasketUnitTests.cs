using GaidukovPSBstudyBasket.Generator;
using System.Reflection.Emit;

namespace GaidukovPSBstudyTests
{            
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            ProductsGenerator generator = new ProductsGenerator();
        }

        [TestCase ("Стиральная машина", "Сушилка")]
        [TestCase ("Фен", "Турборежим")]
        [TestCase ("Микроволновая печь", "Режим разморозки")]
        [TestCase ("Миксер", "#####")]
        public void GetSpecialFeatureByTypeTest(string productType, string expectedResult)
        {
            Assert.That(generator.GetSpecialFeatureByType(), Is.EqualTo (expectedResult));
        }
    }
}
