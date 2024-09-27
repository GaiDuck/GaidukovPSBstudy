using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaidukovPSBstudyBasket
{
    internal static class ProductsDataBase
    {
        //Хранение ещё один отдельный класс
        public static List<ProductsModel> WashingMachines = new List<ProductsModel>();
        public static List<ProductsModel> Fans = new List<ProductsModel>();
        public static List<ProductsModel> Microwaves = new List<ProductsModel>();
    }
}
