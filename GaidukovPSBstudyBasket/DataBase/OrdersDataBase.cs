using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GaidukovPSBstudyBasket.Models;

namespace GaidukovPSBstudyBasket.DataBase
{
    static class OrdersDataBase
    {
        public static List<OrderCardModel> orderCardsList = new List<OrderCardModel>();
        public static List<OrderCardModel> relevantOrderCardsList = new List<OrderCardModel>();
    }
}