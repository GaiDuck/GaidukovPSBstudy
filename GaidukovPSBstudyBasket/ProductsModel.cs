using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal class ProductsModel //сделать абстрактным после вынесения генератора - не получается, мне необходимо иметь возможность создавать экземпляры этого класса.
    {
        //Свойства для моделей продуктов - наследуется именно эта часть
        public string Article { get; set; }
        public string ProductType { get; set; }
        public double Cost { get; set; }
        public double Score { get; set; }
        public double Weight { get; set; }
        public int DeliveryDays { get; set; }
        public string SpecialFeature { get; set; }
    }
}
